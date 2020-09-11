using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundLocalImageService : LocalImageProviderBase, IImageService
    {
        protected override string ImageNameFormat { get { return "bg-*.jpg"; } }
    }
}
