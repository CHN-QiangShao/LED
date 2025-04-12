namespace LED.DLL;

public enum TaskType
{
    /// <summary>
    /// 入库
    /// </summary>
    inbound,
    /// <summary>
    /// 出库
    /// </summary>
    outbound,
    /// <summary>
    /// 手动入库
    /// </summary>
    manualInbound,
    /// <summary>
    /// 手动出库
    /// </summary>
    manualOutbound,
    /// <summary>
    /// 搬运
    /// </summary>
    transport,
    /// <summary>
    /// 移库
    /// </summary>
    move
}
