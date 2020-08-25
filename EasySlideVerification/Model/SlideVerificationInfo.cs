using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerificationInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 背景图片（byte数组图片数据）
        /// </summary>
        public byte[] BackgroundImg { get; set; }

        /// <summary>
        /// 滑块图片（byte数组图片数据）
        /// </summary>
        public byte[] SlideImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BgHeight { get; set; }

        /// <summary>
        /// 
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
    }
}
