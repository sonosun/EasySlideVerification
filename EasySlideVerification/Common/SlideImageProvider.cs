using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySlideVerification.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideImageProvider
    {
        List<byte[]> Images = new List<byte[]>();

        private SlideImageProvider()
        {
            this.Load();
        }

        public static readonly SlideImageProvider Instance = new SlideImageProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetRandomOne()
        {
            Random random = new Random();
            int index = random.Next(this.Images.Count);
            return this.Images[index];
        }

        /// <summary>
        /// 
        /// </summary>
        private void Load()
        {
            string workDir = Directory.GetCurrentDirectory();
            string imageDir = $"{workDir}\\App_Data\\Images\\Slide";
            string[] files = Directory.GetFiles(imageDir, "slide-*.png");

            for (int i = 0; i < files.Length; i++)
            {
                using (Stream stream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                {
                    byte[] image = new byte[stream.Length];
                    stream.Read(image, 0, image.Length);
                    this.Images.Add(image);
                }
            }
        }
    }
}
