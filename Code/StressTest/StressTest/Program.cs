using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Serilog;

class Program
{
    private static readonly Random random = new Random(); // 静态随机数生成器（线程安全）

    /// <summary>
    /// 异步主入口方法
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static async Task Main(string[] args)
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;                    // 获取应用程序基础目录
        string logFilePath  = Path.Combine(appDirectory, "../../../Logs", "Log_.log");  // 构建日志文件路径（向上三级目录中的 Logs 文件夹）
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);                 // 确保日志目录存在
        Console.WriteLine(appDirectory);                                                // 打印基础目录路径

        // 1、配置 Serilog 日志记录器，日志目录结构如下：
        //  应用程序根目录/
        //  └── ../../../(向上三级目录)/
        //      └── Logs/
        //          ├── Log_当天日期.log # 正在写入的日志
        //          ├── Log_20250320.log # 2025 年 3 月 20 日日志
        //          ├── Log_20250321.log # 2025 年 3 月 21 日日志
        //          └── ...              # 按日期递增，格式为 Log_YYYYMMDD.log（每天 0:00 自动创建新文件）
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()                                  // 使用 Serilog 实现双输出（控制台 + 文件）
            .WriteTo.File(logFilePath,                          // 日志文件路径
                        rollingInterval: RollingInterval.Day,   // 日志文件按天滚动分割
                        retainedFileCountLimit: 30)             // 默认无限期保留，此处保留最近 30 天日志
            .CreateLogger();

        try
        {
            string url = "http://127.0.0.1:7836/API/LED/Display";   // API 端点地址
            int numberOfRequests = 50;                              // 请求总数

            // 2、创建 HttpClient 实例（使用 using 保证资源释放）
            using (HttpClient client = new HttpClient())
            {
                // 循环发送指定数量的请求
                for (int i = 0; i < numberOfRequests; i++)
                {
                    // 3、创建请求体
                    var requestBody = new
                    {
                        taskType        = GetRandomTaskType(),              // 任务类型
                        endPickupName   = GenerateRandomString(10, true),   // 端拾器名
                        endPickupCode   = GenerateRandomString(6, false),   // 端拾器码
                        location        = "A-1-2-5-10"                      // 仓库位置
                    };
                    //// 在 HttpClient 初始化时配置超时和重试策略
                    //var httpClient = new HttpClient(new SocketsHttpHandler
                    //{
                    //    PooledConnectionLifetime = TimeSpan.FromMinutes(5) // 连接存活时间
                    //})
                    //{
                    //    Timeout = TimeSpan.FromSeconds(10)
                    //};
                    // 添加序列化选项配置
                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 禁用字符转义
                        WriteIndented = true                                   // 可选：格式化输出
                    };

                    // var json = JsonSerializer.Serialize(requestBody); 序列化为 json 字符串，默认将非 ASCII 字符（比如中文字符）转义成 \uXXXX 形式
                    var json                        = JsonSerializer.Serialize(requestBody, options);               // 序列化为 json 字符串，
                    HttpContent content             = new StringContent(json, Encoding.UTF8, "application/json");   // 创建 HTTP 内容（UTF8 编码，json 格式） 
                    HttpResponseMessage response    = await client.PostAsync(url, content);                         // 发送 POST 请求并等待响应

                    /* 问题：程序第一次运行时可能会返回 422 UnprocessableEntity 错误，后续请求则正常返回 OK
                     * 可能原因 1 ：第一个请求中的某些数据不符合服务器验证
                     * 解决：在 LEDController.cs 让 API 端点再调用一次动态库的非托管方法
                     * 可能原因 2 ：服务端缓存、数据库连接尚未就绪、缓存未加载完成、资源初始化未完成
                     * 解决：当程序结束前，删除所有字段的值
                     * 可能原因 3 ：服务端可能通过 429 Too Many Requests 限流，但错误处理不当返回 422
                     * 可能原因 4 ：HttpClient 首次建立连接时可能需要额外握手时间，导致服务端超时
                     */
                    Log.Information($"Request {i + 1}:\n{json}");              // 记录请求体
                    Log.Information($"Request {i + 1}:{response.StatusCode}"); // 记录请求状态日志
                    //Log.Information($"Request {i + 1} Headers: {response.RequestMessage?.Headers}");// 在发送请求后添加请求内容日志（临时调试）

                    await Task.Delay(5000); // 等待 5 秒进行下次请求
                }
            }
        }
        // 4、处理 HTTP 请求异常
        catch (HttpRequestException ex)
        {
            Log.Error(ex, "HTTP 请求发送失败");
        }
        // 处理其他类型异常
        catch (Exception ex)
        {
            Log.Error(ex, "程序运行中发生异常");
        }
        finally
        {
            Log.CloseAndFlush(); // 确保日志缓冲区被刷新
        }
    }

    /// <summary>
    /// 获取随机任务类型
    /// </summary>
    /// <returns>随机选择的仓库任务类型字符串</returns>
    static string GetRandomTaskType()
    {
        string[] taskTypes = { "入库任务", "出库任务", "移库任务" };
        return taskTypes[random.Next(taskTypes.Length)];
    }

    /// <summary>
    /// 生成指定格式的随机字符串
    /// </summary>
    /// <param name="length">目标字符串总长度</param>
    /// <param name="includeDash">是否包含分隔符</param>
    /// <returns>生成的随机字符串</returns>
    static string GenerateRandomString(int length, bool includeDash)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // 可用字符集（大写字母+数字）
        StringBuilder sb = new StringBuilder();

        // 按字符位置构建字符串
        for (int i = 0; i < length; i++)
        {
            // 在第四个位置插入分隔符
            if (includeDash && i == 3)
            {
                sb.Append('-');
            }
            else
            {
                sb.Append(chars[random.Next(chars.Length)]); // 从字符集中随机选取字符
            }
        }
        return sb.ToString();
    }
}