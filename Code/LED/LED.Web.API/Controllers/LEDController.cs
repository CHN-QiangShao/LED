using System.Text;
using Microsoft.AspNetCore.Mvc;
using LED.DLL;

namespace LED.Web.API.Controllers;

/// <summary>
/// LED 控制器的类定义，继承无视图支持的纯 API 控制器 ControllerBase，并带有 ApiController 和 Route 属性
/// </summary>
[ApiController]                 // 启用 API 控制器属性，自动处理模型验证错误（400响应），后续无需启用 400 属性
[Route("API/[Controller]")]		// 启用路由模板属性，实际路由路径为 API/LED (控制器名去掉 "Controller" 后缀)
public class LedController : ControllerBase
{
    // 警告：在退出构造函数时，不可为 null 的字段必须包含非 null 值
    // 原因：C# 8.0 新增可空引用类型特性，项目默认启用可为空引用类型 <Nullable>enable</Nullable>，此时引用类型的变量默认不可为 null
    // 即：如果声明一个字段而不加问号，编译器会认为这个字段在构造函数完成之前必须被赋予一个非 null 的值。否则在构造函数结束时，如果这个字段没有被初始化，就会触发警告，因为编译器认为它可能为 null，违反不可为 null 的约定
    // 解决方案 1：如果字段确实可能为 null，添加 ? 修饰符允许字段为 null
    // 解决方案 2：如果字段确实不能为 null，使用构造函数中确保字段被赋值
    private readonly LedValue _ledValue;
    private readonly ILogger<LedController>? _logger;

    /// <summary>
    /// 构造函数
    /// <br>readonly 字段只能在构造函数中初始化</br>
    /// </summary>
    /// <param name="ledValue">LedValue 实例</param>
    public LedController(LedValue ledValue/*, ILogger<LedController> logger*/)
    {
        _ledValue = ledValue;   // 没有声明 ?，确保 ledValue 非 null
        //_logger = logger;     // 因为声明 ?，允许 logger 为 null
    }

    /// <summary>
    /// API 端点定义：定义并遍历内码文字数组，调用 SendInternalText_Net()，根据返回的状态码判断是否成功，并记录结果
    /// </summary>
    /// <param name="requestBody">RequestBody 类的 json 格式的请求体</param>
    /// <returns>ResponseBody 类的字段</returns>
    [HttpPost("Display")]                                           // 启用 HttpPost 属性，              定义 POST 端点为 ip:port/API/Led/Display
    [ProducesResponseType(typeof(ResponseBody), 200)]               // 启用 ProducesResponseType 属性，  定义成功响应类型：200+ResponseBody 自定义响应体
    [ProducesResponseType(typeof(ValidationProblemDetails), 422)]   // 启用 ProducesResponseType 属性，  定义失败响应类型：422+ValidationProblemDetails 标准响应体
    [ProducesResponseType(typeof(ProblemDetails), 500)]             // 启用 ProducesResponseType 属性，  定义失败响应类型：500+ProblemDetails 标准响应体
    public IActionResult Display([FromBody] RequestBody requestBody)
    {
        // 从 WMS 获取到的数据源，如果数据为空/格式错误，会根据模型绑定，自动 400 错误，不会进入接口
        string[] showContent = {
                requestBody.taskType.ToString(),
                requestBody.endPickupName,
                requestBody.endPickupCode,
                requestBody.location
            };
        bool isLongText;
        int ret;
        var retDetails = new List<string>();

        // LED 显示逻辑
        for (int i = 0; i < showContent.Length; i++)
        {
            // 如果超过屏幕单行显示字符就设置为从右向左移动滑动显示，否则为立即显示
            isLongText = Encoding.GetEncoding("GBK").GetByteCount(showContent[i]) > _ledValue.longText;
            try
            {
                ret = QYLED_DLL.SendInternalText_Net(
                    showContent[i],
                    _ledValue.controlCardIP,
                    _ledValue.UDP,
                    _ledValue.areaWidth,
                    _ledValue.areaHeight,
                    _ledValue.uid + i,
                    _ledValue.singleFundamentalColor,
                    isLongText ? _ledValue.moveFromRightToLeft : _ledValue.showNow,
                    _ledValue.showSpeedSlowest,
                    _ledValue.cyclicShow,
                    _ledValue.red,
                    _ledValue.songFont,
                    _ledValue.twelveSquared,
                    _ledValue.updatedThisImmediately,
                    _ledValue.saveFalse,
                    _ledValue.nonRotate);
            }
            catch (Exception ex)
            {
                if (ex is BadImageFormatException)
                {
                    retDetails = new List<string> {
                            "原因：无法加载 x86 的 dll 到 x64 程序中",
                            "解决：根据需求，鼠标右击控制台引用程序→属性→生成→选择正确的目标平台" };
                }
                else if (ex is DllNotFoundException)
                {
                    retDetails = new List<string> {
                            "原因：外部动态库不存在 bin/Release/net8.0 目录",
                            "解决：请把动态库复制到 bin/Release/net8.0 目录"};
                }
                else
                {
                    retDetails = new List<string> { "未知异常，可能是内存不足" };
                }
                // BadImageFormatException 和 DllNotFoundException 属于服务器配置或部署问题
                // 400  Bad Request             客户端错误   请求本身存在语法错误或无效参数
                // 422  Unprocessable Entity    客户端错误   请求本身数据语法正确但语义错误，导致业务逻辑校验失败
                // 500  Internal Server Error   服务端错误   服务器在处理请求时发生意外错误
                return StatusCode(500, new ProblemDetails
                {
                    Detail = string.Join("；", retDetails),
                    Instance = $"{ex}"
                });
            }
            retDetails.Add(ret == 0 ? $"{showContent[i]} 下发成功" : $"{showContent[i]} 下发失败"); // 记录每个信息的发送结果
            // 只要任意一次调用非托管方法失败，立即返回 422 错误
            if (ret != 0)
            {
                return StatusCode(422, new ValidationProblemDetails 
                {
                    Detail = string.Join("；", retDetails),
                    Errors = new Dictionary<string, string[]>
                    {
                        { $"第 {i + 1} 行数据", new string[] { $"{retDetails[i]}" } }
                    }
                });
            }
        }
        //return Ok("所有信息已成功发送至 LED 显示屏");
        return StatusCode(200, new ResponseBody
        {
            // LINQ 查询：检查 results 列表中是否存在任何包含【失败】字样的字符串。
            // Any() 方法会遍历集合中的每个元素，只要找到一个满足条件的元素，Any() 就会返回 true。
            isSuccess = !retDetails.Any(r => r.Contains("失败")),
            details = retDetails
        });
    }
}
