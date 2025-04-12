namespace LED.DLL;

public class LedScreen
{
    LedValue ledValue = new LedValue();

    // 定义成员字段
    private string _ip;         // 控制卡 ip 地址
    private int _netProtocol;   // 网络通信协议
    private int _uid;           // 种类编号
    private int _areaWidth;     // 区域宽度
    private int _areaHeight;    // 区域高度
    private int _screenColor;   // 显示屏颜色
    private int _showStyle;     // 显示方式
    private int _showSpeed;     // 显示速度
    private int _stopTime;      // 停留时间
    private int _fontColor;     // 字体颜色
    private int _font;          // 字体
    private int _size;          // 字体大小
    private int _updateStyle;   // 更新方式
    private int _powerOffSave;  // 是否掉电保存
    private int _rotateMode;    // 旋转模式

    // 定义成员属性
    public string Ip { get => _ip; set => _ip = value; }
    public int NetProtocol { get => _netProtocol; set => _netProtocol = value; }
    public int Uid { get => _uid; set => _uid = value; }
    public int AreaWidth { get => _areaWidth; set => _areaWidth = value; }
    public int AreaHeight { get => _areaHeight; set => _areaHeight = value; }
    public int ScreenColor { get => _screenColor; set => _screenColor = value; }
    public int ShowStyle { get => _showStyle; set => _showStyle = value; }
    public int ShowSpeed { get => _showSpeed; set => _showSpeed = value; }
    public int StopTime { get => _stopTime; set => _stopTime = value; }
    public int FontColor { get => _fontColor; set => _fontColor = value; }
    public int Font { get => _font; set => _font = value; }
    public int Size { get => _size; set => _size = value; }
    public int UpdateStyle { get => _updateStyle; set => _updateStyle = value; }
    public int PowerOffSave { get => _powerOffSave; set => _powerOffSave = value; }
    public int RotateMode { get => _rotateMode; set => _rotateMode = value; }


    //// 构造方法
    //public LedScreen(string ip, int netProtocol, int uid, int color, int font, int size)
    //{
    //    this.Ip = ip;
    //    this.NetProtocol = netProtocol;
    //    this.Uid = uid;
    //    this.Color = color;
    //    this.Font = font;
    //    this.Size = size;
    //}


    /// <summary>
    /// 把数据显示到 LDE 显示屏上
    /// </summary>
    public void ShowContentInScreen()
    {
        int ret;
        string[] content = { "入库任务", "HAD-B10102", "A019-2", "00-001-106" };
        for (int i = 0; i < 4; i++)
        {
            // 调用非托管函数
            //ret = QYLED_DLL.SendCollectionData_Net(content[i], ledValue.controlCardIP, ledValue.UDP, ledValue.uid + i, ledValue.green, ledValue.songFont, ledValue.twelveSquared);
            ret = QYLED_DLL.SendInternalText_Net(content[i], ledValue.controlCardIP, ledValue.UDP, ledValue.areaWidth, ledValue.areaHeight,
                                    ledValue.uid + i, ledValue.singleFundamentalColor, ledValue.moveFromRightToLeft, 
                                    ledValue.showSpeedSlowest, ledValue.cyclicShow, ledValue.red, ledValue.songFont,
                                    ledValue.twelveSquared, ledValue.updatedThisImmediately, ledValue.saveFalse, ledValue.nonRotate);
            if (ret == 0)
            {
                Console.WriteLine("成功");
            }
            else
            {
                Console.WriteLine("失败");
            }
        }
    }
}
