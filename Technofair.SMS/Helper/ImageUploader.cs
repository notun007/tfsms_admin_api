using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;


namespace TFSMS.Admin.Helper
{
    public class ImageUploader
    {
        // set default size here
        public int Width { get; set; }

        public int Height { get; set; }
        public  bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        //public  Image Scale(Image imgPhoto)
        //{
        //    float sourceWidth = imgPhoto.Width;
        //    float sourceHeight = imgPhoto.Height;
        //    float destHeight = 0;
        //    float destWidth = 0;
        //    int sourceX = 0;
        //    int sourceY = 0;
        //    int destX = 0;
        //    int destY = 0;

        //    // force resize, might distort image
        //    if (Width != 0 && Height != 0)
        //    {
        //        destWidth = Width;
        //        destHeight = Height;
        //    }
        //    // change size proportially depending on width or height
        //    else if (Height != 0)
        //    {
        //        destWidth = (float)(Height * sourceWidth) / sourceHeight;
        //        destHeight = Height;
        //    }
        //    else
        //    {
        //        destWidth = Width;
        //        destHeight = (float)(sourceHeight * Width / sourceWidth);
        //    }

        //    Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
        //                                PixelFormat.Format32bppPArgb);
        //    bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        //    Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    grPhoto.DrawImage(imgPhoto,
        //        new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
        //        new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
        //        GraphicsUnit.Pixel);

        //    grPhoto.Dispose();

        //    return bmPhoto;
        //}
    }
    public class ImageResult
    {
        public bool Success { get; set; }
        public string ImageName { get; set; }
        public string ErrorMessage { get; set; }

    }
}