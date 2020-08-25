using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace EasySlideVerification.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageUtil
    {
        /// <summary>
        /// byte数组转Image
        /// </summary>
        /// <param name="imageByteArr"></param>
        /// <returns></returns>
        public static Bitmap GetImage(byte[] imageByteArr)
        {
            using (var stream = new MemoryStream(imageByteArr))
            {
                return Bitmap.FromStream(stream) as Bitmap;
            }
        }

        /// <summary>
        /// Image转Byte数组
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArr(Image sourceImage, ImageFormat format)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                sourceImage.Save(stream, format);
                byte[] buffer = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();

                return buffer;
            }
        }

        /// <summary>
        /// Image转base64
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <returns></returns>
        public static string ImageToBase64(Image sourceImage, ImageFormat format)
        {
            byte[] buffer = ImageToByteArr(sourceImage, format);
            StringBuilder result = new StringBuilder();
            result.Append($"data:image/{format};base64,");
            result.Append(Convert.ToBase64String(buffer));
            return result.ToString();
        }

        /// <summary>
        /// Image转base64
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <returns></returns>
        public static string ImageToBase64(byte[] buffer, ImageFormat format)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"data:image/{format};base64,");
            result.Append(Convert.ToBase64String(buffer));
            return result.ToString();
        }
    }
}
