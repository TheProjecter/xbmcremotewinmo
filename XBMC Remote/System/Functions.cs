using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using XbmcJson;
using Microsoft.Drawing;
using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;
using StedySoft.SenseSDK.Localization;

namespace XBMC_Remote
{
    class Functions
    {
        static private IImage _getIImageFromResource(string resource)
        {
            IImage iimg;

            using (MemoryStream strm = (MemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("XBMC_Remote.Resources." + resource + ".png"))
            {
                (ImagingFactory.GetImaging()).CreateImageFromBuffer(strm.GetBuffer(), (uint)strm.Length, BufferDisposalFlag.BufferDisposalFlagNone, out iimg);
            }
            return iimg;
        }

        static private IImage ResizeImageAndSave(XbmcConnection JsonClient, string url, int width, int height)
        {
            string fullpath = StorageCardUtils.GetFirstFlashCardPath() + "\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");

            Image originalImage = JsonClient.Files.GetImageFromThumbnail(url);
            IImage originalIImage = SenseSDKExtended.DrawingCEEx.SenseAPIsEx.SenseImageEx.ImageToIImage(originalImage, originalImage.Size);
            originalImage.Dispose();
            IImage thumbIImage;
            Rectangle destination = new Rectangle(0, 0, width, height);
            Bitmap thumbB = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(thumbB))
            {
                IntPtr hdc = g.GetHdc();
                originalIImage.GetThumbnail((uint)width, (uint)height, out thumbIImage);
                thumbIImage.Draw(hdc, ref destination, (IntPtr)null);
                g.ReleaseHdc(hdc);
            }
            thumbB.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
            thumbB.Dispose();

            return thumbIImage;
        }

        static private bool CheckExist(string url)
        {
            string fullpath = StorageCardUtils.GetFirstFlashCardPath() + "\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");
            string directory = Path.GetDirectoryName(fullpath);

            if (!File.Exists(fullpath))
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                return false;
            }
            return true;
        }

        static private IImage LoadImageFromDisk(string url)
        {
            string fullpath = StorageCardUtils.GetFirstFlashCardPath() + "\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");

            return SenseAPIs.SenseImage.LoadIImageFromFile(fullpath);
        }

        static public IImage GetTvShowThumbnail(XbmcConnection JsonClient, string url)
        {
            if (CheckExist(url) == true)
            {
                return LoadImageFromDisk(url);
            }
            else
            {
                return ResizeImageAndSave(JsonClient, url, Resolution.ScreenWidth, 100);
            }
        }

        static public IImage GetTvSeasonThumbnail(XbmcConnection JsonClient, string url)
        {
            if (CheckExist(url) == true)
            {
                return LoadImageFromDisk(url);
            }
            else
            {
                return ResizeImageAndSave(JsonClient, url, 150, 225);
            }
        }
    }

    public static class StorageCardUtils
    {
        public const int MAX_PATH = 260;

        //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/apisp/html/sp__wcesdk_findfirstflashcard.asp
        //HANDLE FindFirstFlashCard(
        //  LPWIN32_FIND_DATA lpFindFlashData
        //);
        [DllImport("note_prj.dll")]
        public static extern IntPtr FindFirstFlashCard(out Win32FindData findData);

        //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/wcedata5/html/wce50lrfWIN32FINDDATA.asp
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <author>Jon Froehlich</author>
        //typedef struct _WIN32_FIND_DATA { 
        //  DWORD dwFileAttributes; 
        //  FILETIME ftCreationTime; 
        //  FILETIME ftLastAccessTime; 
        //  FILETIME ftLastWriteTime; 
        //  DWORD nFileSizeHigh; 
        //  DWORD nFileSizeLow; 
        //  DWORD dwOID; 
        //  TCHAR cFileName[MAX_PATH]; 
        //} WIN32_FIND_DATA; 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <author>Jon Froehlich</author>
        public struct Win32FindData
        {
            public uint dwFileAttributes;
            public OpenNETCF.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public OpenNETCF.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public OpenNETCF.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwOID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;
        }

        //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/wcedata5/html/wce50lrffindclose.asp
        //BOOL FindClose( 
        //  HANDLE hFindFile 
        //); 
        [DllImport("coredll.dll")]
        public static extern bool FindClose(IntPtr hFindFile);

        public static String GetFirstFlashCardPath()
        {
            Win32FindData win32FindData;
            IntPtr intPtr = FindFirstFlashCard(out win32FindData);
            String path = win32FindData.cFileName;
            FindClose(intPtr);

            if (String.IsNullOrEmpty(path))
            {
                throw new Exception("Could not find a flash card on this device");
            }

            return path;
        }

        public static bool FlashCardExists()
        {
            try
            {
                string flashCardPath = GetFirstFlashCardPath();
                return !String.IsNullOrEmpty(flashCardPath);
            }
            catch
            {
                return false;
            }
        }

        public static long GetStorageCardSizeInBytes()
        {
            if (!FlashCardExists()) { return 0; }

            string storageCardPath = GetFirstFlashCardPath();
            ulong byteAvailableToCaller, totalNumOfBytes, totalNumOfFreeBytes;
            GetDiskFreeSpaceEx(storageCardPath, out byteAvailableToCaller, out totalNumOfBytes, out totalNumOfFreeBytes);
            return (long)totalNumOfBytes;
        }

        public static long GetStorageCardFreeSpaceInBytes()
        {
            if (!FlashCardExists()) { return 0; }

            string storageCardPath = GetFirstFlashCardPath();
            ulong byteAvailableToCaller, totalNumOfBytes, totalNumOfFreeBytes;
            GetDiskFreeSpaceEx(storageCardPath, out byteAvailableToCaller, out totalNumOfBytes, out totalNumOfFreeBytes);
            return (long)totalNumOfFreeBytes;
        }

        [DllImport("coredll.dll")]
        private static extern bool GetDiskFreeSpaceEx(
                string lpDirectoryName,
                out ulong lpFreeBytesAvailableToCaller,
                out ulong lpTotalNumberOfBytes,
                out ulong lpTotalNumberOfFreeBytes
                );
    }
}