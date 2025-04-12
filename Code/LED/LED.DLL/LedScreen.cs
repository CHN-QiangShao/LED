using System.Runtime.InteropServices;

namespace LED.DLL;

public class LedScreen
{
    LedValue ledValue = new LedValue();

    // 声明非托管函数
    [DllImport("QYLED.dll", CallingConvention = CallingConvention.StdCall)]
    // 发送实时采集（UDP；TCP）
    public static extern int SendCollectionData_Net(string TshowContent, string TIP, int TnetProtocol, int TtypeNo, int TfontColor, int TfontBody, int TfontSize);

    // 定义成员字段
    private string _ip;         // 控制卡 ip 地址
    private int _netProtocol;   // 网络通信协议
    private int _uid;           // 种类编号
    private int _color;         // 字体颜色
    private int _font;          // 字体
    private int _size;          // 字体大小

    // 定义成员属性
    public string Ip { get; set; } = string.Empty;
    public int NetProtocol { get => _netProtocol; set => _netProtocol = value; }
    public int Uid { get => _uid; set => _uid = value; }
    public int Color { get => _color; set => _color = value; }
    public int Font { get => _font; set => _font = value; }
    public int Size { get => _size; set => _size = value; }


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
            ret = SendCollectionData_Net(content[i], ledValue.controlCardIP, ledValue.UDP, ledValue.uid + i, ledValue.green, ledValue.songFont, ledValue.twelveSquared);
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
