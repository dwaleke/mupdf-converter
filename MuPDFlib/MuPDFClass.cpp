#include "MuPDFClass.h"

void MuPDFClass::CleanUp()
{
	fz_context* ctx = _ctx;
	if (_CurrentPage > 0)
	{
		fz_drop_page(_ctx, _page);
		fz_flush_warnings(_ctx);
	}
	if (_BitmapBuffer != NULL)
		FreeRenderedPage();
	if (_doc != NULL)
	{
		fz_drop_document(_ctx, _doc);
		_doc = NULL;
	}
	if (_FromStream)
	{
		fz_drop_stream(_ctx, _source);
		_FromStream = false;
		_source = NULL;
	}
	//_ctx->error.top = -1; //Hack to prevent errors on dispose.
	fz_drop_context(ctx);
	ctx = NULL;
	_CurrentPage = -1;
	_CurrentPageWidth = NULL;
	_CurrentPageHeight = NULL;
}

int MuPDFClass::LoadPdf(char* filename, char* password)
{
	_CurrentPage = -1;
	fz_try(_ctx)
	{
		_doc = fz_open_document(_ctx, filename);
		return LoadPdfInit(password);
	}
	fz_catch(_ctx)
	{
		return -1;
	}
	return -1;
}

int MuPDFClass::LoadPdfFromStream(unsigned char* buffer, int bufferSize, char* password)
{
	_CurrentPage = -1;

	fz_try(_ctx)
	{
		_source = fz_open_memory(_ctx, buffer, bufferSize);
		_FromStream = true;
		const char * type = ".pdf";
		_doc = fz_open_document_with_stream(_ctx, type, _source);
		return LoadPdfInit(password);
	}
	fz_catch(_ctx)
	{
		return -1;
	}
	return -1;
}

int MuPDFClass::LoadPdfInit(char* password)
{
	if (fz_needs_password(_ctx, _doc))
	{
		if (password != NULL)
		{
			if (!fz_authenticate_password(_ctx, _doc, password))
				return -6;
		}
		else
			return -5;
	}

	_PageCount = fz_count_pages(_ctx, _doc);

	return _PageCount;
}

int MuPDFClass::LoadPage(int pageNumber)
{
	fz_rect bounds;
	if (_CurrentPage > 0)
	{
		fz_drop_page(_ctx, _page);
		fz_flush_warnings(_ctx);
	}
	fz_try(_ctx)
	{
		_page = fz_load_page(_ctx, _doc, pageNumber - 1);
		bounds = fz_bound_page(_ctx, _page);

		_CurrentPage = pageNumber;
		_CurrentPageWidth = bounds.x1 - bounds.x0;
		_CurrentPageHeight = bounds.y1 - bounds.y0;
		return pageNumber;
	}
	fz_catch(_ctx)
	{
		fz_throw(_ctx, FZ_ERROR_GENERIC, "cannot load page %d'", pageNumber);
		if (_page != NULL)
			fz_drop_page(_ctx, _page);
		_page = NULL;
	}
	return -1;
}

float MuPDFClass::GetWidth()
{
	return _CurrentPageWidth;
}

float MuPDFClass::GetHeight()
{
	return _CurrentPageHeight;
}

void MuPDFClass::SetAlphaBits(int alphaBits)
{
	fz_set_aa_level(_ctx, alphaBits);
}

