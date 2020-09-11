using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideLocalImageService : LocalImageProviderBase, IImageService
    {
        protected override string ImageNameFormat { get { return "slide-*.png"; } }
    }
}
