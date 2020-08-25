using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasySlideVerificationDemoServer.Models;
using EasySlideVerification;
using EasySlideVerification.Model;
using EasySlideVerification.Common;
using System.Drawing.Imaging;

namespace EasySlideVerificationDemoServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        ISlideVerifyService verifyService;

        public HomeController(ISlideVerifyService verifyService)
        {
            this.verifyService = verifyService;
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult<SlideVerificationPlainInfo> GetVerification()
        {
            var data = this.verifyService.Create();
            var result = ConvertToBase64PlainInfo(data);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult<bool> Verify(VerifyParam param)
        {
            var result = this.verifyService.Validate(param.key, param.OffsetX, param.OffsetY);
            return result;
        }


        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationPlainInfo ConvertToBase64PlainInfo(SlideVerificationInfo data)
        {
            SlideVerificationPlainInfo result = SlideVerificationPlainInfo.From(data);
            if (result != null)
            {
                result.BackgroundImg = ImageUtil.ImageToBase64(data.BackgroundImg, ImageFormat.Jpeg);
                result.SlideImg = ImageUtil.ImageToBase64(data.SlideImg, ImageFormat.Png);
                //PositionX 不能输出到前端
                result.PositionX = 0;
            };

            return result;
        }

        ///// <summary>
        ///// 创建图片滑动数据
        ///// </summary>
        //public SlideVerificationPlainInfo ConvertToPlainInfo(SlideVerificationInfo data)
        //{
        //    SlideVerificationPlainInfo result = SlideVerificationPlainInfo.From(data);
        //    if (result != null)
        //    {
        //        result.BackgroundImg = $"/Home/GetBackgroundImage?key={data.Key}";
        //        result.SlideImg = $"/Home/GetSlideImage?key={data.Key}";
        //        //PositionX 不能输出到前端
        //        result.PositionX = 0;
        //    };

        //    return result;
        //}

        //public IActionResult GetBackgroundImage(string key)
        //{
        //    var img = this.verifyService.GetBackgroundImage(key);
        //    return this.File(img, "image/jpeg");
        //}

        //public IActionResult GetSlideImage(string key)
        //{
        //    var img = this.verifyService.GetSlideImage(key);
        //    return this.File(img, "image/png");
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    public class VerifyParam
    {
        public string key { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }
}