unsigned char* MuPDFClass::GetBitmap(int & width, int & height, float dpiX, float dpiY, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, int & pnLength, int maxSize)
{
	fz_colorspace *destcolor;
	fz_matrix ctm;
	fz_rect bounds, tbounds;
	fz_irect ibounds;
	fz_pixmap* pix;
	fz_device *dev;

	float zoomX;
	float zoomY;
	float pdfWidth;
	float pdfHeight;
	float temp;

	fz_var(dev);
	fz_var(pix);


	//Use RGB colorspace and perform conversion to bgr when copying into bitmap.
	//Saves the conversion of rgb to bgr for anything stored already in RGB in pdf.
	if (colorspace == 0)
		destcolor = fz_device_rgb(_ctx);
	else if (colorspace == 1)
		destcolor = fz_device_gray(_ctx);
	else
		destcolor = fz_device_mono(_ctx);

	bounds = fz_bound_page(_ctx, _page);

	pdfWidth = _CurrentPageWidth;
	pdfHeight = _CurrentPageHeight;
	if (maxSize > 0 && width == 0 && (pdfWidth > maxSize || pdfHeight > maxSize))
	{
		width = 8.5 * dpiX;
	}
	if (pdfWidth < 0)
		pdfWidth = pdfWidth * -1;
	if (pdfHeight < 0)
		pdfHeight = pdfHeight * -1;

	if (pdfWidth == 0 || pdfHeight == 0)
		return NULL;//return null if mediabox is messed up.

	if (width == 0 && height == 0)  //No resize.  Scale to resolution.
	{
		width = pdfWidth * dpiX / 72.0f;
		height = pdfHeight * dpiY / 72.0f;
	}


	if (rotateLandscapePages && pdfWidth > 648 && pdfHeight > 576 && pdfHeight / pdfWidth < 0.85)
	{
		rotation = -90;
		temp = height;
		height = width;
		width = temp;

		if (width == 0)
			width = (height / dpiX) * (pdfWidth / pdfHeight) * dpiY;
		if (convertToLetter && (width / dpiY) > 11)
			width = 11 * dpiX;

		zoomX = width / (pdfWidth);
		zoomY = height / (pdfHeight);
	}
	else
	{
		if (height == 0)
			height = (width / dpiX) * (pdfHeight / pdfWidth) * dpiY;
		if (convertToLetter && (height / dpiY) > 11)
			height = 11 * dpiY;
		zoomX = width / (pdfWidth);
		zoomY = height / (pdfHeight);
	}
	ctm = fz_rotate(rotation);
	ctm = fz_pre_scale(ctm, zoomX, zoomY);//dw

	//ctm = fz_transform_page(bounds, 72, rotation);//72 = no resolution change with this method.
	//ctm = fz_pre_scale(ctm, zoomX, zoomY);//dw
	
	tbounds = fz_transform_rect(bounds, ctm);
	ibounds = fz_round_rect(tbounds);
	tbounds = fz_rect_from_irect(ibounds);

	ibounds = fz_round_rect(tbounds);
	tbounds = fz_rect_from_irect(ibounds);

	fz_try(_ctx)
	{
		pix = fz_new_pixmap_with_bbox(_ctx, destcolor, ibounds, nullptr, 1);//dw

		if (!pix)
			return NULL;
		pix->xres = dpiX;
		pix->yres = dpiY;

		fz_clear_pixmap_with_value(_ctx, pix, 255);

		dev = fz_new_draw_device(_ctx, fz_identity, pix);
		//fz_enable_device_hints(_ctx, dev, FZ_NO_CACHE);
		fz_run_page(_ctx, _page, dev, ctm, nullptr);
		fz_close_device(_ctx, dev);
		fz_drop_device(_ctx, dev);
		dev = NULL;

		width = fz_pixmap_width(_ctx, pix); //pix->w;
		height = fz_pixmap_height(_ctx, pix); //pix->h;


		if (_BitmapBuffer != NULL)
			FreeRenderedPage();

		if (destcolor == fz_device_mono(_ctx))
		{
			unsigned char *MonoBuffer, *o, *p;
			int x, y, pstride;
			int MonoStride = ((1 * pix->w + 31) & ~31) >> 3;
			int MonoSize = pix->h * MonoStride;
			MonoBuffer = (unsigned char *)fz_calloc(_ctx, pix->h, MonoStride);
			memset(MonoBuffer, 0, MonoSize);
			pnLength = MonoSize;
			pstride = pix->w * pix->n;

			p = pix->samples;
			o = MonoBuffer;
			for (y = 0; y<pix->h; y++)
			{
				for (x = 0; x<pix->w; x++, p += 2)
				{
					int index = y * MonoStride + (x >> 3);
					if (p[0] > 128)
					{
						unsigned char p2 = o[index];
						unsigned char mask = (unsigned char)(0x80 >> (x & 0x7));
						p2 |= mask;
						o[index] = p2;
					}
				}
			}
			_BitmapBuffer = MonoBuffer;
		}
		else
		{
			pnLength = pix->h * pix->w * pix->n;
			_BitmapBuffer = pix->samples;
			pix->samples = NULL;
		}
		fz_drop_pixmap(_ctx, pix);
	}
	fz_always(_ctx)
	{
		fz_drop_device(_ctx, dev);
		fz_drop_pixmap(_ctx, pix);
		dev = NULL;
	}
	fz_catch(_ctx)
	{		
		
	}

	return _BitmapBuffer;
}

void MuPDFClass::FreeRenderedPage()
{
	if (_BitmapBuffer != NULL)
	{
		free(_BitmapBuffer);
		_BitmapBuffer = NULL;
	}
}