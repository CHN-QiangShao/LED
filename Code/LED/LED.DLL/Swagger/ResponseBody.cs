namespace LED.DLL;

/// <summary>
/// swagger 响应体类
/// </summary>
public class ResponseBody
{
    /// <summary>
    /// 调用 swagger 接口返回的 http 状态码
    /// </summary>
    public int code { get; set; }
    /// <summary>
    /// 调用 swagger 接口返回的错误消息
    /// </summary>
    public string errorMessage { get; set; } = "";
    /// <summary>
    /// 调用 swagger 接口返回的消息
    /// </summary>
    public string message { get; set; } = "";
}

//public class ResponseBody
//{
//    /// <summary>
//    /// swagger 调用接口是否成功
//    /// </summary>
//    public bool isSuccess { get; set; }
//    /// <summary>
//    /// swagger 调用接口详情
//    /// </summary>
//    public List<string>? details { get; set; }
//    /// <summary>
//    /// swagger 调用接口错误信息
//    /// </summary>
//    public string error { get; set; } = string.Empty;
//}
