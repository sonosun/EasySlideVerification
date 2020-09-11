# EasySlideVerification
滑动验证，拼图验证

## 简介：

.net core平台下的滑动验证，拼图验证模块。可参考示例：http://39.99.144.98:8080/ 

服务端生成校验图片，前端展示，验证用户操作结果。

校验数据存储支持`本地内存`和`redis分布式缓存`两种模式，开发调试或单机部署时，可使用本地内存模式，项目以分布式集群形式部署时，需开启redis缓存模式。


提供了两个Demo:

`EasySlideVerificationDemoServer`是基于.net core mvc 实现。服务端生成校验图片，view页面展示图片，并验证用户操作。

`EasySlideVerificationDemo`是通过vue实现的前端校验示例，依赖于`EasySlideVerificationDemoServer`提供的后端服务。

两种方式的Demo中都提供了基础的js组件支持，可以根据实际项目情况决定采用哪一种。


---

## 基本使用：

### 第一步：添加引用

通过 `NuGet` 包管理器，搜索并添加依赖包 `EasySlideVerification`

### 第二步：注册EasySlideVerification服务

在 `Startup.cs` 中注册图形校验码模块

``` csharp
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //注册拼图校验
            services.AddSlideVerification();

            //此处忽略其他代码
        }
        //此处忽略其他代码
    }
``` 
            
### 第三步：生成校验码以及校验

`ISlideVerifyService` 接口是拼图校验图片生成及校验的接口。 

接口有两个方法：

        /// <summary>
        /// 创建图片滑动数据(图片以byte数组格式返回)
        /// </summary>
        SlideVerificationInfo Create();

        /// <summary>
        /// 验证结果
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Validate(VerifyParam param); 

#### 接口使用：

由于 .net core 支持依赖注入，所以不需要关心接口的实现类，只需注入即可。
(目前默认实现类是 `SlideVerifyService`)

``` csharp
    
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
``` 
### 第四步：将背景图片及滑动图片放置到项目目录下

复制示例项目中的 `App_Data` 文件夹到你的项目中，其中包含背景图片和拼图图片，也可以添加或替换自己的图片，图片命名规则为：

背景图片：bg-xxx.jpg

滑动图片：slide-xxx.png

使用自定义图片需留意两个图片的图片格式。

`注意`项目部署时也需要将此文件夹一同发布。



### 第五步：前端展示

参考示例项目：`EasySlideVerificationDemoServer`
引用滑动拼图的前端 `js` 类库，目录如下：

    wwwroot
        + slider
            slider.js
            slider.css

示例页面为：

    Views
        + Home
            Index.cshtml



详细代码可参考 Demo ：`EasySlideVerificationDemoServer`

鉴于目前VUE的广泛应用，我在`EasySlideVerificationDemo`示例中，提供了一个基于VUE的前端校验实现。
核心代码参考下面三个文件：

    src
        + api
            VerificationDemoApi.js
        + components
            Home.vue
        + slider
            slider.vue

其中，Home.vue是展示页面，slider.vue是滑动组件，在实际项目开发时，引入此组件，提供图片数据获取逻辑和校验逻辑即可。

---

## 进阶使用：

### 使用`redis`缓存模式
修改 `Startup.cs` 中校验码模块注册方式，增加Redis服务配置

``` csharp
        public void ConfigureServices(IServiceCollection services)
        {
            //注册滑动校验，使用Redis缓存
            services.AddSlideVerification(
                redisOptions =>
                {
                    redisOptions.Connection = "127.0.0.1:6379";
                    redisOptions.DatabaseIndex = 0;
                    redisOptions.KeyPrefix = "slide:";
                },
                options => {
                });

        }
```
说明：
`DatabaseIndex` 是使用的redis数据库索引，值为0~15，默认为0。
`KeyPrefix` 是redis缓存的key前缀，起到对缓存项分组的作用。可以为空，但建议使用 `slide:`

### 配置校验码生成细节
通过对`options`进行赋值，可以定制校验码
``` csharp
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

```

## Linux或Docker部署可能遇到的问题：


```
Exception：
    An unhandled exception has occurred while executing the request.
    System.TypeInitializationException: The type initializer for 'Gdip' threw an exception. 
    ---> System.DllNotFoundException: Unable to load shared library 'libdl' or one of its dependencies. In order to help diagnose loading problems, consider setting the LD_DEBUG environment variable: liblibdl: cannot open shared object file: No such file or directory
```
这是由于项目中使用了System.Drawing.Common库，在Linux环境下，需要安装 `libdl.so`和`libgdiplus.so` 并建立文件链接，下面的命令可以解决Linux环境部署的问题：
```
ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so

apt-get update
apt-get install libgdiplus -y && ln -s libgdiplus.so gdiplus.dll
```

# 结语

拼图校验是我之前项目中的一个功能，由于具体独立于实际业务的特点，所以单独提取作为一个组件，这里将代码重构，开源出来，并提供了前端展示的基础代码，让整个校验逻辑变得完整。

希望开发人员遇到类似需求时，能够有所帮助，让编程变得更简单，更高效，让软件开发成为一件快乐的事情。


我的口号是：`让天下没有难写的代码。`