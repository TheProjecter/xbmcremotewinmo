using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
        static private IImage ResizeImageAndSave(XbmcConnection JsonClient, string url, int width, int height)
        {
            string fullpath = "\\Application Data\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");

            Image originalImage = JsonClient.Files.GetImageFromThumbnail(url);
            IImage originalIImage = SenseSDKExtended.DrawingCEEx.SenseAPIsEx.SenseImageEx.ImageToIImage(originalImage, originalImage.Size);
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

            return thumbIImage;
        }

        static private bool CheckExist(string url)
        {
            string fullpath = "\\Application Data\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");
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
            string fullpath = "\\Application Data\\XBMC_Remote\\cache\\" + url.Replace("special://", "").Replace("/", "\\");

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
                return ResizeImageAndSave(JsonClient, url, 200, 300);
            }
        }
    }
}
