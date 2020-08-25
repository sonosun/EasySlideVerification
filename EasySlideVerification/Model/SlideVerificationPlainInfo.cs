using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationPlainInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackgroundImg { get; set; }

        /// <summary>
        /// 滑块图片
        /// </summary>
        public string SlideImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// 背景图片高度
        /// </summary>
        public int BgHeight { get; set; }

        /// <summary>
        /// 背景图片宽度
        /// </summary>
        public int BgWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SlideHeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SlideWidth { get; set; }

        /// <summary>
        /// 对象拷贝
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SlideVerificationPlainInfo From(SlideVerificationInfo data)
        {
            SlideVerificationPlainInfo result = null;
            if (data != null)
            {
                result = new SlideVerificationPlainInfo()
                {
                    Key = data.Key,
                    PositionX = data.PositionX,
                    PositionY = data.PositionY,
                    BgHeight = data.BgHeight,
                    BgWidth = data.BgWidth,
                    SlideHeight = data.SlideHeight,
                    SlideWidth = data.SlideWidth,
                };
            }
            return result;
        }
    }
}
