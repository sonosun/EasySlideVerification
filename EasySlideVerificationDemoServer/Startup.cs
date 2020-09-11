using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasySlideVerification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasySlideVerificationDemoServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册滑动校验
            services.AddSlideVerification(options=> {
                //可接受的误差范围
                options.AcceptableDeviation = 5;
                //右边框距离(防止由于太靠近右侧，自定义的滑动按钮无法到达）
                options.Edge = 0;
                //数据过期时间
                options.Expire = new TimeSpan(0, 5, 0);
                //混淆点数量
                options.MixedCount = 0;
            });

            ////注册滑动校验，使用Redis缓存
            //services.AddSlideVerification(
            //    redisOptions =>
            //    {
            //        redisOptions.Connection = "127.0.0.1:6379";
            //        redisOptions.DatabaseIndex = 0;
            //        redisOptions.KeyPrefix = "slide:";
            //    },
            //    options => {
            //    });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(policy =>
            {
                policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
