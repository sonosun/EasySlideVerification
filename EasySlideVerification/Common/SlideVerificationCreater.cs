using EasySlideVerification.Common;
using EasySlideVerification.Model;
using EasySlideVerification.Store;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;

namespace EasySlideVerification.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationCreater
    {
        /// <summary>
        /// 
        /// </summary>
        private SlideVerificationCreater() { }

        public static readonly SlideVerificationCreater Instance = new SlideVerificationCreater();

        Random random = new Random();

        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationInfo Create(SlideVerificationParam param)
        {
            SlideVerificationInfo result = new SlideVerificationInfo();
            result.Key = Guid.NewGuid().ToString("N");

            using (Image coverImage = GetImage(param.SlideImage))
            using (Image sourceImage = GetImage(param.BackgroundImage))
            {
                int coverWidth = coverImage.Width;
                int coverHeight = coverImage.Height;

                int offsetX = random.Next(coverImage.Width, sourceImage.Width - coverImage.Width - param.Edge);
                int offsetY = random.Next(coverImage.Height, sourceImage.Height - coverImage.Height);

                result.OffsetX = offsetX;
                result.OffsetY = offsetY;

                //滑块图片
                using (Image slideImage = CaptureImage(sourceImage, offsetX, offsetY, coverWidth, coverHeight))
                {
                    using (Graphics graphics = Graphics.FromImage(slideImage))
                    {
                        graphics.DrawImage(coverImage, 0, 0, coverWidth, coverHeight);
                        graphics.Save();
                    }

                    result.SlideImage = ImageToByteArr(slideImage, ImageFormat.Png);
                }

                //背景图片
                using (Graphics graphics = Graphics.FromImage(sourceImage))
                {
                    graphics.DrawImage(coverImage, offsetX, offsetY, coverWidth, coverHeight);
                    graphics.Save();
                }

                //画混淆图
                DrawMix(sourceImage, coverImage, param.MixedCount);

                result.BackgroudImage = ImageToByteArr(sourceImage, ImageFormat.Jpeg);
            }

            return result;
        }

        /// <summary>
        /// 画混淆图
        /// </summary>
        /// <param name="mixImageByteArr"></param>
        /// <param name="sourceImage"></param>
        private void DrawMix(byte[] mixImageByteArr, Image sourceImage, int count)
        {
            if (mixImageByteArr != null)
            {
                using (Image mixImage = GetImage(mixImageByteArr))
                {
                    DrawMix(sourceImage, mixImage, count);
                }
            }
        }

        private void DrawMix(Image sourceImage, Image mixImage, int count)
        {
            using (Graphics graphics = Graphics.FromImage(sourceImage))
            {
                int coverWidth = mixImage.Width;
                int coverHeight = mixImage.Height;
                for (int i = 0; i < count; i++)
                {
                    int offsetX = random.Next(mixImage.Width, sourceImage.Width - mixImage.Width);
                    int offsetY = random.Next(mixImage.Height, sourceImage.Height - mixImage.Height);

                    graphics.DrawImage(mixImage, offsetX, offsetY, coverWidth, coverHeight);
                    graphics.Save();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageByteArr"></param>
        /// <returns></returns>
        protected Image GetImage(byte[] imageByteArr)
        {
            using (var stream = new MemoryStream(imageByteArr))
            {
                return Bitmap.FromStream(stream);
            }
        }

        /// <summary>
        /// Image转Byte数组
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <returns></returns>
        protected byte[] ImageToByteArr(Image sourceImage, ImageFormat format)
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
        protected string ImageToBase64(Image sourceImage, ImageFormat format)
        {
            byte[] buffer = ImageToByteArr(sourceImage, format);
            StringBuilder result = new StringBuilder();
            result.Append($"data:image/{format};base64,");
            result.Append(Convert.ToBase64String(buffer));
            return result.ToString();
        }

        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="fromImage">来源图片</param>        
        /// <param name="offsetX">从偏移X坐标位置开始截取</param>
        /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
        /// <param name="width">保存图片的宽度</param>
        /// <param name="height">保存图片的高度</param>
        /// <returns></returns>
        private Image CaptureImage(Image fromImage, int offsetX, int offsetY, int width, int height)
        {
            //创建新图位图
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                //截取原图相应区域写入作图区
                graphic.DrawImage(fromImage, 0, 0, new Rectangle(offsetX, offsetY, width + 6, height + 6), GraphicsUnit.Pixel);
                //graphic.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);
                graphic.Save();
                return bitmap;
            }
        }
    }
}
