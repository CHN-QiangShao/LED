namespace LED.DLL;

/// <summary>
/// swagger 请求体类
/// </summary>
public class RequestBody
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public string taskType { get; set; } = string.Empty;
    /// <summary>
    /// 端拾器名
    /// </summary>
    public string endPickupName { get; set; } = string.Empty;
    /// <summary>
    /// 端拾器码
    /// </summary>
    public string endPickupCode { get; set; } = string.Empty;
    /// <summary>
    /// 仓库位置
    /// </summary>
    public string location { get; set; } = string.Empty;
}
