using Microsoft.Extensions.Configuration;

namespace LED.DLL;

public class LedValue
{
    /// <summary>
    /// 从 appsettings.json 获取控制卡 IP 地址
    /// </summary>
    public string controlCardIP = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build()
        .GetSection("ControlCardSettings")["ControlCardIP"]!;

    // 与控制卡通信所用的网络通信协议 net protocol
    public int UDP = 1; // UDP 协议
    public int TCP = 2; // TCP 协议

    /// <summary>
    /// 区域宽度，要求：
    /// <br>1、参数需要与所建模板的内码文字一致</br>
    /// <br>2、不大于屏宽</br>
    /// <br>3、必须是 8 的倍数</br>
    /// <br>4、最小值是 16</br>
    /// </summary>
    public int areaWidth = 72;

    /// <summary>
    /// 区域高度，要求：
    /// <br>1、参数需要与所建模板的内码文字一致</br>
    /// <br>2、不大于屏高</br>
    /// <br>3、最小值是 16</br>
    /// </summary>
    public int areaHeight = 64;

    /// <summary>
    /// 内码文字 ，要求：
    /// <br>1、参数需要与所建模板的内码文字一致</br>
    /// <br>2、值唯一</br>
    /// </summary>
    public int uid = 1;

    // 显示屏颜色 screenColor，要求参数需要与所建模板的内码文字一致
    public int singleFundamentalColor   = 1; // 单基色
    public int twoFundamentalColor      = 2; // 双基色
    public int threeFundamentalColors   = 3; // 三基色

    // 显示方式 showStyle
    public int showStyleAdaptive                = 0;  // 自适应(系统自动配置)
    public int moveFromRightToLeft              = 1;  // 从右向左移动
    public int moveFromLeftToRight              = 2;  // 从左向右移动
    public int moveFromBottomToTop              = 3;  // 从下向上移动
    public int moveFromTopToBottom              = 4;  // 从上向下移动
    public int expandFromRightToLeft            = 5;  // 从右向左展开
    public int expandFromLeftToRight            = 6;  // 从左向右展开
    public int expandFromBottomToTop            = 7;  // 从下向上展开
    public int expandFromTopToBottom            = 8;  // 从上向下展开
    public int showNow                          = 9;  // 立即显示
    public int expandFromSidesToCenter          = 10; // 从中间向两边展开
    public int expandFromCenterToSides          = 11; // 从两边向中间展开
    public int expandFromCenterToTopAndBottom   = 12; // 从中间向上下展开
    public int expandFromCenterToBottomAndTop   = 13; // 从上下向中间展开
    public int blink                            = 14; // 闪烁
    public int rightLouver                      = 15; // 右百叶
    public int bottomLouvre                     = 16; // 下百叶

    // 显示速度 showSpeed，数值越大，移动速度越慢
    public int showSpeedAdaptive    = 0; // 自适应(控制卡自动配置)
    public int showSpeedFastest     = 1;
    public int showSpeedTwo         = 2;
    public int showSpeedThree       = 3;
    public int showSpeedFour        = 4;
    public int showSpeedFive        = 5;
    public int showSpeedSix         = 6;
    public int showSpeedSeven       = 7;
    public int showSpeedSlowest     = 8;

    // 停留时间 stopTime
    public int cyclicShow       = 0;   // 由程序依据播放方式和每屏的显示字符数自动设定(每屏内容不停留, 循环播放)
    public int unit             = 1;   //  N*5 (以 5 秒钟为一个单位)
    public int stationaryShow   = 255; // 一直静止显示

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

    // 更新方式 updateStyle
    public int updatedAllImmediately    = 1; // 发送完毕所有素材立即更新
    public int updatedThisImmediately   = 2; // 发送完毕后本素材立即更新，其他素材显示不变
    public int normalDisplay            = 3; // 发送完毕后本素材不立即更新，按正常显示进行

    // 是否掉电保存 powerOffSave
    public bool saveTrue  = true;   // 是
    public bool saveFalse = false;  // 否

    // 旋转模式 rotateMode
    public int nonRotate                = 0; // 不旋转
    public int singleCharacterRotate    = 1; // 单字旋转
    public int areaRotate               = 2; // 区域旋转

    /// <summary>
    /// 12×12 字体模板下单行最多显示 GBK 编码下字符串所占字节数
    /// </summary>
    public int longText = 10;
}
