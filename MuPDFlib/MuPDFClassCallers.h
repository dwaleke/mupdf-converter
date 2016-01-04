#ifndef __MuPDFClassCallers_h__
#define __MuPDFClassCallers_h__

#include "MuPDFClass.h"		// needed for MuPDFDLL_API

#ifdef __cplusplus
extern "C" {
#endif

extern MuPDFDLL_API MuPDFClass* CreateMuPDFClass();
extern MuPDFDLL_API void DisposeMuPDFClass(MuPDFClass* pObject);

extern MuPDFDLL_API int CallLoadPdf(MuPDFClass* pObject, char* filename, char* password);
extern MuPDFDLL_API int CallLoadPdfFromStream(MuPDFClass* pObject, unsigned char* buffer, int bufferSize, char* password);
extern MuPDFDLL_API int CallLoadPage(MuPDFClass* pObject, int pageNumber);
extern MuPDFDLL_API float CallGetWidth(MuPDFClass* pObject);
extern MuPDFDLL_API float CallGetHeight(MuPDFClass* pObject);
extern MuPDFDLL_API void CallSetAlphaBits(MuPDFClass* pObject, int alphaBits);
extern MuPDFDLL_API unsigned char* CallGetBitmap(MuPDFClass* pObject, int & width, int & height, float dpiX, float dpiY, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, int & pnLength, int maxSize);
extern MuPDFDLL_API void CallFreeRenderedPage(MuPDFClass* pObject);

#ifdef __cplusplus
}
#endif

#endif // __MuPDFClassCallers_h__

