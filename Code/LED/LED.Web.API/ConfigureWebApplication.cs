using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;
using LED.DLL;

namespace LED.Web.API;

public static class ConfigureWebApplication
{
    /// <summary>
    /// 添加服务到容器中
    /// </summary>
    public static void AddServicesToContainer(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<LedValue>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // 1、核心 MVC 服务
        services.AddControllers();          // 添加 控制器服务，          用于 MVC 架构
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer(); // 添加 API 端点元数据服务，  用于支持 swagger
        services.AddSwaggerGen();           // 添加 swagger 文档生成服务，用于生成 API 文档

        /* 2、业务相关服务
         * 报错：System.InvalidOperationException: Unable to resolve service for type 'LED.DLL.LedValue' while attempting to activate 'LED.Web.API.Controllers.LedController'
         * 原因：在尝试激活 LedController 时，ASP.NET Core 的依赖注入容器无法解析 LED.DLL.LedValue 类型的服务，即在依赖注入容器在创建 LedController 实例时，找不到用于注入 LedValue 参数的合适服务
         * 解决：需要添加单例实例服务
         */
        services.AddSingleton<LedValue>();  // 添加 单例实例服务
        
        // 可以显式设置 https 端口。这样即使 launchSettings.json 没有设置，中间件也能使用指定的端口
        //services.AddHttpsRedirection(options => {
        //    options.HttpsPort = 7836;
        //});
        
        // 3、编码支持
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // 注册中文编码支持，增加 GBK 字符编码规则
    }

    /// <summary>
    /// 启用 http 请求处理管道中间件
    /// <br>中间件的启用是有顺序的</br>
    /// </summary>
    public static void UseMiddleware(WebApplication app)
    {
        // 1、开发工具中间件
        ConfigureSwagger(app);

        // 2、安全中间件
        app.UseAuthorization(); // 启用 授权中间件，         身份验证/授权，授权在重定向之前启用

        // 3、路由配置
        app.MapControllers();   // 启用 映射控制器路由中间件，自动发现 Controller 及其路由
    }

    /// <summary>
    /// 专用 Swagger 配置
    /// </summary>
    private static void ConfigureSwagger(WebApplication app)
    {
        app.UseSwagger();       // 启用 swagger json 文档端点中间件
        app.UseSwaggerUI(c => { // 启用 swagger UI 交互界面中间件，默认路径为 /swagger
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "LED API v1");    // 指定 Swagger UI 加载的 OpenAPI 规范文件路径，设置网页右上角 Select a definition 下拉项
            c.DocExpansion(DocExpansion.List);                              // 默认展开模式
        });
    }
}
