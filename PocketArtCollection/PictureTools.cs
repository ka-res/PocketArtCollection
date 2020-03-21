using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Media;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Interop;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using PocketArtCollection.Models;

namespace PocketArtCollection
{
    public class PictureTools
    {
        //public static byte[] monaLisa = ImageToByte(Bitmap2BitmapImage(Properties.Resources.monLisaDaVinci));
        //public static byte[] damaZLasiczka = ImageToByte(PictureTools.Bitmap2BitmapImage(Properties.Resources.damaZLasiczkaDaVinci));
        //public static byte[] fontanna = PictureTools.ImageToByte(PictureTools.Bitmap2BitmapImage(Properties.Resources.fontannaDuchamp));
        //public static byte[] lhooq = PictureTools.ImageToByte(PictureTools.Bitmap2BitmapImage(Properties.Resources.lhooqDuchamp));
        //public static byte[] guernica = PictureTools.ImageToByte(PictureTools.Bitmap2BitmapImage(Properties.Resources.guernicaPicasso));
        //public static byte[] syrenkaWarszawska = PictureTools.ImageToByte(PictureTools.Bitmap2BitmapImage(Properties.Resources._12_WarszawskasyrenaPablaPicassaz1948r_));

        public static string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }

        public static ImageSource ByteToImageSource(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);

            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;
            return imgSrc;
        }

        public static byte[] ImageToByte(BitmapImage bmpImg)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmpImg));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapImage retval;

            try
            {
                retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }
    }
}