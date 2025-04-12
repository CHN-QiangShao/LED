namespace LED.DLL;

public class LedValue
{
    /// <summary>
    /// 控制卡 IP 地址
    /// </summary>
    public string controlCardIP = "192.168.0.100";

    // 与控制卡通信所用的网络通信协议 net protocol
    public int UDP = 1; // UDP 协议
    public int TCP = 2; // TCP 协议

    /// <summary>
    /// 内码文字 ，要求：
    /// <br>1、参数需要与所建模板的内码文字一致</br>
    /// <br>2、值唯一</br>
    /// </summary>
    public int uid = 1;

    // 字体颜色 fontColor
    public int red      = 1; // 红色
    public int green    = 2; // 绿色
    public int yellow   = 3; // 黄色

    // 字体 font
    public int songFont             = 1; // 宋体
    public int regularScript        = 2; // 楷体
    public int bold                 = 3; // 黑体
    public int officialScript       = 4; // 隶书
    public int semiCursiveScript    = 5; // 行书/幼圆

    // 字体大小 fontSize
    public int twelveSquared        = 0; // 12×12 点阵
    public int sixteenSquared       = 1; // 16×16 点阵
    public int twentyFourSquared    = 2; // 24×24 点阵
    public int thirtyTwoSquared     = 3; // 32×32 点阵
    public int fortyEightSquared    = 4; // 48×48 点阵
    public int sixtyFourSquared     = 5; // 64×64 点阵
    public int eightySquared        = 6; // 80×80 点阵
    public int ninetySixSquared     = 7; // 96×96 点阵
}
