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

namespace EasySlideVerification
{
    /// <summary>
    /// 
    /// </summary>
    public class SlideVerifyService : ISlideVerifyService
    {
        IHttpClientFactory httpClientFactory;
        ISlideVerificationStore store;

        public SlideVerifyService(
            IHttpClientFactory httpClientFactory,
            ISlideVerificationStore store)
        {
            this.httpClientFactory = httpClientFactory;
            this.store = store;
        }

        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationInfo Create()
        {
            SlideVerificationParam param = new SlideVerificationParam()
            {
                BackgroundImage = BackgroundImageProvider.Instance.GetRandomOne(),
                SlideImage = SlideImageProvider.Instance.GetRandomOne(),
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
        public bool Validate(string key, int x, int y)
        {
            var data = this.store.Get(key);
            if (data == null) return false;

            int accept = SlideVerificationOptions.Default.AcceptableDeviation;
            bool success = x > data.PositionX - accept && x < data.PositionX + accept
                        && y > data.PositionY - accept && y < data.PositionY + accept;
            //验证成功，移除缓存
            if (success)
            {
                this.store.Remove(key);
            }

            return success;
        }
    }
}
