using System;
using System.Collections.Generic;
using System.Text;

namespace EasySlideVerification.ImageProvider
{
    /// <summary>
    /// 
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        byte[] GetRandomOne();

        /// <summary>
        /// 
        /// </summary>
        void Load();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISlideImageService : IImageService { }

    /// <summary>
    /// 
    /// </summary>
    public interface IBackgroundImageService : IImageService { }
}
