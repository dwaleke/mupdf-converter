
#include "MuPDFClassCallers.h"


extern "C" MuPDFDLL_API MuPDFClass* CreateMuPDFClass()
{
	return new MuPDFClass();
}

extern "C" MuPDFDLL_API void DisposeMuPDFClass(MuPDFClass* pObject)
{
	if(pObject != NULL)
	{
		delete pObject;
		pObject = NULL;
	}
}

extern "C" MuPDFDLL_API int CallLoadPdf(MuPDFClass* pObject, char* filename, char* password)
{
	if(pObject != NULL)
	{
		return pObject->LoadPdf(filename, password);
	}
	else
		return -1;
}

extern "C" MuPDFDLL_API int CallLoadPdfFromStream(MuPDFClass* pObject, unsigned char* buffer, int bufferSize, char* password)
{
	if(pObject != NULL)
	{
		return pObject->LoadPdfFromStream(buffer, bufferSize, password);
	}
	else
		return -1;
}

extern "C" MuPDFDLL_API int CallLoadPage(MuPDFClass* pObject, int pageNumber)
{
	if(pObject != NULL)
	{
		return pObject->LoadPage(pageNumber);
	}
	else
		return -1;
}

extern "C" MuPDFDLL_API float CallGetWidth(MuPDFClass* pObject)
{
	if(pObject != NULL)
	{
		return pObject->GetWidth();
	}
	else
		return -1;
}

extern "C" MuPDFDLL_API float CallGetHeight(MuPDFClass* pObject)
{
	if(pObject != NULL)
	{
		return pObject->GetHeight();
	}
	else
		return -1;
}

extern "C" MuPDFDLL_API void CallSetAlphaBits(MuPDFClass* pObject, int alphaBits)
{
	if(pObject != NULL)
	{
		pObject->SetAlphaBits(alphaBits);
	}
}

extern "C" MuPDFDLL_API unsigned char* CallGetBitmap(MuPDFClass* pObject, int & width, int & height, float dpiX, float dpiY, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, int & pnLength, int maxSize)
{
	if(pObject != NULL)
	{
		return pObject->GetBitmap(width, height, dpiX, dpiY, rotation, colorspace, rotateLandscapePages, convertToLetter, pnLength, maxSize);
	}
	else
		return nullptr;
}

extern "C" MuPDFDLL_API void CallFreeRenderedPage(MuPDFClass* pObject)
{
	if(pObject != NULL)
	{
		return pObject->FreeRenderedPage();
	}
}