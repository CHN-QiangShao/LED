using LED.DLL;

namespace LED.Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        // 1、创建 WebApplication 构建器，用于后续添加服务和启用中间件
        var builder = WebApplication.CreateBuilder(args);

        // 2、添加服务到容器中
        builder.Services.AddControllers();          // 添加 控制器服务，          用于 MVC 架构
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer(); // 添加 API 端点元数据服务，  用于支持 swagger
        builder.Services.AddSwaggerGen();           // 添加 swagger 文档生成服务，用于生成 API 文档

        //builder.Services.AddHostedService<ShowContent>();   // 添加后台服务
        // 需要添加单例实例服务，否则会报错 System.InvalidOperationException: Unable to resolve service for type 'LED.DLL.LedValue' while attempting to activate 'LED.Web.API.Controllers.LedController'
        // 表明在尝试激活 LedController 时，ASP.NET Core 的依赖注入容器无法解析 LED.DLL.LedValue 类型的服务
        // 即在依赖注入容器在创建 LedController 实例时，找不到用于注入 LedValue 参数的合适服务
        builder.Services.AddSingleton<LedValue>();  // 添加 单例实例服务
        // 可以显式设置 https 端口。这样即使 launchSettings.json 没有设置，中间件也能使用指定的端口
        //builder.Services.AddHttpsRedirection(options => {
        //    options.HttpsPort = 7836;
        //});

        // 3、构建 web 应用程序
        var app = builder.Build();

        // 4、启用 http 请求处理管道中间件（中间件的启用是有顺序的）
        if (app.Environment.IsDevelopment())
        {
            // 开发环境专用配置，不适用于生产/发布环境
            app.UseSwagger();       // 启用 swagger json 文档端点中间件
            app.UseSwaggerUI(c => { // 启用 swagger UI 交互界面中间件，默认路径为 /swagger
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LED API");   // 指定 Swagger UI 加载的 OpenAPI 规范文件路径，设置网页右上角 Select a definition 下拉项
            });
        }

        //app.UseHttpsRedirection();  // 启用 https 重定向中间件， 强制将所有 http 请求重定向到 https，安全访问链接
        app.UseAuthorization();     // 启用 授权中间件，         身份验证/授权，授权在重定向之前启用
        app.MapControllers();       // 启用 映射控制器路由中间件，自动发现 Controller 及其路由

        // 5、阻塞调用应用程序，启动 Web 服务器并开始监听请求，会持续运行直到应用关闭。
        app.Run();
    }
}
