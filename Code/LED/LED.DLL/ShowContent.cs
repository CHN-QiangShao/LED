using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LED.DLL;

/// <summary>
/// 该类继承后台服务类 BackgroundService，用于在应用程序后台执行长时间运行的任务
/// </summary>
public class ShowContent : BackgroundService
{
    public ILogger<ShowContent> Logger { get; set; } = null!;

    // 创建对象
    //public LedScreen ledScreen = new LedScreen("192.168.0.100", 1, 1, 1, 1, 1);
    public LedScreen ledScreen = new LedScreen();

    /// <summary>
    /// BackgroundService 的生命周期方法：当应用程序启动时会调用 StartAsync() 来启动后台服务
    /// <br>作用：通常进行初始化操作</br>
    /// </summary>
    /// <param name="cancellationToken">取消标志</param>
    /// <returns>执行基类的启动逻辑</returns>
    public override Task StartAsync(CancellationToken cancellationToken = default)
    {
        return base.StartAsync(cancellationToken);
    }


    /// <summary>
    /// 不是 BackgroundService 的标准方法，是自定义方法。它不会自动被调用，除非在其他地方显式调用它。
    /// </summary>
    /// <param name="cancellationToken">取消标志</param>
    /// <returns>任务刷新时间</returns>
    public async Task Run(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(5000, cancellationToken);  // 等待 5000 毫秒
        }
    }


    /// <summary>
    /// BackgroundService 的生命周期方法：当应用程序停止时，会调用 StopAsync() 来停止后台服务
    /// <br>作用: 通常进行清理操作</br>
    /// </summary>
    /// <param name="cancellationToken">取消标志</param>
    /// <returns>执行基类的停止逻辑</returns>
    public override Task StopAsync(CancellationToken cancellationToken = default)
    {
        return base.StopAsync(cancellationToken);
    }


    /// <summary>
    /// 在 StartAsync 方法执行完成后，会调用 ExecuteAsync 方法来执行后台任务
    /// </summary>
    /// <param name="cancellationToken">取消标志</param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            ledScreen.ShowContentInScreen();
            await Task.Delay(5000, cancellationToken);
        }
    }
}
