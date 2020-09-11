using EasySlideVerification.Common;
using EasySlideVerification.ImageProvider;
using EasySlideVerification.Model;
using EasySlideVerification.Store;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;

namespace EasySlideVerification
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerifyService : ISlideVerifyService
    {
        IHttpClientFactory httpClientFactory;
        ISlideVerificationStore store;
        IBackgroundImageService backgrouondImageService;
        ISlideImageService slideImageService;

        public SlideVerifyService(
            IHttpClientFactory httpClientFactory,
            ISlideVerificationStore store,
            IBackgroundImageService backgrouondImageService,
            ISlideImageService slideImageService)
        {
            this.httpClientFactory = httpClientFactory;
            this.store = store;
            this.backgrouondImageService = backgrouondImageService;
            this.slideImageService = slideImageService;
        }

        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationInfo Create()
        {
            SlideVerificationParam param = new SlideVerificationParam()
            {
                BackgroundImage = this.backgrouondImageService.GetRandomOne(),
                SlideImage = this.slideImageService.GetRandomOne(),
                Edge = SlideVerificationOptions.Default.Edge,
                MixedCount = SlideVerificationOptions.Default.MixedCount,
            };
            SlideVerificationInfo result = SlideVerificationCreater.Instance.Create(param);

            this.store.Add(result, SlideVerificationOptions.Default.Expire);

            return result;
        }

        /// <summary>
        /// 验证结果
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Validate(VerifyParam param)
        {
            var data = this.store.Get(param.Key);
            if (data == null) return false;

            int accept = SlideVerificationOptions.Default.AcceptableDeviation;
            bool success = param.PositionX > data.PositionX - accept && param.PositionX < data.PositionX + accept
                        && param.PositionY > data.PositionY - accept && param.PositionY < data.PositionY + accept;
            //验证成功，移除缓存
            if (success && param.RemoveIfSuccess)
            {
                this.store.Remove(param.Key);
            }

            return success;
        }
    }
}
