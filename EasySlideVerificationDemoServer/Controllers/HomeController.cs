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
        /// 创建图片滑动数据
        /// </summary>
        /// <returns></returns>
        public ActionResult<SlideVerificationPlainInfo> GetVerification()
        {
            var data = this.verifyService.Create();
            var result = ConvertToBase64PlainInfo(data);
            return result;
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult<bool> Verify([FromBody]VerifyParam param)
        {
            return this.verifyService.Validate(param);
        }
        
        /// <summary>
        /// 图片由byte[]转换为base64字符串
        /// </summary>
        private SlideVerificationPlainInfo ConvertToBase64PlainInfo(SlideVerificationInfo data)
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
    }

}
