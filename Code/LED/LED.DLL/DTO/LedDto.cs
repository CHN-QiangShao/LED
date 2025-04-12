namespace LED.DLL;

public class LedDto
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public TaskType taskType { get; set; }
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
