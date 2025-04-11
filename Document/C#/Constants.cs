using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dll_Csharp
{
    static class Constants
    {
        public static readonly int SUCCESS_SEND = 0;	//发送成功	
        public static readonly int FAIL_COMM = 1;	//发送失败：通讯异常
        public static readonly int FAIL_SEND_TIME_OUT = 2;	//发送失败：发送超时
        public static readonly int FAIL_FLASH = 3;	//发送失败：擦写Flash次数太多，卡写保护（当日发送不能超过100次）

        public static readonly int CONTENT_TYPE_NONE = -1;	//无

        public static readonly int CONTENT_TYPE_COLLECTION_DATA = 0;	//实时采集
        public static readonly int CONTENT_TYPE_INTERNAL_TEXT = 1;	//内码文字
        public static readonly int CONTENT_TYPE_LINE_UP = 2;	//排队叫号
        public static readonly int CONTENT_TYPE_IMAGE_GROUP = 3;	//图片组
        public static readonly int CONTENT_TYPE_DATA_TIME = 4;	//日期时间

        public static readonly int CONTENT_TYPE_VOICE_PLAY = 5;	//语音播放

        public static readonly int CONTENT_TYPE_SHOW_PAGE_PLAY = 6;	//显示页点播
        public static readonly int CONTENT_TYPE_IMAGE_PLAY = 7;	//图片组点播

        public static readonly int CONTENT_TYPE_SWITCH_CONTROL = 8;	//继电器控制
        public static readonly int CONTENT_TYPE_BRIGHT_SET = 9;	//亮度设置
        public static readonly int CONTENT_TYPE_PLAY_CONTROL = 10;	//开关屏控制
        public static readonly int CONTENT_TYPE_TIME_CHECK = 11;	//时间校准

        public static readonly int CONTENT_TYPE_TEMPLATE = 12;	//节目单模板
        public static readonly int CONTENT_TYPE_BAR_SCREEN = 13;	//条屏显示

        public static readonly int CONTENT_TYPE_MEDIA = 14;	//同步视频播放      

        public static readonly int CONTENT_TYPE_COORDINATETEXT = 15;	//坐标文本内容下发      

        public static readonly int COMM_MODE_UDP = 1;	//UDP
        public static readonly int COMM_MODE_TCP_CLIENT = 2;	//TCP（客户端多点）
        public static readonly int COMM_MODE_TCP_SERVER = 3;	//TCP（服务端单点）
        public static readonly int COMM_MODE_RS232 = 4;	//串口 RS232
        public static readonly int COMM_MODE_RS485 = 5;	//串口 RS485
       
    }
}
