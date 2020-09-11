using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideLocalImageService : LocalImageProviderBase, ISlideImageService
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string ImageNameFormat { get { return "slide-*.png"; } }
    }
}
