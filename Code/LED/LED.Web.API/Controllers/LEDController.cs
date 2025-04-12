using Microsoft.AspNetCore.Mvc;
using LED.DLL;

namespace LED.Web.API.Controllers;

[ApiController]             // 定义一个 API 专用控制器，启用 API 特定行为
[Route("[controller]")]     // 设置路由模板，使用控制器名称(去掉"Controller"后缀)，实际路由路径为 /LED
public class LEDController : ControllerBase
{
    private readonly ILogger<LEDController> _logger;

    /// <summary>
    /// 构造函数：依赖注入日志记录器
    /// </summary>
    /// <param name="logger">日志</param>
    public LEDController(ILogger<LEDController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name = "LED")]     // 定义 GET 请求端点，并命名路由为"LED"
    public IEnumerable<LedDto> Get()
    {
        return new LedDto[]
        {
            new LedDto
            {
                taskType = TaskType.inbound,
                endPickupName = "端拾器小车 A",
                endPickupCode = "Car A",
                location = "A-1-1-1-1"
            }
        };
    }
}
