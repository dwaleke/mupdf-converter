#ifndef __MuPDF_h__
#define __MuPDF_h__

#define MuPDFDLL_EXPORTS
// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the MuPDFDLL_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// MuPDFDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MuPDFDLL_EXPORTS
#define MuPDFDLL_API __declspec(dllexport)
#else
#define MuPDFDLL_API __declspec(dllimport)
#endif

#include <vcruntime_string.h>

extern "C" {
#include "mupdf/fitz.h"
}

typedef struct
{
	size_t size;
#if defined(_M_IA64) || defined(_M_AMD64)
	size_t align;
#endif
} trace_header;

static size_t memtrace_current = 0;
static size_t memtrace_peak = 0;
static size_t memtrace_total = 0;

static void *
trace_malloc(void *arg, unsigned int size)
{
	trace_header *p;
	if (size == 0)
		return NULL;
	p = (trace_header *)malloc(size + sizeof(trace_header));
	if (p == NULL)
		return NULL;
	p[0].size = size;
	memtrace_current += size;
	memtrace_total += size;
	if (memtrace_current > memtrace_peak)
		memtrace_peak = memtrace_current;
	return (void *)&p[1];
}

static void
trace_free(void *arg, void *p_)
{
	trace_header *p = (trace_header *)p_;

	if (p == NULL)
		return;
	memtrace_current -= p[-1].size;
	free(&p[-1]);
}

static void *
trace_realloc(void *arg, void *p_, unsigned int size)
{
	trace_header *p = (trace_header *)p_;
	size_t oldsize;

	if (size == 0)
	{
		trace_free(arg, p_);
		return NULL;
	}
	if (p == NULL)
		return trace_malloc(arg, size);
	oldsize = p[-1].size;
	p = (trace_header *)realloc(&p[-1], size + sizeof(trace_header));
	if (p == NULL)
		return NULL;
	memtrace_current += size - oldsize;
	if (size > oldsize)
		memtrace_total += size - oldsize;
	if (memtrace_current > memtrace_peak)
		memtrace_peak = memtrace_current;
	p[0].size = size;
	return &p[1];
}

class MuPDFDLL_API MuPDFClass
{
private:
	fz_stream* _source;
	fz_document* _doc;
	fz_context* _ctx;
	fz_page* _page;

	bool _FromStream;
	int _CurrentPage;
	float _CurrentPageWidth;
	float _CurrentPageHeight;
	unsigned char* _BitmapBuffer;
	void CleanUp();

public:
	int _PageCount;
	MuPDFClass()
	{
		_source = NULL;
		_FromStream = false;
		_doc = NULL;
		_ctx = NULL;
		_page = NULL;
		_PageCount = -1;
		_CurrentPage = -1;
		_CurrentPageWidth = 0;
		_CurrentPageHeight = 0;
		_BitmapBuffer = NULL;
		static int showmemory = 0;

		fz_var(_doc);

		_ctx = fz_new_context(NULL, NULL, FZ_STORE_DEFAULT);
		if (!_ctx)
		{
			//error here
			fprintf(stderr, "cannot initialise context\n");
			exit(1);
		}
		fz_register_document_handlers(_ctx);
	};
	~MuPDFClass()
	{
		CleanUp();
	};
	int LoadPdf(char* filename, char* password);
	int LoadPdfFromStream(unsigned char* buffer, int bufferSize, char* password);
	int LoadPdfInit(char* password);
	int LoadPage(int pageNumber);
	float GetWidth();
	float GetHeight();
	void SetAlphaBits(int alphaBits);
	unsigned char* GetBitmap(int & width, int & height, float dpiX, float dpiY, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, int & pnLength, int maxSize);
	void FreeRenderedPage();


};

#endif	// __MuPDF_h__