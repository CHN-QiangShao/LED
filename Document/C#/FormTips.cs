using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dll_Csharp
{
    public partial class FormTips : Form
    {

        string[] _szArrCommon = new string[20];

        public FormTips()
        {
            InitializeComponent();

            _szArrCommon[0] = "确定已用参数配置软件配置好控制卡，保证上电重启能正常显示开机信息";
            _szArrCommon[1] = "确定已用编辑软件成功下发节目单：添加显示页--》添加区域--》添加素材--》设置素材属性--》下发";
            
            _szArrCommon[2] = "如固定不变的显示内容或表格素材，请使用编辑软件下发，不需调用SDK";
            _szArrCommon[3] = "对比前后两次的下发内容，如是相同不需下发";

            _szArrCommon[4] = "检查控制卡是否已正常接入网络，IP地址是否能ping通";
            _szArrCommon[5] = "检查控制卡是否已配置服务端端口";

            _szArrCommon[6] = "检查控制卡是否已配置串口通讯";
            _szArrCommon[7] = "检查控制卡串口波特率是否正确";

            _szArrCommon[8] = "如显示屏没正常显示，请从以下步骤排查：";

            _szArrCommon[11] = "确保每个实时采集的【种类编号】唯一不能相同，如超过8个汉字（16个字符）请使用内码文字";
            _szArrCommon[12] = "如是多个实时采集，请换成多个实时采集的函数下发";

            _szArrCommon[13] = "确保每个内码文字的【UID】唯一不能相同，如不超过8个汉字（16个字符）请使用实时采集";
            _szArrCommon[14] = "如是多个内码文字，请换成多个内码文字的函数下发";            
        }

        public void showTips(int nRetCode, int nContentType, int nCommMode)
        {
            Color clrFont = Color.Blue;
            int nFormHeight = 260;
            int nHeightAdd = 0;
            int nIndex = 1;

            StringBuilder sbTips = new StringBuilder();
            if (nRetCode == Constants.SUCCESS_SEND)
            {
                lbTips.ForeColor = Color.Blue;

                sbTips.Append("    ---》已下发成功" + Environment.NewLine + Environment.NewLine);
                if ((nContentType == Constants.CONTENT_TYPE_COLLECTION_DATA) ||
                       (nContentType == Constants.CONTENT_TYPE_INTERNAL_TEXT) ||
                       (nContentType == Constants.CONTENT_TYPE_LINE_UP) ||
                       (nContentType == Constants.CONTENT_TYPE_IMAGE_GROUP) ||
                        (nContentType == Constants.CONTENT_TYPE_DATA_TIME) ||
                        (nContentType == Constants.CONTENT_TYPE_SHOW_PAGE_PLAY) ||
                       (nContentType == Constants.CONTENT_TYPE_IMAGE_PLAY))
                {
                    sbTips.Append("    " + _szArrCommon[8] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[0] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[1]);
                }

                if (nContentType == Constants.CONTENT_TYPE_COLLECTION_DATA)
                {
                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[11] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[12]);
                }
                else if (nContentType == Constants.CONTENT_TYPE_INTERNAL_TEXT)
                {
                    nFormHeight = 380;

                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[13] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[14] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】像素颜色：0 -- 单基色；1 -- 双基色；2 -- 三基色" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】所在区域宽度" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】所在区域高度" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】强烈建议使用掉电不保存" + Environment.NewLine);
                    sbTips.Append("           如特殊场合使用掉电保存，需控制下发频率，当日发送不能超过100次，否则会出现卡写保护异常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】如显示内容不超过8个汉字（16个字符），请换成实时采集素材");
                }
                else if (nContentType == Constants.CONTENT_TYPE_LINE_UP)
                {
                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保排队叫号的【窗口地址】是否正确");
                }
                else if (nContentType == Constants.CONTENT_TYPE_IMAGE_GROUP)
                {
                    nFormHeight = 320;

                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保图片组的【UID】是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查图片路径是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保图片是bmp格式" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】图片仅支持掉电保存下发，不能频繁下发" + Environment.NewLine);
                    sbTips.Append("           如在频繁更新的场合，请使用图片组点播或视频同步指令实现");
                }
                else if (nContentType == Constants.CONTENT_TYPE_DATA_TIME)
                {
                    nFormHeight = 280;

                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保日期时间的【UID】是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查日期时间格式是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】如日期时间不准确，请对控制卡进行校时，同时确保电池正常工作");
                }
                else if (nContentType == Constants.CONTENT_TYPE_VOICE_PLAY)
                {                    
                    sbTips.Append("     如没声音输出，请确保有外接喇叭");
                }
                else if (nContentType == Constants.CONTENT_TYPE_SHOW_PAGE_PLAY)
                {
                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡节目单是否存在多个显示页" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】点播的显示页序号是否正确");
                }
                else if (nContentType == Constants.CONTENT_TYPE_IMAGE_PLAY)
                {
                    nFormHeight = 300;

                    sbTips.Append(Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】图片组只能在第一个显示页点播" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保控制卡的图片组存在多张图片" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的区域序号是否正常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的图片起始序号是否正常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的图片数量是否正常");
                }                
                else if (nContentType == Constants.CONTENT_TYPE_TEMPLATE)
                {
                    nFormHeight = 300;

                    sbTips.Append("    " + _szArrCommon[8] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】强烈建议使用编辑软件制作节目单模板" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】由于控制卡不能回读验证节目单模板，故不能在程序后台自动下发" + Environment.NewLine);
                    sbTips.Append("           需开发类似编辑软件的界面手动下发，否则出现故障异常会给维护带来困难" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】节目单模板是掉电保存的，需控制下发频率，当日发送不能超过100次，否则会出现卡写保护异常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保区域不重叠，x坐标和宽度是8的倍数" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】根据SDK文档说明确保素材属性的正确性");
                }
                else if (nContentType == Constants.CONTENT_TYPE_BAR_SCREEN)
                {
                }
            }
            else
            {
                nFormHeight = 330;
                nHeightAdd = 60;

                //设置好素材的UID；实时采集的种类编号；图片的路径；排队的窗口地址     
                clrFont = Color.Red;

                sbTips.Append("1. 下发失败！返回错误码为： %RETCODE%".Replace("%RETCODE%", nRetCode.ToString()) + Environment.NewLine);
                sbTips.Append("    返回错误码说明：" + Environment.NewLine);
                sbTips.Append("      1 -- 通讯异常" + Environment.NewLine);
                sbTips.Append("      2 -- 发送超时" + Environment.NewLine);
                sbTips.Append("      3 -- 擦写Flash次数太多，卡写保护异常（当日发送不能超过100次），需重启才能恢复" + Environment.NewLine);

                sbTips.Append(Environment.NewLine);
                sbTips.Append("2. 请从以下步骤故障排查：" + Environment.NewLine);

                if (nCommMode == Constants.COMM_MODE_TCP_CLIENT)
                {
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[4] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡是否已配置网络客户端多点通讯" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡是否已配置服务端IP地址" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[5] + Environment.NewLine);
                }
                else if (nCommMode == Constants.COMM_MODE_TCP_SERVER)
                {
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[4] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡是否已配置网络服务端单点通讯" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[5] + Environment.NewLine);
                }
                else if (nCommMode == Constants.COMM_MODE_RS232)
                {
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[6] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[7] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查电脑连接到控制卡的串口RS232通讯线是否正常" + Environment.NewLine);
                }
                else if (nCommMode == Constants.COMM_MODE_RS485)
                {
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[6] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[7] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查电脑连接到控制卡的RS485通讯线是否正常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡的RS485地址是否正确" + Environment.NewLine);
                }
                else
                {
                    nHeightAdd = 0;
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[4] + Environment.NewLine);
                }

                sbTips.Append("  【" + (nIndex++) + "】发送失败的情况需补发1-2次，最多不超过3次" + Environment.NewLine);
                sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[0] + Environment.NewLine);

                if ((nContentType == Constants.CONTENT_TYPE_COLLECTION_DATA) ||
                        (nContentType == Constants.CONTENT_TYPE_INTERNAL_TEXT) ||
                        (nContentType == Constants.CONTENT_TYPE_LINE_UP) ||
                        (nContentType == Constants.CONTENT_TYPE_IMAGE_GROUP) ||
                         (nContentType == Constants.CONTENT_TYPE_DATA_TIME) ||
                         (nContentType == Constants.CONTENT_TYPE_SHOW_PAGE_PLAY) ||
                        (nContentType == Constants.CONTENT_TYPE_IMAGE_PLAY))
                {
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[1] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[2] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[3] + Environment.NewLine);
                }

                if (nContentType == Constants.CONTENT_TYPE_COLLECTION_DATA)
                {
                    nFormHeight = 400;

                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[11] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[12]);
                }
                else if (nContentType == Constants.CONTENT_TYPE_INTERNAL_TEXT)
                {
                    nFormHeight = 530;

                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[13] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】" + _szArrCommon[14] + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】像素颜色：0 -- 单基色；1 -- 双基色；2 -- 三基色" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】所在区域宽度" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】所在区域高度" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】强烈建议使用掉电不保存" + Environment.NewLine);
                    sbTips.Append("             如特殊场合使用掉电保存，需控制下发频率，当日发送不能超过100次，否则会出现卡写保护异常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】如显示内容不超过8个汉字（16个字符），请换成实时采集素材");
                }
                else if (nContentType == Constants.CONTENT_TYPE_LINE_UP)
                {
                    nFormHeight = 400;

                    sbTips.Append("  【" + (nIndex++) + "】保证每个排队叫号的【窗口地址】唯一不能相同");
                }
                else if (nContentType == Constants.CONTENT_TYPE_IMAGE_GROUP)
                {
                    nFormHeight = 480;

                    sbTips.Append("  【" + (nIndex++) + "】确保图片组的【UID】唯一不能相同" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查图片路径是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保图片是bmp格式" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】图片仅支持掉电保存下发，不能频繁下发" + Environment.NewLine);
                    sbTips.Append("             如在频繁更新的场合，请使用图片组点播或视频同步指令实现");
                }
                else if (nContentType == Constants.CONTENT_TYPE_DATA_TIME)
                {
                    nFormHeight = 430;

                    sbTips.Append("  【" + (nIndex++) + "】确保日期时间的【UID】唯一不能相同" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查日期时间格式是否正确" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】如日期时间不准确，请对控制卡进行校时，同时确保电池正常工作");
                }
                else if (nContentType == Constants.CONTENT_TYPE_VOICE_PLAY)
                {
                    sbTips.Append("  【" + (nIndex++) + "】确定是语音一体卡还是分体卡，根据SDK文档说明选择对应转发的串口号");
                }
                else if (nContentType == Constants.CONTENT_TYPE_SHOW_PAGE_PLAY)
                {
                    nFormHeight = 430;

                    sbTips.Append("  【" + (nIndex++) + "】检查控制卡节目单是否存在多个显示页" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】点播的显示页序号是否正确");
                }
                else if (nContentType == Constants.CONTENT_TYPE_IMAGE_PLAY)
                {
                    nFormHeight = 460;

                    sbTips.Append("  【" + (nIndex++) + "】图片组只能在第一个显示页点播" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保控制卡的图片组存在多张图片" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的区域序号是否正常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的图片起始序号是否正常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】检查点播的图片数量是否正常");
                }
                else if (nContentType == Constants.CONTENT_TYPE_SWITCH_CONTROL)
                {
                    sbTips.Append("  【" + (nIndex++) + "】确保继电器回路编号是否正常");
                }
                else if (nContentType == Constants.CONTENT_TYPE_TIME_CHECK)
                {
                    sbTips.Append("  【" + (nIndex++) + "】确保电池正常工作");
                }
                else if (nContentType == Constants.CONTENT_TYPE_TEMPLATE)
                {
                    nFormHeight = 430;

                    sbTips.Append("  【" + (nIndex++) + "】强烈建议使用编辑软件制作节目单模板" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】由于控制卡不能回读验证节目单模板，故不能在程序后台自动下发" + Environment.NewLine);
                    sbTips.Append("           需开发类似编辑软件的界面手动下发，否则出现故障异常会给维护带来困难" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】节目单模板是掉电保存的，需控制下发频率，当日发送不能超过100次，否则会出现卡写保护异常" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】确保区域不重叠，x坐标和宽度是8的倍数" + Environment.NewLine);
                    sbTips.Append("  【" + (nIndex++) + "】根据SDK文档说明确保素材属性的正确性");
                }
                else if (nContentType == Constants.CONTENT_TYPE_BAR_SCREEN)
                {
                }
            }

            this.Height = nFormHeight + nHeightAdd;

            lbTips.ForeColor = clrFont;
            lbTips.Text = sbTips.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
