using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundLocalImageService : LocalImageProviderBase, IBackgroundImageService
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string ImageNameFormat { get { return "bg-*.jpg"; } }
    }
}
