using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MuPDFLib
{
    interface IApi
    {
        IntPtr CreateMuPDFClass();
        void DisposeMuPDFClass(IntPtr pTestClassObject);
        int LoadPdf(IntPtr pTestClassObject, string filename, string password);
        int LoadPdfFromStream(IntPtr pTestClassObject, byte[] buffer, int bufferSize, string password);
        int LoadPage(IntPtr pTestClassObject, int pageNumber);
        float GetWidth(IntPtr pTestClassObject);
        float GetHeight(IntPtr pTestClassObject);
        void SetAlphaBits(IntPtr pTestClassObject, int alphaBits);
        IntPtr GetBitmapData(IntPtr pTestClassObject, out int width, out int height, float dpix, float dpiy, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, out int nLength, int maxSize);
        void FreeRenderedPage(IntPtr pTestClassObject);
    }

    internal class ThirtyTwoBitApi : IApi
    {
        private const string MuDLL = "MuPDFLib-x86.dll";

        [DllImport(MuDLL, EntryPoint = "CreateMuPDFClass", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static private extern IntPtr CreateMuPDFClass_EXT();

        public IntPtr CreateMuPDFClass()
        {
            return CreateMuPDFClass_EXT();
        }

        [DllImport(MuDLL, EntryPoint = "DisposeMuPDFClass", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static private extern void DisposeMuPDFClass_EXT(IntPtr pTestClassObject);

        public void DisposeMuPDFClass(IntPtr pTestClassObject)
        {
            DisposeMuPDFClass_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetBitmap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr CallGetBitmap_EXT(IntPtr pTestClassObject, out int width, out int height, float dpix, float dpiy, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, out int nLength, int maxSize);

        public IntPtr GetBitmapData(IntPtr pTestClassObject, out int width, out int height, float dpix, float dpiy, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, out int nLength, int maxSize)
        {
            return CallGetBitmap_EXT(pTestClassObject, out width, out height, dpix, dpiy, rotation, colorspace, rotateLandscapePages, convertToLetter, out nLength, maxSize);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPdf", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPdf_EXT(IntPtr pTestClassObject, string filename, string password);

        public int LoadPdf(IntPtr pTestClassObject, string filename, string password)
        {
            return CallLoadPdf_EXT(pTestClassObject, filename, password);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPdfFromStream", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPdfFromStream_EXT(IntPtr pTestClassObject, byte[] buffer, int bufferSize, string password);

        public int LoadPdfFromStream(IntPtr pTestClassObject, byte[] buffer, int bufferSize, string password)
        {
            return CallLoadPdfFromStream_EXT(pTestClassObject, buffer, bufferSize, password);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPage_EXT(IntPtr pTestClassObject, int pageNumber);

        public int LoadPage(IntPtr pTestClassObject, int pageNumber)
        {
            return CallLoadPage_EXT(pTestClassObject, pageNumber);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetWidth", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern float CallGetWidth_EXT(IntPtr pTestClassObject);

        public float GetWidth(IntPtr pTestClassObject)
        {
            return CallGetWidth_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetHeight", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern float CallGetHeight_EXT(IntPtr pTestClassObject);

        public float GetHeight(IntPtr pTestClassObject)
        {
            return CallGetHeight_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallSetAlphaBits", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void CallSetAlphaBits_EXT(IntPtr pTestClassObject, int alphaBits);

        public void SetAlphaBits(IntPtr pTestClassObject, int alphaBits)
        {
            CallSetAlphaBits_EXT(pTestClassObject, alphaBits);
        }

        [DllImport(MuDLL, EntryPoint = "CallFreeRenderedPage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void CallFreeRenderedPage_EXT(IntPtr pTestClassObject);

        public void FreeRenderedPage(IntPtr pTestClassObject)
        {
            CallFreeRenderedPage_EXT(pTestClassObject);
        }
    }

    internal class SixtyFourBitApi : IApi
    {
        private const string MuDLL = "MuPDFLib-x64.dll";

        [DllImport(MuDLL, EntryPoint = "CreateMuPDFClass", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static private extern IntPtr CreateMuPDFClass_EXT();

        public IntPtr CreateMuPDFClass()
        {
            return CreateMuPDFClass_EXT();
        }

        [DllImport(MuDLL, EntryPoint = "DisposeMuPDFClass", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static private extern void DisposeMuPDFClass_EXT(IntPtr pTestClassObject);

        public void DisposeMuPDFClass(IntPtr pTestClassObject)
        {
            DisposeMuPDFClass_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetBitmap", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern IntPtr CallGetBitmap_EXT(IntPtr pTestClassObject, out int width, out int height, float dpix, float dpiy, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, out int nLength, int maxSize);

        public IntPtr GetBitmapData(IntPtr pTestClassObject, out int width, out int height, float dpix, float dpiy, int rotation, int colorspace, bool rotateLandscapePages, bool convertToLetter, out int nLength, int maxSize)
        {
            return CallGetBitmap_EXT(pTestClassObject, out width, out height, dpix, dpiy, rotation, colorspace, rotateLandscapePages, convertToLetter, out nLength, maxSize);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPdf", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPdf_EXT(IntPtr pTestClassObject, string filename, string password);

        public int LoadPdf(IntPtr pTestClassObject, string filename, string password)
        {
            return CallLoadPdf_EXT(pTestClassObject, filename, password);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPdfFromStream", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPdfFromStream_EXT(IntPtr pTestClassObject, byte[] buffer, int bufferSize, string password);

        public int LoadPdfFromStream(IntPtr pTestClassObject, byte[] buffer, int bufferSize, string password)
        {
            return CallLoadPdfFromStream_EXT(pTestClassObject, buffer, bufferSize, password);
        }

        [DllImport(MuDLL, EntryPoint = "CallLoadPage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int CallLoadPage_EXT(IntPtr pTestClassObject, int pageNumber);

        public int LoadPage(IntPtr pTestClassObject, int pageNumber)
        {
            return CallLoadPage_EXT(pTestClassObject, pageNumber);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetWidth", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern float CallGetWidth_EXT(IntPtr pTestClassObject);

        public float GetWidth(IntPtr pTestClassObject)
        {
            return CallGetWidth_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallGetHeight", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern float CallGetHeight_EXT(IntPtr pTestClassObject);

        public float GetHeight(IntPtr pTestClassObject)
        {
            return CallGetHeight_EXT(pTestClassObject);
        }

        [DllImport(MuDLL, EntryPoint = "CallSetAlphaBits", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void CallSetAlphaBits_EXT(IntPtr pTestClassObject, int alphaBits);

        public void SetAlphaBits(IntPtr pTestClassObject, int alphaBits)
        {
            CallSetAlphaBits_EXT(pTestClassObject, alphaBits);
        }

        [DllImport(MuDLL, EntryPoint = "CallFreeRenderedPage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern void CallFreeRenderedPage_EXT(IntPtr pTestClassObject);

        public void FreeRenderedPage(IntPtr pTestClassObject)
        {
            CallFreeRenderedPage_EXT(pTestClassObject);
        }
    }

    public class MuPDF : IDisposable
    {
        private IApi _Api;
        private IntPtr m_pNativeObject;
        private string _FileName;
        private byte[] _Image;
        private GCHandle _ImagePin;
        private string _PdfPassword;
        private int _CurrentPage;
        private int _LoadType;
        private int _AliasBits;

        public int PageCount { get; set; }
        public int Page
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = _Api.LoadPage(this.m_pNativeObject, value);
            }
        }

        public double Width
        {
            get
            {
                if (_CurrentPage > 0)
                    return _Api.GetWidth(this.m_pNativeObject);
                else
                    return 0;
            }
        }

        public double Height
        {
            get
            {
                if (_CurrentPage > 0)
                    return _Api.GetHeight(this.m_pNativeObject);
                else
                    return 0;
            }
        }

        public bool AntiAlias
        {
            get
            {
                if (_AliasBits > 0)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    if (_AliasBits != 8)
                    {
                        _AliasBits = 8;
                        _Api.SetAlphaBits(this.m_pNativeObject, 8);
                    }
                }
                else
                {
                    if (_AliasBits != 0)
                    {
                        _AliasBits = 0;
                        _Api.SetAlphaBits(this.m_pNativeObject, 0);
                    }
                }
            }
        }

        public MuPDF(byte[] image, string pdfPassword)
        {
            _LoadType = 1;
            _Image = image;
            _PdfPassword = pdfPassword;

            if (image == null)
                throw new ArgumentNullException();
            Initialize();
        }

        public MuPDF(string fileName, string pdfPassword)
        {
            _LoadType = 0;
            _FileName = fileName;
            _PdfPassword = pdfPassword;

            if (!File.Exists(_FileName))
                throw new FileNotFoundException("Cannot find file to open!", _FileName);
            Initialize();
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public void Initialize()
        {
            if (Environment.Is64BitProcess)
                _Api = new SixtyFourBitApi();
            else
                _Api = new ThirtyTwoBitApi();


            this.m_pNativeObject = _Api.CreateMuPDFClass();
            if (_LoadType == 0)
                PageCount = _Api.LoadPdf(this.m_pNativeObject, _FileName, _PdfPassword);
            else if (_LoadType == 1)
            {
                _ImagePin = GCHandle.Alloc(_Image, GCHandleType.Pinned);
                PageCount = _Api.LoadPdfFromStream(this.m_pNativeObject, _Image, (int)_Image.Length, _PdfPassword);
            }

            if (PageCount == -5)
                throw new Exception("PDF password needed!");
            else if (PageCount == -6)
                throw new Exception("Invalid PDF password supplied!");
            else if (PageCount < 1)
                throw new Exception("Unable to open pdf document!");
            _CurrentPage = 1;

            _AliasBits = 0;
            _Api.SetAlphaBits(this.m_pNativeObject, 0);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        protected virtual void Dispose(bool bDisposing)
        {
            if (this.m_pNativeObject != IntPtr.Zero)
            {
                // Call the DLL Export to dispose this class
                _Api.DisposeMuPDFClass(this.m_pNativeObject);
                this.m_pNativeObject = IntPtr.Zero;
                if (_ImagePin.IsAllocated)
                    _ImagePin.Free();
            }

            if (bDisposing)
            {
                // No need to call the finalizer since we've now cleaned
                // up the unmanaged memory
                GC.SuppressFinalize(this);
            }
        }

        ~MuPDF()
        {
            Dispose(false);
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public unsafe Bitmap GetBitmap(int width, int height, float dpix, float dpiy, int rotation, RenderType type, bool rotateLandscapePages, bool convertToLetter, int maxSize)
        {
            Bitmap bitmap2 = null;
            int nLength = 0;
            IntPtr data = _Api.GetBitmapData(this.m_pNativeObject, out width, out height, dpix, dpiy, rotation, (int)type, rotateLandscapePages, convertToLetter, out nLength, maxSize);
            if (data == null || data == IntPtr.Zero)
                throw new Exception("Unable to render pdf page to bitmap!");

            if (type == RenderType.RGB)
            {
                bitmap2 = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData imageData = bitmap2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap2.PixelFormat);
                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)imageData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        //Swap these here instead of in MuPDF because most pdf images will be rgb or cmyk.
                        //Since we are going through the pixels one by one anyway swap here to save a conversion from rgb to bgr.
                        pl[2] = sl[0]; //b-r
                        pl[1] = sl[1]; //g-g
                        pl[0] = sl[2]; //r-b
                        //pl[3] = sl[3]; //alpha
                        pl += 3;
                        sl += 4;
                    }
                    ptrDest += imageData.Stride;
                    ptrSrc += width * 4;
                }
                bitmap2.UnlockBits(imageData);
            }
            else if (type == RenderType.Grayscale)
            {
                bitmap2 = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                ColorPalette palette = bitmap2.Palette;
                for (int i = 0; i < 256; ++i)
                    palette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
                bitmap2.Palette = palette;
                BitmapData imageData = bitmap2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap2.PixelFormat);

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)imageData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        pl[0] = sl[0];
                        //pl[1] = sl[1]; //alpha
                        pl += 1;
                        sl += 2;
                    }
                    ptrDest += imageData.Stride;
                    ptrSrc += width * 2;
                }
                bitmap2.UnlockBits(imageData);
            }
            else//RenderType.Monochrome
            {
                //bitmap2 = new Bitmap(width, height, bmpstride, PixelFormat.Format1bppIndexed, data);//Doesn't free memory
                bitmap2 = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
                ColorPalette palette = bitmap2.Palette;
                palette.Entries[0] = System.Drawing.Color.FromArgb(0, 0, 0);
                palette.Entries[1] = System.Drawing.Color.FromArgb(255, 255, 255);
                bitmap2.Palette = palette;
                BitmapData imageData = bitmap2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap2.PixelFormat);

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)imageData.Scan0;
                for (int i = 0; i < nLength; i++)
                {
                    ptrDest[i] = ptrSrc[i];
                }
                bitmap2.UnlockBits(imageData);
            }
            bitmap2.SetResolution(dpix, dpiy);
            _Api.FreeRenderedPage(this.m_pNativeObject);//Free unmanaged array

            return bitmap2;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public unsafe BitmapSource GetBitmapSource(int width, int height, float dpix, float dpiy, int rotation, RenderType type, bool rotateLandscapePages, bool convertToLetter, int maxSize)
        {
            WriteableBitmap write = null;
            int nLength = 0;
            IntPtr data = _Api.GetBitmapData(this.m_pNativeObject, out width, out height, dpix, dpiy, rotation, (int)type, rotateLandscapePages, convertToLetter, out nLength, maxSize);
            if (data == null || data == IntPtr.Zero)
                throw new Exception("Unable to render pdf page to bitmap!");

            if (type == RenderType.RGB)
            {
                const int depth = 24;
                int bmpstride = ((width * depth + 31) & ~31) >> 3;

                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.Bgr24, null);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        //Swap these here instead of in MuPDF because most pdf images will be rgb or cmyk.
                        //Since we are going through the pixels one by one anyway swap here to save a conversion from rgb to bgr.
                        pl[2] = sl[0]; //b-r
                        pl[1] = sl[1]; //g-g
                        pl[0] = sl[2]; //r-b
                        //pl[3] = sl[3]; //alpha
                        pl += 3;
                        sl += 4;
                    }
                    ptrDest += bmpstride;
                    ptrSrc += width * 4;
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            else if (type == RenderType.Grayscale)
            {
                const int depth = 8;//(n * 8)
                int bmpstride = ((width * depth + 31) & ~31) >> 3;

                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.Gray8, BitmapPalettes.Gray256);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        pl[0] = sl[0]; //g
                        //pl[1] = sl[1]; //alpha
                        pl += 1;
                        sl += 2;
                    }
                    ptrDest += bmpstride;
                    ptrSrc += width * 2;
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            else//RenderType.Monochrome
            {
                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.BlackWhite, BitmapPalettes.BlackAndWhite);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();
                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int i = 0; i < nLength; i++)
                {
                    ptrDest[i] = ptrSrc[i];
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            _Api.FreeRenderedPage(this.m_pNativeObject);//Free unmanaged array
            if (write.CanFreeze)
                write.Freeze();
            return write;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public unsafe WriteableBitmap GetWriteableBitmap(int width, int height, float dpix, float dpiy, int rotation, RenderType type, bool rotateLandscapePages, bool convertToLetter, int maxSize)
        {
            WriteableBitmap write = null;
            int nLength = 0;
            IntPtr data = _Api.GetBitmapData(this.m_pNativeObject, out width, out height, dpix, dpiy, rotation, (int)type, rotateLandscapePages, convertToLetter, out nLength, maxSize);
            if (data == null || data == IntPtr.Zero)
                throw new Exception("Unable to render pdf page to bitmap!");

            if (type == RenderType.RGB)
            {
                const int depth = 24;
                int bmpstride = ((width * depth + 31) & ~31) >> 3;

                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.Bgr24, null);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        //Swap these here instead of in MuPDF because most pdf images will be rgb or cmyk.
                        //Since we are going through the pixels one by one anyway swap here to save a conversion from rgb to bgr.
                        pl[2] = sl[0]; //b-r
                        pl[1] = sl[1]; //g-g
                        pl[0] = sl[2]; //r-b
                        //pl[3] = sl[3]; //alpha
                        pl += 3;
                        sl += 4;
                    }
                    ptrDest += bmpstride;
                    ptrSrc += width * 4;
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            else if (type == RenderType.Grayscale)
            {
                const int depth = 8;//(n * 8)
                int bmpstride = ((width * depth + 31) & ~31) >> 3;

                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.Gray8, BitmapPalettes.Gray256);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        pl[0] = sl[0]; //g
                        //pl[1] = sl[1]; //alpha
                        pl += 1;
                        sl += 2;
                    }
                    ptrDest += bmpstride;
                    ptrSrc += width * 2;
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            else
            {
                write = new WriteableBitmap(width, height, (double)dpix, (double)dpiy, PixelFormats.BlackWhite, BitmapPalettes.BlackAndWhite);
                Int32Rect rect = new Int32Rect(0, 0, width, height);
                write.Lock();
                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)write.BackBuffer;
                for (int i = 0; i < nLength; i++)
                {
                    ptrDest[i] = ptrSrc[i];
                }
                write.AddDirtyRect(rect);
                write.Unlock();
            }
            _Api.FreeRenderedPage(this.m_pNativeObject);//Free unmanaged array
            if (write.CanFreeze)
                write.Freeze();
            return write;
        }

        [System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptions]
        public unsafe byte[] GetPixels(ref int width, ref int height, float dpix, float dpiy, int rotation, RenderType type, bool rotateLandscapePages, bool convertToLetter, out uint cbStride, int maxSize)
        {
            byte[] output = null;
            int nLength = 0;
            IntPtr data = _Api.GetBitmapData(this.m_pNativeObject, out width, out height, dpix, dpiy, rotation, (int)type, rotateLandscapePages, convertToLetter, out nLength, maxSize);
            if (data == null || data == IntPtr.Zero)
                throw new Exception("Unable to render pdf page to bitmap!");

            if (type == RenderType.RGB)
            {
                const int depth = 24;
                int bmpstride = ((width * depth + 31) & ~31) >> 3;
                int newSize = bmpstride * height;

                output = new byte[newSize];
                cbStride = (uint)bmpstride;
                IntPtr DestPointer = Marshal.UnsafeAddrOfPinnedArrayElement(output, 0);

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)DestPointer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        //Swap these here instead of in MuPDF because most pdf images will be rgb or cmyk.
                        //Since we are going through the pixels one by one anyway swap here to save a conversion from rgb to bgr.
                        pl[2] = sl[0]; //b-r
                        pl[1] = sl[1]; //g-g
                        pl[0] = sl[2]; //r-b
                        //pl[3] = sl[3]; //alpha
                        pl += 3;
                        sl += 4;
                    }
                    ptrDest += cbStride;
                    ptrSrc += width * 4;
                }
            }
            else if (type == RenderType.Grayscale)
            {
                const int depth = 8;//(n * 8)
                int bmpstride = ((width * depth + 31) & ~31) >> 3;
                int newSize = bmpstride * height;

                output = new byte[newSize];
                cbStride = (uint)bmpstride;
                IntPtr DestPointer = Marshal.UnsafeAddrOfPinnedArrayElement(output, 0);

                byte* ptrSrc = (byte*)data;
                byte* ptrDest = (byte*)DestPointer;
                for (int y = 0; y < height; y++)
                {
                    byte* pl = ptrDest;
                    byte* sl = ptrSrc;
                    for (int x = 0; x < width; x++)
                    {
                        pl[0] = sl[0]; //g
                        //pl[1] = sl[1]; //alpha
                        pl += 1;
                        sl += 2;
                    }
                    ptrDest += cbStride;
                    ptrSrc += width * 2;
                }
            }
            else//RenderType.Monochrome
            {
                const int depth = 1;
                int bmpstride = ((width * depth + 31) & ~31) >> 3;

                cbStride = (uint)bmpstride;
                output = new byte[nLength];
                Marshal.Copy(data, output, 0, nLength);
            }
            _Api.FreeRenderedPage(this.m_pNativeObject);//Free unmanaged array
            return output;
        }
    }

    public enum RenderType
    {
        /// <summary>24-bit Color RGB</summary>
        RGB = 0,
        /// <summary>8-bit Grayscale</summary>
        Grayscale = 1,
        /// <summary>1-bit Monochrome</summary>
        Monochrome = 2
    }
}
