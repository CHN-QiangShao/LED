using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Net;

namespace dll_Csharp
{
    public partial class FormMain : Form
    {
        int nRetCode = 0;
        int nNetProtocol = 1;
        int updatestyle = 1;
        bool save = false;
        bool flash = false;
        int comport = 1;
        int ipport = 1;
        int baud = 1;
        int ledcolour = 1;

        int nXPos = 0;
        int nYPos = 0;
        int width = 1;
        int higth = 1;

        string text = "";
        string voicetext = "";
        int UID = 1;
        int colour = 1;
        int font = 1;
        int size = 1;
        string ip = "";
        int pagenumber = 1;
        int style = 1;
        int speed = 1;
        int stoptime = 1;

        string imgpath = "E:\test1.bmp";

        int _nImgIndex = -1;
        string[] _arrImgPath;

        int brightness = 1;
        int timeformat = 0;
        int showformat = 0;
        //485地址
        string rsadr = "1";
        //1=串口，2=485
        int rstype = 1;

        int nContentNo = 1;
        int _nPageIndex = 0;
        int _nItemIndex = 0;

        int _nControlType = 0;
        int nRotateMode = 0;

        StringBuilder sbContent = new StringBuilder();
        FormTips _formTips = new FormTips();

        byte[] arrLight1 = new byte[] { 0xFE, 0x5C, 0x4B, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x77, 0X00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x68, 0x00, 0xF4, 0x01, 0xFF, 0x00, 0x00, 0xFF, 0xFF };
        byte[] arrLight2 = new byte[] { 0xFE, 0x5C, 0x4B, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x03, 0x68, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF };
        byte[] arrLight3 = new byte[] { 0xFE, 0x5C, 0x4B, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x04, 0x68, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF };
        byte[] arrLight4 = new byte[] { 0xFE, 0x5C, 0x4B, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x68, 0x00, 0x0A, 0x00, 0xFF, 0xFF, 0x00, 0xFF, 0xFF };
        byte[] arrLight5 = new byte[] { 0xFE, 0x5C, 0x4B, 0x89, 0x1B, 0x00, 0x00, 0x00, 0x77, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x68, 0x00, 0xF4, 0x01, 0x00, 0x00, 0xFF, 0xFF, 0xFF };

        List<byte[]> _listSendBuffer = new List<byte[]>();

        private Socket _udpSocket = null;
        IPEndPoint _localIpep = null;

        byte[] _byArrRecBuffer = new byte[4096];

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        public FormMain()
        {
            InitializeComponent();         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sbContent.Append("故事的名字是----好伙伴");
            sbContent.Append("马哈多是一名护林员，他养着一只猎狗。");
            sbContent.Append("在一处草丛中，猎狗咬住马哈多的衣角，汪汪汪地叫起来。");
            sbContent.Append("有一次，他带着猎狗上山巡逻。马哈多向前走了几步，拨开草丛，发现地上躺着一只受伤的乌鸦。");
            sbContent.Append("马哈多把乌鸦带回家，给它抹药，帮它养伤。");
            sbContent.Append("乌鸦的伤很快就好了。从此，乌鸦成了马哈多和猎狗的小伙伴。");
            sbContent.Append("马哈多和猎狗外出护林时，乌鸦就守在家里。护林回来，乌鸦就会哇哇地叫着迎接他们。日子过得很快乐。");
            sbContent.Append("可是，过了不久，发生了一件意外的事：马哈多两天两夜没见到可爱的猎狗了。");
            sbContent.Append("他很担心，吃不下饭，睡不着觉，");
            sbContent.Append("像得了一场大病。");
            sbContent.Append("“猎狗，你究竟在哪里呀?”马哈多抬头望望乌鸦。");
            sbContent.Append("乌鸦飞进飞出，也显得焦急不安。更令马哈多惊奇的是，乌鸦把他放在院子里的肉一块一块都叼走了。");
            sbContent.Append("马哈多心理想：它把肉叼到哪里去了呢?我得弄明白乌鸦叼着肉在前边飞，马哈多紧紧地跟在后面。乌鸦落在一口枯井边，把肉投入井中。");
            sbContent.Append("马哈多往井里一看，什么都明白了。原来，他心爱的猎狗掉进了枯井，井太深了，它爬不出来。乌鸦怕它饿死，就一次一次地给它送食物。");
            sbContent.Append("马哈多看着他的两个好伙伴，激动得流下了眼泪。");

            _listSendBuffer = new List<byte[]>();
            _listSendBuffer.Add(arrLight1);
            _listSendBuffer.Add(arrLight2);
            _listSendBuffer.Add(arrLight3);
            _listSendBuffer.Add(arrLight4);
            _listSendBuffer.Add(arrLight5);

            _udpSocket = new Socket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0).Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            string _szAppPath = Application.StartupPath + "\\";

            _arrImgPath = new string[] { _szAppPath + "test192x64_1.bmp", _szAppPath + "test192x64_2.bmp" };

            //GifFrames2Bmp(@"E:\0.gif", @"E:");

            /* Graphics graphics = CreateGraphics();
             SizeF sizeF = graphics.MeasureString("A", new Font("宋体", 9));
             StringBuilder sb = new StringBuilder();
             for (int i = 7; i < 66; i++)
             {
                 sizeF = graphics.MeasureString("A", new Font("宋体", i));
                 sb.Append(string.Format(i + " 宽：{0}，高：{1}", sizeF.Width, sizeF.Height)+ Environment.NewLine);
             }
             textBoxText.Text = sb.ToString();
             graphics.Dispose();*/

            //Console.WriteLine($"打印{1}");
        }
        public void GifFrames2Bmp(string szGifFilePath, string szSavedPath)
        {
            Image gifFile = Image.FromFile(szGifFilePath);
            FrameDimension fd = new FrameDimension(gifFile.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            int nCount = gifFile.GetFrameCount(fd);
            //以bmp格式保存各帧
            for (int i = 0; i < nCount; i++)
            {
                gifFile.SelectActiveFrame(fd, i);

                ImageCodecInfo bmpCodec = GetEncoder(ImageFormat.Bmp);
                EncoderParameters parameters = new EncoderParameters();
                parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 24);
                gifFile.Save(szSavedPath + "\\frame_" + i + ".bmp", bmpCodec, parameters);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void GifFrames2Jpg(string szGifPath, string szSavedPath)
        {
            Image gif = Image.FromFile(szGifPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(szSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        private void radioButtonuUDP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIPPort.Enabled = false;
            textBoxIP.Enabled = true;
            buttonOpenServer.Enabled = false;
            buttonCloseServer.Enabled = false;
            comboBoxCOMPort.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            textBoxRSAdr.Enabled = false;

            radioButtonZFCOM1.Enabled = true;
            radioButtonZFCOM2.Enabled = true;
            radioButtonZF485.Enabled = true;
            radioButtonBZF.Enabled = true;    
        }

        private void radioButtonTCPServer_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIPPort.Text = "8900";
            textBoxIPPort.Enabled = false;
            textBoxIP.Enabled = true;
            buttonOpenServer.Enabled = false;
            buttonCloseServer.Enabled = false;
            comboBoxCOMPort.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            textBoxRSAdr.Enabled = false;

            radioButtonZFCOM1.Enabled = true;
            radioButtonZFCOM2.Enabled = true;
            radioButtonZF485.Enabled = true;
            radioButtonBZF.Enabled = true;
        }

        private void radioButtonTCPClient_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIPPort.Enabled = true;
            textBoxIP.Enabled = false;
            buttonOpenServer.Enabled = true;
            buttonCloseServer.Enabled = false;
            comboBoxCOMPort.Enabled = false;
            comboBoxBaudRate.Enabled = false;
            textBoxRSAdr.Enabled = false;

            radioButtonZFCOM1.Enabled = true;
            radioButtonZFCOM2.Enabled = true;
            radioButtonZF485.Enabled = true;
            radioButtonBZF.Enabled = true;
        }

        private void radioButtonCOM_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIPPort.Enabled = false;
            textBoxIP.Enabled = false;
            buttonOpenServer.Enabled = false;
            buttonCloseServer.Enabled = false;
            comboBoxCOMPort.Enabled = true;
            comboBoxBaudRate.Enabled = true;
            textBoxRSAdr.Enabled = true;

            radioButtonZFCOM1.Enabled = false;
            radioButtonZFCOM2.Enabled = false;
            radioButtonZF485.Enabled = false;
            radioButtonBZF.Enabled = false;
        }

        //选中485通讯时动作
        private void radioButton485_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIPPort.Enabled = false;
            textBoxIP.Enabled = false;
            buttonOpenServer.Enabled = false;
            buttonCloseServer.Enabled = false;
            comboBoxCOMPort.Enabled = true;
            comboBoxBaudRate.Enabled = true;
            textBoxRSAdr.Enabled = true;

            radioButtonZFCOM1.Enabled = false;
            radioButtonZFCOM2.Enabled = false;
            radioButtonZF485.Enabled = false;
            radioButtonBZF.Enabled = false;
        }

        //选中内码文字时动作
        private void radioButtonNB_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = true;
            comboBoxSave.SelectedIndex = 1;

            comboBoxUpdateStyle.Enabled = true;
            comboBoxFont.Enabled = true;
            comboBoxSize.Enabled = true;
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = true;
            comboBoxSpeed.Enabled = true;
            comboBoxStopTime.Enabled = true;
            comboBoxColour.Enabled = true;
            buttonImg.Enabled = false;
            textBoxImgPath.Enabled = false;
            textBoxText.Enabled = true;
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;
        }

        //选中实时采集时动作
        private void radioButtonCJ_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = false;
            comboBoxUpdateStyle.Enabled = false;
            comboBoxFont.Enabled = true;
            comboBoxSize.Enabled = true;
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = false;
            comboBoxSpeed.Enabled = false;
            comboBoxStopTime.Enabled = false;
            comboBoxColour.Enabled = true;
            buttonImg.Enabled = false;
            textBoxImgPath.Enabled = false;
            textBoxText.Enabled = true;
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;
        }

        //选中排队叫号时动作
        private void radioButtonPD_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = false;
            comboBoxUpdateStyle.Enabled = false;
            comboBoxFont.Enabled = false;
            comboBoxSize.Enabled = false;
            comboBoxFlash.Enabled = true;
            comboBoxStyle.Enabled = false;
            comboBoxSpeed.Enabled = false;
            comboBoxStopTime.Enabled = true;
            comboBoxColour.Enabled = true;
            buttonImg.Enabled = false;
            textBoxImgPath.Enabled = false;
            textBoxText.Enabled = true;
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;                  
        }

        //选中日期时间时动作
        private void radioButtonTime_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = true;
            comboBoxUpdateStyle.Enabled = false;
            comboBoxFont.Enabled = true;
            comboBoxSize.Enabled = true;
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = false;
            comboBoxSpeed.Enabled = false;
            comboBoxStopTime.Enabled = true;
            comboBoxColour.Enabled = true;
            buttonImg.Enabled = false;
            textBoxImgPath.Enabled = false;
            textBoxText.Enabled = false;
            comboBoxTimeFormat.Enabled = true;
            comboBoxShowFormat.Enabled = true;
        }

        //选中图片组时动作
        private void radioButtonImg_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = true;
            comboBoxSave.SelectedIndex = 1;

            comboBoxUpdateStyle.Enabled = true;
            comboBoxFont.Enabled = false;
            comboBoxSize.Enabled = false;
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = true;
            comboBoxSpeed.Enabled = true;
            comboBoxStopTime.Enabled = true;
            comboBoxColour.Enabled = false;
            buttonImg.Enabled = true;
            textBoxImgPath.Enabled = true;
            textBoxText.Enabled = false;
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;
        }

        private void rtbMediaPlay_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSave.Enabled = true;
            comboBoxSave.SelectedIndex = 1;

            comboBoxUpdateStyle.Enabled = true;
            comboBoxFont.Enabled = false;
            comboBoxSize.Enabled = false;
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = true;
            comboBoxSpeed.Enabled = true;
            comboBoxStopTime.Enabled = true;
            comboBoxColour.Enabled = false;
            buttonImg.Enabled = true;
            textBoxImgPath.Enabled = true;
            textBoxText.Enabled = false;
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;
        }

        private void rtbCoordinateText_CheckedChanged(object sender, EventArgs e)
        {
            textBoxText.Enabled = true;

            comboBoxFont.Enabled = true;
            comboBoxSize.Enabled = true;
            comboBoxColour.Enabled = true;           

            comboBoxSave.Enabled = false;
            comboBoxUpdateStyle.Enabled = false;            
            comboBoxFlash.Enabled = false;
            comboBoxStyle.Enabled = false;
            comboBoxSpeed.Enabled = false;
            comboBoxStopTime.Enabled = false;

            buttonImg.Enabled = false;
            textBoxImgPath.Enabled = false;
            
            comboBoxTimeFormat.Enabled = false;
            comboBoxShowFormat.Enabled = false;
        }

        //选取图片路径函数
        private void buttonImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Img file(*.bmp)|*.bmp";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Open Img file ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxImgPath.Text = openFileDialog1.FileName;
            }
            openFileDialog1.Dispose();
        }

        //开启服务
        private void buttonOpenServer_Click(object sender, EventArgs e)
        {
            ipport = Convert.ToInt32(textBoxIPPort.Text);
            nRetCode = 1;
            nRetCode = QYLED_DLL.OpenServer(ipport);
            if (nRetCode == Constants.SUCCESS_SEND)
            {
                buttonOpenServer.Enabled = false;
                buttonCloseServer.Enabled = true;
                MessageBox.Show("开启成功！", "开启服务");
            }
            else
            {
                MessageBox.Show("开启失败！", "开启服务");
            }
        }

        //关闭服务
        private void buttonCloseServer_Click(object sender, EventArgs e)
        {
            nRetCode = 1;
            nRetCode = QYLED_DLL.CloseServer();
            if (nRetCode == Constants.SUCCESS_SEND)
            {
                buttonOpenServer.Enabled = true;
                buttonCloseServer.Enabled = false;
                MessageBox.Show("关闭成功！", "关闭服务");
            }
            else
            {
                MessageBox.Show("关闭失败！", "关闭服务");
            }
        }

        //添加显示页
        private void buttonAddPage_Click(object sender, EventArgs e)
        {
            string starttime = "00:00";  //开始播放时间
            string stoptime = "23:59";    //结束播放时间
            int day = 0;                  //0=天天播放，1=周一播放，2=周二播放，3=…………
            nRetCode = 1;
            //result = QYLED_DLL.AddShowPage(starttime, stoptime, day);
            nRetCode = QYLED_DLL.AddShowPage(int.Parse(tbShowPageNo.Text), starttime, stoptime, day);
            if (nRetCode == Constants.SUCCESS_SEND)
            {               
                MessageBox.Show("添加成功！", "添加显示页");
            }
            else
            {
                MessageBox.Show("添加失败！", "添加显示页");
            }
        }

        //添加区域
        private void buttonAddArea_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(tbXPos.Text);        //x坐标
            int y = Convert.ToInt32(tbYPos.Text);        //y坐标
            width = Convert.ToInt32(textBoxWidth.Text);//宽度
            higth = Convert.ToInt32(textBoxHigth.Text);//高度
            nRetCode = 1;
            nRetCode = QYLED_DLL.AddArea(int.Parse(tbShowPageNo.Text), x, y, width, higth);
            if (nRetCode == Constants.SUCCESS_SEND)
            {
                MessageBox.Show("添加成功！", "添加区域");
            }
            else
            {
                MessageBox.Show("添加失败！", "添加区域");
            }
        }

        //添加素材
        private void buttonAddMaterial_Click(object sender, EventArgs e)
        {
            ledcolour = comboBoxLEDcolour.SelectedIndex;    //led颜色
            width = Convert.ToInt32(textBoxWidth.Text);     //区域宽度
            higth = Convert.ToInt32(textBoxHigth.Text);     //区域高度
            colour = comboBoxColour.SelectedIndex + 1;      //显示内容颜色
            font = comboBoxFont.SelectedIndex;          //字体
            size = comboBoxSize.SelectedIndex;              //字号
            style = comboBoxStyle.SelectedIndex;            //显示方式
            speed = comboBoxSpeed.SelectedIndex;            //显示速度
            stoptime = comboBoxStopTime.SelectedIndex;      //停留时间
            text = textBoxText.Text.Trim();                 //显示内容
            UID = Convert.ToInt32(textBoxUID.Text.Trim());  //素材uid
            imgpath = textBoxImgPath.Text;                  //图片路径
            timeformat = comboBoxTimeFormat.SelectedIndex + 1;  //日期时间长度
            showformat = comboBoxShowFormat.SelectedIndex + 1;  //日期时间格式

            int numcolour = colour;   //数字颜色
            int chrcolour = colour;   //字符颜色
            int yearlen = 0;          //=0 年四位; =1年两位
            int timedifset = 1;       //=0 滞后  =1超前       
            int hourspan = 0;         //滞后或超前小时数
            int minspan = 0;          //滞后或超前分钟数

            if (radioButtonNB.Checked)
            {
                //添加内码文字素材
                nRetCode = 1;
                nRetCode = QYLED_DLL.AddTemplate_InternalText(int.Parse(tbShowPageNo.Text), text, UID, ledcolour, style, speed, stoptime, colour, font, size, save);              
                if (nRetCode == Constants.SUCCESS_SEND)
                {
                    MessageBox.Show("添加成功！", "添加内码文字素材");
                }
                else
                {
                    MessageBox.Show("添加失败！", "添加内码文字素材");
                }
            }
            else if (radioButtonCJ.Checked)
            {
                //添加实时采集素材
                nRetCode = 1;
                nRetCode = QYLED_DLL.AddTemplate_CollectData(int.Parse(tbShowPageNo.Text), text, UID, ledcolour, colour, font, size);
                if (nRetCode == Constants.SUCCESS_SEND)
                {
                    MessageBox.Show("添加成功！", "添加实时采集素材");
                }
                else
                {
                    MessageBox.Show("添加失败！", "添加实时采集素材");
                }
            }
            else if (radioButtonPD.Checked)
            {
                //添加排队叫号素材
                nRetCode = 1;
                nRetCode = QYLED_DLL.AddTemplate_LineUp(int.Parse(tbShowPageNo.Text), text, stoptime, colour, font, size, UID, flash);
                if (nRetCode == Constants.SUCCESS_SEND)
                {
                    MessageBox.Show("添加成功！", "添加排队叫号素材");
                }
                else
                {
                    MessageBox.Show("添加失败！", "添加排队叫号素材");
                }
            }
            else if (radioButtonTime.Checked)
            {
                //添加日期时间素材
                nRetCode = 1;
                nRetCode = QYLED_DLL.AddTemplate_DateTime(int.Parse(tbShowPageNo.Text), UID, ledcolour, numcolour, chrcolour, font, size, yearlen, timeformat, showformat, timedifset, hourspan, minspan, stoptime, save);//单基色时下发正常，双基色时报错。
                if (nRetCode == Constants.SUCCESS_SEND)
                {
                    MessageBox.Show("添加成功！", "添加日期时间素材");
                }
                else
                {
                    MessageBox.Show("添加失败！", "添加日期时间素材");
                }
            }
            else if (radioButtonImg.Checked)
            {
                //添加图片组素材
                nRetCode = 1;
                nRetCode = QYLED_DLL.AddTemplate_ImageGroup(int.Parse(tbShowPageNo.Text), imgpath, UID, ledcolour, style, speed, stoptime);
                if (nRetCode == Constants.SUCCESS_SEND)
                {
                    MessageBox.Show("添加成功！", "添加素图片组材");
                }
                else
                {
                    MessageBox.Show("添加失败！", "添加图片组素材");
                }
            }
        }

        //发送模板
        private void buttonSendFormwork_Click(object sender, EventArgs e)
        {
            QYLED_DLL.SetTimeOut(int.Parse(tbSendTimeOut.Text), int.Parse(tbReceiveTimeOut.Text));           

            ip = textBoxIP.Text.Trim();                       //ip地址

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                //UDP发送模板
                nNetProtocol = 1;
                nRetCode = QYLED_DLL.SendTemplateData_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_TEMPLATE;
                szFormText = "节目单模板下发【网络 UDP】";                       
            }
            if (radioButtonTCPServer.Checked)
            {
                //TCP发送模板
                nNetProtocol = 2;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.SendTemplateData_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_TEMPLATE;
                szFormText = "节目单模板下发【网络 TCP】";                
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //下发显示内容（需建模板）
        private void buttonSendScreen_Click(object sender, EventArgs e)
        {
            QYLED_DLL.SetTimeOut(int.Parse(tbSendTimeOut.Text), int.Parse(tbReceiveTimeOut.Text));           

            ip = textBoxIP.Text.Trim();                         //ip地址
            ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口
            comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
            baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
            ledcolour = comboBoxLEDcolour.SelectedIndex;        //led颜色

            nXPos = Convert.ToInt32(tbXPos.Text);         //X坐标
            nYPos = Convert.ToInt32(tbYPos.Text);         //Y坐标
            width = Convert.ToInt32(textBoxWidth.Text);         //区域宽度
            higth = Convert.ToInt32(textBoxHigth.Text);         //区域高度

            colour = comboBoxColour.SelectedIndex + 1;          //显示颜色
            font = comboBoxFont.SelectedIndex;              //字体
            size = comboBoxSize.SelectedIndex;                  //字号
            style = comboBoxStyle.SelectedIndex;                //显示方式
            speed = comboBoxSpeed.SelectedIndex;                //移动速度
            stoptime = comboBoxStopTime.SelectedIndex;          //停留时间
            updatestyle = comboBoxUpdateStyle.SelectedIndex + 1;//更新方式
            timeformat = comboBoxTimeFormat.SelectedIndex+1;    //时间格式
            showformat = comboBoxShowFormat.SelectedIndex+1;    //显示格式
            //text = textBoxText.Text.Trim();                            //显示内容
            text = textBoxText.Text;                            //显示内容
            UID = Convert.ToInt32(textBoxUID.Text.Trim());      //素材uid
            imgpath = textBoxImgPath.Text.Trim();               //图片路径
            rsadr = textBoxRSAdr.Text;                          //485地址

            nRotateMode = cmbRotateMode.SelectedIndex;

            save = false;
            if (comboBoxSave.SelectedIndex == 0)                //掉电保存
            {
                save = true;
            }
            flash = false;
            if (comboBoxFlash.SelectedIndex == 0)               //排队叫号闪烁
            {
                flash = true;
            }

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";

            //同步播放
            //false--不支持部分区域播放；true--支持部分区域播放
            bool bAreaPlay = true;
            //0--异步；1--同步
            int nMode = 1;

            //UDP发送
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = 1;
                if (radioButtonNB.Checked)
                {                    
                    //UDP下发内码文字内容      
                    //text = "第一行" + Environment.NewLine + "第二行"  + Environment.NewLine + "第三行";
                    //text = "第1行" + "\r\n" + "第2行" + "\r\n" + "第3行";                

                    nRetCode = QYLED_DLL.SendInternalText_Net(text, ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, colour, font, size, updatestyle, save, nRotateMode);
                                          
                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "内码文字内容下发【网络 UDP】";

                    //UDP 显示页点播
                    //nNetProtocol = Constants.COMM_MODE_UDP;
                    //nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, 1);

                    //UDP 显示页点播
                    //nNetProtocol = Constants.COMM_MODE_UDP;
                    //nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, 0);                   
                }
                else if (radioButtonCJ.Checked)
                {
                    //UDP下发实时采集内容           
                    nRetCode = QYLED_DLL.SendCollectionData_Net(text, ip, nNetProtocol, UID, colour, font, size);

                    nContentType = Constants.CONTENT_TYPE_COLLECTION_DATA;
                    szFormText = "实时采集内容下发【网络 UDP】";
                }
                else if (radioButtonPD.Checked)
                {
                    //UDP下发排队叫号内容          
                    //text = "01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40";
                    //text = "01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30";
                    nRetCode = QYLED_DLL.SendLineUp_Net(text, ip, nNetProtocol, stoptime, colour, UID, flash);
                    //nRetCode = QYLED_DLL.SendLineUp2_Net(text, ip, nNetProtocol, stoptime, colour, UID, flash, 16);
                
                    nContentType = Constants.CONTENT_TYPE_LINE_UP;
                    szFormText = "排队叫号内容下发【网络 UDP】";
                }
                else if (radioButtonTime.Checked)
                {
                    //UDP 显示页点播
                    //nNetProtocol = Constants.COMM_MODE_UDP;
                    //nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, 2);

                    nRetCode = QYLED_DLL.SendDateTime_Net(ip, nNetProtocol, UID, ledcolour, 1, 1, font, size, 0, timeformat, showformat, 0, 0, 0, stoptime, nRotateMode);

                    nContentType = Constants.CONTENT_TYPE_DATA_TIME;
                    szFormText = "日期时间下发【网络 UDP】";
                }
                else if (radioButtonImg.Checked)
                {         
                    nRetCode = QYLED_DLL.SendImageGroup_Net(imgpath, ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, 2, save, 1, 1);

                    nContentType = Constants.CONTENT_TYPE_IMAGE_GROUP;
                    szFormText = "图片组下发【网络 UDP】";      
                }
                else if (rtbCoordinateText.Checked)
                {
                    //UDP下发坐标文本 
                    nRetCode = QYLED_DLL.SendCoordinateText_Net(text, ip, nNetProtocol,  nXPos, nYPos, colour, font, size);

                    nContentType = Constants.CONTENT_TYPE_COLLECTION_DATA;
                    szFormText = "坐标文本内容下发【网络 UDP】";
                }
                else if (rtbMediaPlay.Checked)
                {
                    nRetCode = QYLED_DLL.SendMultimedia_Net(imgpath, ip, nNetProtocol, bAreaPlay, nXPos, nYPos, width, higth, ledcolour, nMode, stoptime);

                    nContentType = Constants.CONTENT_TYPE_MEDIA;
                    szFormText = "同步播放【网络 UDP】";
                }
            }
            //TCP发送
            else if (radioButtonTCPServer.Checked || radioButtonTCPClient.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                if (radioButtonTCPClient.Checked)
                {
                    nNetProtocol = Constants.COMM_MODE_TCP_SERVER;
                    nCommMode = Constants.COMM_MODE_TCP_SERVER;
                }

                if (radioButtonNB.Checked)
                {     
                    nRetCode = QYLED_DLL.SendInternalText_Net(text, ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, colour, font, size, updatestyle, save, nRotateMode);

                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "内码文字内容下发【网络 TCP】";                        
                }
                else if (radioButtonCJ.Checked)
                {          
                    nRetCode = QYLED_DLL.SendCollectionData_Net(text, ip, nNetProtocol, UID, colour, font, size);

                    nContentType = Constants.CONTENT_TYPE_COLLECTION_DATA;
                    szFormText = "实时采集内容下发【网络 TCP】";                     
                }
                else if (radioButtonPD.Checked)
                {
                    nRetCode = QYLED_DLL.SendLineUp_Net(text, ip, nNetProtocol, stoptime, colour, UID, flash);
                    //nRetCode = QYLED_DLL.SendLineUp2_Net(text, ip, nNetProtocol, stoptime, colour, UID, flash, 16);

                    nContentType = Constants.CONTENT_TYPE_LINE_UP;
                    szFormText = "排队叫号内容下发【网络 TCP】";   
                }
                else if (radioButtonImg.Checked)
                {
                    nRetCode = QYLED_DLL.SendImageGroup_Net(imgpath, ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, 1, save, 1, 1);

                    nContentType = Constants.CONTENT_TYPE_IMAGE_GROUP;
                    szFormText = "图片组下发【网络 TCP】";  
                }
                else if (rtbCoordinateText.Checked)
                {
                    //UDP下发坐标文本 
                    nRetCode = QYLED_DLL.SendCoordinateText_Net(text, ip, nNetProtocol, nXPos, nYPos, colour, font, size);

                    nContentType = Constants.CONTENT_TYPE_COORDINATETEXT;
                    szFormText = "坐标文本内容下发【网络 TCP】";
                }
                else if (rtbMediaPlay.Checked)
                {   
                    nRetCode = QYLED_DLL.SendMultimedia_Net(imgpath, ip, nNetProtocol, bAreaPlay, nXPos, nYPos, width, higth, ledcolour, nMode, stoptime);

                    nContentType = Constants.CONTENT_TYPE_MEDIA;
                    szFormText = "同步播放【网络 TCP】";  
                }
            }
            //串口发送
            else if (radioButtonCOM.Checked)
            { 
                //RS232
                nCommMode = Constants.COMM_MODE_RS232;
                if (radioButtonNB.Checked)
                {
                    nRetCode = QYLED_DLL.SendInternalText_Com(text, rsadr, rstype, comport, baud, width, higth, UID, ledcolour, style, speed, stoptime, colour, font, size, updatestyle, save, nRotateMode);
                                        
                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "内码文字内容下发【串口 RS232】"; 
                }
                else if (radioButtonCJ.Checked)
                {
                    nRetCode = QYLED_DLL.SendCollectionData_Com(text, rsadr, rstype, comport, baud, UID, colour, font, size);
                                        
                    nContentType = Constants.CONTENT_TYPE_COLLECTION_DATA;
                    szFormText = "实时采集内容下发【串口 RS232】";                     
                }
                else if (radioButtonImg.Checked)
                {                  
                    nRetCode = QYLED_DLL.SendImageGroup_Com(imgpath, rsadr, rstype, comport, baud, width, higth, UID, ledcolour, style, speed, stoptime, 1, save, 1, 1);

                    nContentType = Constants.CONTENT_TYPE_IMAGE_GROUP;
                    szFormText = "图片组下发【串口 RS232】";
                }
                else if (rtbCoordinateText.Checked)
                {
                    string[] strContentArray = new string[] { "坐标文本1", "坐标文本2", "坐标文本3" };    
                    int intCount = strContentArray.Length;
                    for (int i = 0; i < intCount; i++)
                    {
                        nRetCode = QYLED_DLL.SendMulCoordinateText_Com(strContentArray[i], rsadr, rstype, comport, baud, 0, i * 16, colour, font, size, i + 1, intCount);
                    }

                    nContentType = Constants.CONTENT_TYPE_COORDINATETEXT;
                    szFormText = "多条坐标文本内容下发【串口 RS232】";
                }
            }
            else if (radioButton485.Checked)
            {
                //RS485
                rstype = 2;
                nCommMode = Constants.COMM_MODE_RS485;
                if (radioButtonNB.Checked)
                {             
                    nRetCode = QYLED_DLL.SendInternalText_Com(text, rsadr, rstype, comport, baud, width, higth, UID, ledcolour, style, speed, stoptime, colour, font, size, updatestyle, save, nRotateMode);

                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "内码文字内容下发【串口 RS485】";                      
                }
                else if (radioButtonCJ.Checked)
                { 
                    nRetCode = QYLED_DLL.SendCollectionData_Com(text, rsadr, rstype, comport, baud, UID, colour, font, size);
                   
                    nContentType = Constants.CONTENT_TYPE_COLLECTION_DATA;
                    szFormText = "实时采集内容下发【串口 RS485】";     
                }
                else if (rtbCoordinateText.Checked)
                {
                    string[] strContentArray = new string[] { "坐标文本1", "坐标文本2", "坐标文本3" };
                    int intCount = strContentArray.Length;
                    for (int i = 0; i < intCount; i++)
                    {
                        nRetCode = QYLED_DLL.SendMulCoordinateText_Com(strContentArray[i], rsadr, rstype, comport, baud, 0, i * 16, colour, font, size, i + 1, intCount);
                    }

                    nContentType = Constants.CONTENT_TYPE_COORDINATETEXT;
                    szFormText = "多条坐标文本内容下发【串口 RS485】";
                }
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        private void btnText2Image_Click(object sender, EventArgs e)
        {
            QYLED_DLL.SetTimeOut(int.Parse(tbSendTimeOut.Text), int.Parse(tbReceiveTimeOut.Text));

            ip = textBoxIP.Text.Trim();                         //ip地址
            ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口
            comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
            baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
            ledcolour = comboBoxLEDcolour.SelectedIndex;        //led颜色

            nXPos = Convert.ToInt32(tbXPos.Text);         //X坐标
            nYPos = Convert.ToInt32(tbYPos.Text);         //Y坐标
            width = Convert.ToInt32(textBoxWidth.Text);         //区域宽度
            higth = Convert.ToInt32(textBoxHigth.Text);         //区域高度

            colour = comboBoxColour.SelectedIndex + 1;          //显示颜色
            font = comboBoxFont.SelectedIndex;              //字体
            size = comboBoxSize.SelectedIndex;                  //字号
            style = comboBoxStyle.SelectedIndex;                //显示方式
            speed = comboBoxSpeed.SelectedIndex;                //移动速度
            stoptime = comboBoxStopTime.SelectedIndex;          //停留时间
            updatestyle = comboBoxUpdateStyle.SelectedIndex + 1;//更新方式
            timeformat = comboBoxTimeFormat.SelectedIndex + 1;    //时间格式
            showformat = comboBoxShowFormat.SelectedIndex + 1;    //显示格式
            //text = textBoxText.Text.Trim();                            //显示内容
            text = textBoxText.Text;                            //显示内容
            UID = Convert.ToInt32(textBoxUID.Text.Trim());      //素材uid
            imgpath = textBoxImgPath.Text.Trim();               //图片路径
            rsadr = textBoxRSAdr.Text;                          //485地址

            nRotateMode = cmbRotateMode.SelectedIndex;

            save = false;
            if (comboBoxSave.SelectedIndex == 0)                //掉电保存
            {
                save = true;
            }
            flash = false;
            if (comboBoxFlash.SelectedIndex == 0)               //排队叫号闪烁
            {
                flash = true;
            }

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";            

            //UDP发送
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = 1;
                if (radioButtonNB.Checked)
                {
                    nRetCode = QYLED_DLL.SendInternalTextEx_Net(text, ip, nNetProtocol, width, higth, UID, ledcolour,
                                                                                                    comboBoxFont.Text.Trim(), size, colour, style, speed, stoptime, updatestyle, save, "E:\\");

                    //string TshowContent, string TcardIP, int TnetProtocol, int TareaWidth, int TareaHigth, int Tuid, int TscreenColor,
            //string TfontName, int TfontSize, int TfontColor, int TshowStyle, int TshowSpeed, int TstopTime, int TupdateStyle, bool TpowerOffSave, string TfileDir

                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "文字处理下发【网络 UDP】";                                 
                }
            }
            //TCP发送
            else if (radioButtonTCPServer.Checked || radioButtonTCPClient.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                if (radioButtonTCPClient.Checked)
                {
                    nNetProtocol = Constants.COMM_MODE_TCP_SERVER;
                    nCommMode = Constants.COMM_MODE_TCP_SERVER;
                }

                if (radioButtonNB.Checked)
                {
                    nRetCode = QYLED_DLL.SendInternalTextEx_Net(text, ip, nNetProtocol, width, higth, UID, ledcolour,
                                                                                                    comboBoxFont.Text.Trim(), size, colour, style, speed, stoptime, updatestyle, save, "E:\\");
                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "文字处理下发【网络 TCP】";
                }
            }
            //串口发送
            else if (radioButtonCOM.Checked)
            {
                //RS232
                nCommMode = Constants.COMM_MODE_RS232;
                if (radioButtonNB.Checked)
                {
                    nRetCode = QYLED_DLL.SendInternalTextEx_Com(text, rsadr, rstype, comport, baud, width, higth, UID, ledcolour,
                                                                                                        comboBoxFont.Text.Trim(), size, colour, style, speed, stoptime, updatestyle, save, "E:\\");

                    nContentType = Constants.CONTENT_TYPE_INTERNAL_TEXT;
                    szFormText = "文字处理下发【串口 RS232】";
                }
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();


            }
        }

        //显示页点播（需建模板）
        private void buttonShowPage_Click_1(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                         //ip地址
            ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口号
            comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
            baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
            pagenumber = comboBoxPageNumber.SelectedIndex + 1;  //显示页序号

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";

            if (radioButtonUDP.Checked)
            {
                //UDP 显示页点播
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, pagenumber);

                nContentType = Constants.CONTENT_TYPE_SHOW_PAGE_PLAY;
                szFormText = "显示页点播【网络 UDP】";   
            }
            else if (radioButtonTCPServer.Checked)
            {
                //TCP 显示页点播
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, pagenumber);

                nContentType = Constants.CONTENT_TYPE_SHOW_PAGE_PLAY;
                szFormText = "显示页点播【网络 TCP】";                 
            }
            else if (radioButtonCOM.Checked)
            {
                //RS232 显示页点播
                nCommMode = Constants.COMM_MODE_RS232;
                nRetCode = QYLED_DLL.PlayShowPage_Com(rsadr, rstype, comport, baud, pagenumber);

                nContentType = Constants.CONTENT_TYPE_SHOW_PAGE_PLAY;
                szFormText = "显示页点播【串口 RS232】"; 
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //设置亮度
        private void buttonSetBright_Click_1(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                     //ip地址
            ipport = Convert.ToInt32(textBoxIPPort.Text);   //tcp服务端口
            int priority = 3;                               //亮度优先级固定取值
            brightness = comboBoxBrightness.SelectedIndex;  //亮度等级

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";

            if (radioButtonUDP.Checked)
            {
                //UDP 设置亮度
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.SetBright_Net(ip, nNetProtocol, priority, brightness);

                nContentType = Constants.CONTENT_TYPE_BRIGHT_SET;
                szFormText = "亮度设置【网络 UDP】";                
            }
            else if (radioButtonTCPServer.Checked)
            {
                //TCP 设置亮度
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.SetBright_Net(ip, nNetProtocol, priority, brightness);

                nContentType = Constants.CONTENT_TYPE_BRIGHT_SET;
                szFormText = "亮度设置【网络 TCP】";           
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //软开屏
        private void buttonStartPlay_Click(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                     //ip地址

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.StartPlay_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_PLAY_CONTROL;
                szFormText = "软开屏【网络 UDP】";                  
            }
            else if (radioButtonTCPServer.Checked)
            {
                //TCP 软开屏
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.StartPlay_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_PLAY_CONTROL;
                szFormText = "软开屏【网络 TCP】";                
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //软关屏
        private void buttonStopPlay_Click(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                     //ip地址

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.StopPlay_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_PLAY_CONTROL;
                szFormText = "软关屏【网络 UDP】";                   
            }
            else if (radioButtonTCPServer.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.StopPlay_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_PLAY_CONTROL;
                szFormText = "软关屏【网络 TCP】";     
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //校准时间
        private void buttonSendDateTime_Click_1(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                     //ip地址

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                //UDP校准时间
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.TimeCheck_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_TIME_CHECK;
                szFormText = "时间校准【网络 UDP】";                 
            }
            else if (radioButtonTCPServer.Checked)
            {
                //TCP校准时间
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.TimeCheck_Net(ip, nNetProtocol);

                nContentType = Constants.CONTENT_TYPE_TIME_CHECK;
                szFormText = "时间校准【网络 TCP】";                     
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        //语音播放
        private void buttonPlayVoice_Click(object sender, EventArgs e)
        {
            try
            {
                ip = textBoxIP.Text.Trim();                         //ip地址
                ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口号
                comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
                baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
                voicetext = textBoxVoiceText.Text.Trim();           //语音播放内容
                rsadr = textBoxRSAdr.Text.Trim();                   //485地址

                int nCommMode = Constants.COMM_MODE_UDP;
                int nContentType = Constants.CONTENT_TYPE_NONE;
                string szFormText = "";
                if (radioButtonUDP.Checked)
                {
                    //udp语音播放
                    nNetProtocol = Constants.COMM_MODE_UDP;
                    nRetCode = 1;
                    //转发串口1
                    if (radioButtonZFCOM1.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, rsadr, nNetProtocol, 1);
                    }
                    //转发串口2
                    else if (radioButtonZFCOM2.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, rsadr, nNetProtocol, 2);
                    }
                    //转发485
                    else if (radioButtonZF485.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, rsadr, nNetProtocol, 3);
                    }
                    //不转发
                    else if (radioButtonBZF.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, rsadr, nNetProtocol, 1);
                    }

                    nContentType = Constants.CONTENT_TYPE_VOICE_PLAY;
                    szFormText = "语音播放【网络 UDP】";  
                }
                else if (radioButtonTCPServer.Checked)
                {
                    //TCP语音播放
                    nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                    nCommMode = Constants.COMM_MODE_TCP_CLIENT;

                    nRetCode = 1;
                    //转发串口1
                    if (radioButtonZFCOM1.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, "", nNetProtocol, 1);
                    }
                    //转发串口2
                    else if (radioButtonZFCOM2.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, "", nNetProtocol, 2);
                    }
                    //转发485
                    else if (radioButtonZF485.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, "", nNetProtocol, 3);
                    }
                    //不转发
                    else if (radioButtonBZF.Checked)
                    {
                        nRetCode = QYLED_DLL.PlayVoice_Net(voicetext, ip, "", nNetProtocol, 1);
                    }

                    nContentType = Constants.CONTENT_TYPE_VOICE_PLAY;
                    szFormText = "语音播放【网络 TCP】";                      
                }
                else if (radioButtonCOM.Checked)
                {
                    //com语音播放
                    rstype = 1;
                    nCommMode = Constants.COMM_MODE_RS232;
                    QYLED_DLL.PlayVoice_Com(voicetext, rsadr, rstype, comport, baud);

                    nContentType = Constants.CONTENT_TYPE_VOICE_PLAY;
                    szFormText = "语音播放【串口 RS232】";       
                }
                else if (radioButton485.Checked)
                {
                    //485语音播放
                    rstype = 2;
                    nCommMode = Constants.COMM_MODE_RS485;
                    nRetCode = 1;
                    nRetCode = QYLED_DLL.PlayVoice_Com(voicetext, rsadr, rstype, comport, baud);

                    nContentType = Constants.CONTENT_TYPE_VOICE_PLAY;
                    szFormText = "语音播放【串口 RS485】";    
                }

                if (nContentType != Constants.CONTENT_TYPE_NONE)
                {
                    _formTips.showTips(nRetCode, nContentType, nCommMode);
                    _formTips.Text = szFormText;
                    _formTips.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        /// 【summary】
        /// 实时采集批量下发
        /// 【/summary】
        /// 【param name="sender"】【/param】
        /// 【param name="e"】【/param】
        private void btnMulCollection_Click(object sender, EventArgs e)
        {
            //只支持UDP
            //批量发3条，要分别调用三次：参数分别为：
            //DataIndex=1，DataCount=3；
            //DataIndex=2，DataCount=3；
            //DataIndex=3，DataCount=3；
            //当DataIndex=DataCount等于总条数的话才真正下发数据
            ip = textBoxIP.Text.Trim();
            ledcolour = comboBoxLEDcolour.SelectedIndex;        //led颜色

            if (radioButtonUDP.Checked)
            {
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("第1条实时采集", ip, 1, 1, ledcolour, 1, 1, 1, 1, 3);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("第2条实时采集", ip, 1, 2, ledcolour, 1, 1, 1, 2, 3);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("第3条实时采集", ip, 1, 3, ledcolour, 1, 1, 1, 3, 3);

                _formTips.showTips(nRetCode, Constants.CONTENT_TYPE_COLLECTION_DATA, Constants.COMM_MODE_UDP);
                _formTips.Text = "多条实时采集内容下发【网络 UDP】";
                _formTips.ShowDialog();
            }
        }

        /// 【summary】
        /// 内码文字批量下发
        /// 【/summary】
        /// 【param name="sender"】【/param】
        /// 【param name="e"】【/param】
        private void btnMulText_Click(object sender, EventArgs e)
        {
            //只支持UDP
            //批量发3条，要分别调用三次：参数分别为：
            //DataIndex=1，DataCount=3；
            //DataIndex=2，DataCount=3；
            //DataIndex=3，DataCount=3；
            //当DataIndex=DataCount等于总条数的话才真正下发数据
            ip = textBoxIP.Text.Trim();

            if (radioButtonUDP.Checked)
            {
                //result = QYLED_DLL.SendMulInternalText_Net("江苏南京 日用电器 10T 10M车 10日", ip, 1, 64, 16, 1, 2, 1, 1, 1, 1, 1, 1, 2, false, 1, 6);
                //result = QYLED_DLL.SendMulInternalText_Net("江苏南通 日用百货 11T 11M车 11日", ip, 1, 64, 16, 2, 2, 1, 1, 1, 1, 1, 1, 2, false, 2, 6);
                //result = QYLED_DLL.SendMulInternalText_Net("江苏无锡 新鲜蔬菜 12T 12M车 12日", ip, 1, 64, 16, 3, 2, 1, 1, 1, 1, 1, 1, 2, false, 3, 6);
                //result = QYLED_DLL.SendMulInternalText_Net("江苏武进 快递业务 13T 13M车 13日", ip, 1, 64, 16, 4, 2, 1, 1, 1, 1, 1, 1, 2, false, 4, 6);
                //result = QYLED_DLL.SendMulInternalText_Net("江苏苏州 散装货物 14T 14M车 14日", ip, 1, 64, 16, 5, 2, 1, 1, 1, 1, 1, 1, 2, false, 5, 6);
                //result = QYLED_DLL.SendMulInternalText_Net("江苏泰州 工厂设备 15T 15M车 15日", ip, 1, 64, 16, 6, 2, 1, 1, 1, 1, 1, 1, 2, false, 6, 6);
                nRetCode = QYLED_DLL.SendMulInternalText_Net("第1行内码文字", ip, 1, 64, 16, 1, 1, 9, 1, 1, 1, 1, 1, 2, false, 0, 1, 3);
                nRetCode = QYLED_DLL.SendMulInternalText_Net("第2行内码文字", ip, 1, 64, 16, 2, 1, 9, 1, 1, 1, 1, 1, 2, false, 0, 2, 3);
                nRetCode = QYLED_DLL.SendMulInternalText_Net("第3行内码文字", ip, 1, 64, 16, 3, 1, 1, 1, 1, 1, 1, 1, 2, false, 0, 3, 3);

                _formTips.showTips(nRetCode, Constants.CONTENT_TYPE_INTERNAL_TEXT, Constants.COMM_MODE_UDP);
                _formTips.Text = "多条内码文字内容下发【网络 UDP】";
                _formTips.ShowDialog();               
            }
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {
        }

        private void btnControll_Click(object sender, EventArgs e)
        {
             ip = textBoxIP.Text.Trim();                     //ip地址
            int nCircuitNo = comboBoxCircuitNo.SelectedIndex + 1;
            int nSwitchStatus = comboBoxSwitchStatus.SelectedIndex + 1;

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = 1;
                nRetCode = QYLED_DLL.RelaySwitch_Net(ip, nNetProtocol, nCircuitNo, nSwitchStatus);

                nContentType = Constants.CONTENT_TYPE_SWITCH_CONTROL;
                szFormText = "继电器控制【网络 UDP】";  
            }
            else if (radioButtonTCPServer.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = 1;
                nRetCode = QYLED_DLL.RelaySwitch_Net(ip, nNetProtocol, nCircuitNo, nSwitchStatus);

                nContentType = Constants.CONTENT_TYPE_SWITCH_CONTROL;
                szFormText = "继电器控制【网络 TCP】";                  
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        private void btnRelay_Click(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                     //ip地址
            int nCircuitNo = comboBoxCircuitNo.SelectedIndex + 1;
            int nDelayTime = comboBoxDelayTime.SelectedIndex + 1;

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_UDP;   
                nRetCode = QYLED_DLL.RelayDelay_Net(ip, nNetProtocol, nCircuitNo, nDelayTime);

                nContentType = Constants.CONTENT_TYPE_SWITCH_CONTROL;
                szFormText = "继电器控制【网络 UDP】";               
            }
            else if (radioButtonTCPServer.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;     
                nRetCode = QYLED_DLL.RelayDelay_Net(ip, nNetProtocol, nCircuitNo, nDelayTime);

                nContentType = Constants.CONTENT_TYPE_SWITCH_CONTROL;
                szFormText = "继电器控制【网络 TCP】";               
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }               

        private void button2_Click(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                         //ip地址
            ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口号
            comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
            baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
            pagenumber = comboBoxPageNumber.SelectedIndex + 1;  //显示页序号

            int nCommMode = Constants.COMM_MODE_UDP;
            int nContentType = Constants.CONTENT_TYPE_NONE;
            string szFormText = "";
            if (radioButtonUDP.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_UDP;
                nRetCode = QYLED_DLL.PlayImageGroup_Net(ip, nNetProtocol, 0, 1, Int32.Parse(tbAreaNo.Text), Int32.Parse(tbImgStartNo.Text), Int32.Parse(tbImgNum.Text), 9, 1, 1, true, false);

                nContentType = Constants.CONTENT_TYPE_IMAGE_PLAY;
                szFormText = "图片组点播【网络 UDP】";  
            }
            else if (radioButtonTCPServer.Checked)
            {
                nNetProtocol = Constants.COMM_MODE_TCP_CLIENT;
                nCommMode = Constants.COMM_MODE_TCP_CLIENT;
                nRetCode = QYLED_DLL.PlayImageGroup_Net(ip, nNetProtocol, 0, 1, Int32.Parse(tbAreaNo.Text), Int32.Parse(tbImgStartNo.Text), Int32.Parse(tbImgNum.Text), 9, 1, 1, true, false);


                nContentType = Constants.CONTENT_TYPE_IMAGE_PLAY;
                szFormText = "图片组点播【网络 TCP】";  
            }
            else if (radioButtonCOM.Checked)
            {
                nCommMode = Constants.COMM_MODE_RS232; 
                nRetCode = 1;
                nRetCode = QYLED_DLL.PlayShowPage_Com(rsadr, rstype, comport, baud, pagenumber);

                nContentType = Constants.CONTENT_TYPE_IMAGE_PLAY;
                szFormText = "图片组点播【串口 RS232】";                 
            }

            if (nContentType != Constants.CONTENT_TYPE_NONE)
            {
                _formTips.showTips(nRetCode, nContentType, nCommMode);
                _formTips.Text = szFormText;
                _formTips.ShowDialog();
            }
        }

        private void btnBindIP_Click(object sender, EventArgs e)
        {
            QYLED_DLL.BindNetworkCard(tbNetworkCardIP.Text, int.Parse(tbPort.Text));
        }

        private void btnSendBarContent_Click(object sender, EventArgs e)
        {
            if (radioButtonCOM.Checked)
            {
                int nRSType = 1;
                int nComPort = comboBoxCOMPort.SelectedIndex + 1;
                int nBaudRate = Convert.ToInt32(comboBoxBaudRate.Text);

                string szShowContent = tbBarContent.Text.Trim();
                int nAddress = Convert.ToInt32(tbBarAddress2.Text);

                int nFontColor = cbBarFontColor.SelectedIndex;
                int nChnNum = Convert.ToInt32(tbChnNum.Text);
                int nTopScreenNum = Convert.ToInt32(tbTopScreenNum.Text);
                int nSubScreenNo = Convert.ToInt32(tbSubScreenNo.Text);
                int nTopLoopTimes = Convert.ToInt32(tbTopLoopTimes.Text);
                int nMoveStyleIn = cbBarMoveStyleIn.SelectedIndex;
                int nMoveStyleOut = cbBarMoveStyleOut.SelectedIndex;
                int nStopTime = Convert.ToInt32(tbBarStopTime.Text);
                int nShowSpeed = cbBarShowSpeed.SelectedIndex;
                int nFlashStart = Convert.ToInt32(tbFlashStart.Text);
                int nFlashLen = Convert.ToInt32(tbFlashLen.Text);

                bool bPowerOffSave = false;
                if (cbBarPowerOffSave.SelectedIndex == 0)
                {
                    bPowerOffSave = true;
                }

                nRetCode = QYLED_DLL.SendContent2BarScreen_Com(szShowContent, nRSType, nComPort, nBaudRate, nAddress,
                                                                                             nFontColor, nChnNum, nTopScreenNum, nSubScreenNo, nTopLoopTimes,
                                                                                            nMoveStyleIn, nMoveStyleOut, nStopTime, nShowSpeed, nFlashStart, nFlashLen, bPowerOffSave);

                _formTips.showTips(nRetCode, Constants.CONTENT_TYPE_BAR_SCREEN,  Constants.COMM_MODE_RS232);
                _formTips.Text = "条屏内容下发【串口 RS232】";
                _formTips.ShowDialog(); 
            }
            else
            {
                MessageBox.Show("只支持串口通讯！", "通讯");
            }
        }

        private void btnModifyBarParams_Click(object sender, EventArgs e)
        {
            /**
            if (radioButtonCOM.Checked)
            { 
                 int nRSType = 1;
                 int nComPort = comboBoxCOMPort.SelectedIndex + 1;        
                 int nBaudRate = Convert.ToInt32(comboBoxBaudRate.Text);      
                 int nScreenColor = cbBarScreenColor.SelectedIndex;  
                 int nAddress = Convert.ToInt32(tbBarAddress.Text);

                 int nChnNumWidth = Convert.ToInt32(tbMaxchnNum.Text);
                int nChnNumHeight = 1;
                int nBrightness = cbBarBrightness.SelectedIndex;
                bool bCommFailTips = false;
                if (cbCommFailTips.SelectedIndex == 1)                
                {
                    bCommFailTips = true;
                }

                int nRet = QYLED_DLL.SetParams4BarScreen_Com(nRSType, nComPort, nBaudRate, nAddress, nChnNumWidth,
                                                                                         nChnNumHeight, nScreenColor, nBrightness, bCommFailTips);
                if (nRet == 0)
                {
                    MessageBox.Show("修改成功！", "参数修改");
                }
                else
                {
                    MessageBox.Show("修改失败！", "参数修改");
                }
            }
            else
            {
                MessageBox.Show("只支持串口通讯！", "通讯");
            }**/
        }


        int _nIndex1 = 0;
        int _nIndex2 = 0;
        private void tAutoSend_Tick(object sender, EventArgs e)
        {
            try
            {
                int nRetCode = 0;

                QYLED_DLL.SetTimeOut(int.Parse(tbSendTimeOut.Text), int.Parse(tbReceiveTimeOut.Text));

                ip = textBoxIP.Text.Trim();                         //ip地址
                ipport = Convert.ToInt32(textBoxIPPort.Text);       //tcp服务端口
                comport = comboBoxCOMPort.SelectedIndex + 1;        //com端口
                baud = Convert.ToInt32(comboBoxBaudRate.Text);      //波特率
                ledcolour = comboBoxLEDcolour.SelectedIndex;        //led颜色

                nXPos = Convert.ToInt32(tbXPos.Text);         //X坐标
                nYPos = Convert.ToInt32(tbYPos.Text);         //Y坐标
                width = Convert.ToInt32(textBoxWidth.Text);         //区域宽度
                higth = Convert.ToInt32(textBoxHigth.Text);         //区域高度

                colour = comboBoxColour.SelectedIndex + 1;          //显示颜色
                font = comboBoxFont.SelectedIndex;              //字体
                size = comboBoxSize.SelectedIndex;                  //字号
                style = comboBoxStyle.SelectedIndex;                //显示方式
                speed = comboBoxSpeed.SelectedIndex;                //移动速度
                stoptime = comboBoxStopTime.SelectedIndex;          //停留时间
                updatestyle = comboBoxUpdateStyle.SelectedIndex + 1;//更新方式
                timeformat = comboBoxTimeFormat.SelectedIndex + 1;    //时间格式
                showformat = comboBoxShowFormat.SelectedIndex + 1;    //显示格式
                //text = textBoxText.Text.Trim();                            //显示内容
                text = textBoxText.Text;                            //显示内容
                UID = Convert.ToInt32(textBoxUID.Text.Trim());      //素材uid
                //imgpath = textBoxImgPath.Text.Trim();               //图片路径
                rsadr = textBoxRSAdr.Text;                          //485地址

                save = false;
                if (comboBoxSave.SelectedIndex == 0)                //掉电保存
                {
                    save = true;
                }
                flash = false;
                if (comboBoxFlash.SelectedIndex == 0)               //排队叫号闪烁
                {
                    flash = true;
                }

                //同步播放
                //false--不支持部分区域播放；true--支持部分区域播放
                bool bAreaPlay = true;
                //0--异步；1--同步
                int nMode = 1;

                /**
                if (radioButtonPD.Checked)
                {
                    if (sbContent.ToString().Length >= _nIndex1 - 3)
                    {
                        textBoxText.Text = textBoxText.Text + sbContent.ToString().Substring(_nIndex1, 1);
                        _nIndex1 += 1;
                        nRetCode = QYLED_DLL.SendLineUp2_Net(textBoxText.Text, ip, nNetProtocol, stoptime, colour, UID, flash, 16);
                    }
                }
                else
                {
                    string szIP = textBoxIP.Text.Trim();                         //ip地址                
                    string szVoiceContentext = textBoxVoiceText.Text.Trim();           //语音播放内容

                    int nRet = QYLED_DLL.PlayVoice_Net(szVoiceContentext, szIP, "0", 1, 2);
                }
                 * **/

                if (radioButtonUDP.Checked)
                {
                    _nImgIndex++;
                    if (_nImgIndex >= _arrImgPath.Length)
                    {
                        _nImgIndex = 0;
                    }

                    if (radioButtonImg.Checked)
                    {
                        nRetCode = QYLED_DLL.SendImageGroup_Net(_arrImgPath[_nImgIndex], ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, 2, save, 1, 1);
                    }
                    else if (rtbMediaPlay.Checked)
                    {
                        nRetCode = QYLED_DLL.SendMultimedia_Net(_arrImgPath[_nImgIndex], ip, nNetProtocol, bAreaPlay, nXPos, nYPos, width, higth, ledcolour, nMode, stoptime);

                        int nOtherImgIndex = _nImgIndex + 1;
                        if (nOtherImgIndex >= _arrImgPath.Length)
                        {
                            nOtherImgIndex = 0;
                        }
                        nRetCode = QYLED_DLL.SendMultimedia_Net(_arrImgPath[nOtherImgIndex], ip, nNetProtocol, bAreaPlay, nXPos, nYPos + 64, width, higth, ledcolour, nMode, stoptime);
                    }
                    else
                    {
                        //nRetCode = QYLED_DLL.SendInternalText_Net("序号【" + (_nIndex2++) + "】内码文字滚动", ip, nNetProtocol, width, higth, UID, ledcolour, style, speed, stoptime, colour, font, size, updatestyle, false);

                        //nRetCode = QYLED_DLL.SendMulCollectionData_Net("序号" + (_nIndex1++) + "实时数据", ip, 1, 1, ledcolour, 2, 1, 1, 1, 3);
                        //nRetCode = QYLED_DLL.SendMulCollectionData_Net("序号" + (_nIndex1++) + "实时数据", ip, 1, 2, ledcolour, 3, 2, 1, 2, 3);
                        //nRetCode = QYLED_DLL.SendMulCollectionData_Net("序号" + (_nIndex1++) + "实时数据", ip, 1, 3, ledcolour, 1, 3, 1, 3, 3);

                        //Thread.Sleep(100);

                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字从左向右滚动连续显示", ip, 1, 64, 16, 1, 1, 1, 1, 0, 1, 1, 3, 2, false, 0, 1, 3);
                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字从下向上滚动连续显示", ip, 1, 64, 16, 2, 1, 3, 2, 0, 2, 2, 3, 2, false, 0, 2, 3);
                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字静止显示", ip, 1, 64, 16, 3, 1, 9, 1, 1, 3, 1, 1, 2, false, 0, 3, 3);

                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字静止", ip, 1, 64, 16, 1, 1, 9, 1, 1, 1, 1, 1, 2, false, 1, 3);
                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字静止", ip, 1, 64, 16, 2, 1, 9, 1, 1, 2, 2, 1, 2, false, 2, 3);
                        //nRetCode = QYLED_DLL.SendMulInternalText_Net("序号【" + (_nIndex2++) + "】内码文字静止", ip, 1, 64, 16, 3, 1, 9, 1, 1, 3, 1, 1, 2, false, 3, 3);


                        int intNum = 8;
                        int intIndex = 1;

                        nRetCode = QYLED_DLL.SendMulInternalText_Net($"限员管理_{_nIndex1++}", ip, 1, 192, 16, 1, 1, 9, 1, 1, 2, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net("限员", ip, 1, 32, 16, 2, 1, 9, 1, 1, 2, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net("进入", ip, 1, 32, 16, 3, 1, 9, 1, 1, 2, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net(     "1", ip, 1, 16, 16, 5, 1, 9, 1, 1, 1, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net(    "人", ip, 1, 16, 16, 8, 1, 9, 1, 1, 2, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net(    "人", ip, 1, 16, 16, 9, 1, 9, 1, 1, 2, 1, 0, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net(    "人", ip, 1, 16, 16, 10, 1, 9, 1, 1, 1, 0, 1, 1, false, 0, intIndex++, intNum);
                        nRetCode = QYLED_DLL.SendMulInternalText_Net("张  三"+Environment.NewLine + "李  四" + Environment.NewLine + "王  五" + Environment.NewLine + "邓有六" + Environment.NewLine, ip, 1, 64, 48, 11, 1, 9, 1, 1, 1, 1, 1, 1, false, 0, intIndex++, intNum);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAutoSend_Click(object sender, EventArgs e)
        {
            //udp下发排队叫号内容
            /**   
            string[] arrShowContent = new string[] { "01号车  进  1号站", "02号车  进  2号站", "03号车  进  3号站", "04号车  进  4号站" };

            int nWidth = 160 / 8;
            int nRowLen = 0;
            int nSpaceLen = 0;

            string szLineUp = "";
            string szSpace = "";   
            for (int i = 0; i 【 arrShowContent.Length;i++ )
            {
                nRowLen = Encoding.Default.GetByteCount(arrShowContent[i]);
                nSpaceLen = (nWidth - nRowLen) / 2;   
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j 【 nSpaceLen; j++)
                {
                    sb.Append(" ");
                }
                szSpace = sb.ToString();
               
                if (((nWidth - nRowLen) % 2) 】 0)
                {
                    szLineUp += szSpace + arrShowContent[i] + szSpace + " ";
                }
                else
                {
                    szLineUp += szSpace + arrShowContent[i] + szSpace ;
                }
            }

            result = QYLED_DLL.SendLineUp_Net(szLineUp, "192.168.0.113", 1, 1, 1, 1, false);
            //result = QYLED_DLL.SendLineUp_Net("123" + "\r\n" + "456", "192.168.0.113", 1, 1, 1, 1, false);
            //result = QYLED_DLL.SendLineUp_Net("123456789", "192.168.0.113", 1, 1, 1, 1, false);
            if (result == Constants.SUCCESS_SEND)
            {
                MessageBox.Show("下发成功！", "udp发送排队叫号内容");
            }
            else
            {
                MessageBox.Show("下发失败！", "udp发送排队叫号内容");
            }
            **/

            //UDP 显示页点播
            //nNetProtocol = Constants.COMM_MODE_UDP;
            //nRetCode = QYLED_DLL.PlayShowPage_Net( textBoxIP.Text.Trim(), nNetProtocol, 1);

            tAutoSend.Interval = int.Parse(tbAutoInterval.Text);
            _nIndex1 = 0;
            _nIndex2 = 0;
            if (tAutoSend.Enabled)
            {
                tAutoSend.Enabled = false;
                btnAutoSend.Text = "自动发送";
            }
            else
            {
                tAutoSend.Enabled = true;    
                btnAutoSend.Text = "停止发送";
            }
        }

        private void btnPageMulData_Click(object sender, EventArgs e)
        {
            _nControlType = 0;
            if (btnPageMulData.Text.Equals("暂停"))
            {
                tAutoPlay.Enabled = false;
                btnPageMulData.Text = "多项实时采集+点播";
            }
            else
            {
                tAutoPlay.Enabled = true;
                tAutoPlay.Interval = int.Parse(tbAutoInterval.Text);
                btnPageMulData.Text = "暂停";
            }
        }

        private void btnPlayTest_Click(object sender, EventArgs e)
        {
            _nControlType = 1;
            if (btnPlayTest.Text.Equals("暂停"))
            {
                tAutoPlay.Enabled = false;
                btnPlayTest.Text = "显示页+图片点播";                
            }
            else
            {
                tAutoPlay.Enabled = true;
                tAutoPlay.Interval = int.Parse(tbAutoInterval.Text);
                btnPlayTest.Text = "暂停";         
            }
        }

        private void btnLightControl_Click(object sender, EventArgs e)
        {
            _nControlType = 2;
            if (btnLightControl.Text.Equals("暂停"))
            {
                tAutoPlay.Enabled = false;
                btnLightControl.Text = "灯条控制+图片点播";
            }
            else
            {
                tAutoPlay.Enabled = true;
                tAutoPlay.Interval = int.Parse(tbAutoInterval.Text);
                btnLightControl.Text = "暂停";
            }
        }

        private void tAutoPlay_Tick(object sender, EventArgs e)
        {
            ip = textBoxIP.Text.Trim();                         //ip地址
            nNetProtocol = Constants.COMM_MODE_UDP;  //UDP 显示页点播

            int nUid = 1;
            int nSleepTime = 50;

            _nPageIndex++;
            if (_nPageIndex > 3)
            {
                _nPageIndex = 1;
            }

            if (_nItemIndex >= _listSendBuffer.Count)
            {
                _nItemIndex = 0;
            }

            if (_nControlType == 0)
            {
                int nTypeNo = 1;

                int nItemIndex = 1;
                int nItemNum = 7;

                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_1]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_2]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_3]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_4]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_5]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "_6]实时采集", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);
                nRetCode = QYLED_DLL.SendMulCollectionData_Net("[" + _nPageIndex + "] 显示页", ip, 1, nTypeNo++, ledcolour, 1, 1, 1, nItemIndex++, nItemNum);

                Thread.Sleep(nSleepTime);
                nRetCode = QYLED_DLL.PlayShowPage_Net(ip, nNetProtocol, _nPageIndex);
            }
            else if (_nControlType == 1)
            {
                nRetCode = QYLED_DLL.SendMulInternalText_Net("【" + (_nIndex2++) + "】内码文字", ip, nNetProtocol, 64, 16, nUid, 1, 9, 1, 1, 3, 1, 1, 2, false, 0, 1, 1);

                Thread.Sleep(nSleepTime);
                nRetCode = QYLED_DLL.PlayImageGroup_Net(ip, nNetProtocol, 0, 1, 0, _nItemIndex++, 1, 9, 1, 1, true, false);
            }
            else if (_nControlType == 2)
            {
                UdpSendBuffer(_listSendBuffer[_nItemIndex], ip);

                Thread.Sleep(nSleepTime);
                nRetCode = QYLED_DLL.PlayImageGroup_Net(ip, nNetProtocol, 0, 1, 0, _nItemIndex++, 1, 9, 1, 1, true, false);
            }   
        }    

        private void tClear_Tick(object sender, EventArgs e)
        {
            ClearMemory();
        }

        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.OpenConnection("BSB-CALL001", 1, Int32.Parse(tbComPort.Text), 9600,
                                                                 1, 30000, (char)65);
            if (nRetCode == 0)
            {
                MessageBox.Show("打开串口成功！");
            }
            else
            {
                MessageBox.Show("打开串口失败！");
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.CloseConnection();
            if (nRetCode == 0)
            {
                MessageBox.Show("关闭串口成功！");
            }
            else
            {
                MessageBox.Show("关闭串口失败！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.InitLED(2, 0, 0, int.Parse(textBoxWidth.Text), int.Parse(textBoxHigth.Text), 1, int.Parse(tbChnNum.Text));
            if (nRetCode == 0)
            {
                MessageBox.Show("初始化成功！");
            }
            else
            {
                MessageBox.Show("初始化失败！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.ClearLED(2);
            if (nRetCode == 0)
            {
                MessageBox.Show("清屏成功！");
            }
            else
            {
                MessageBox.Show("清屏失败！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.SetLedShowStyle(2, "01", 1, 1, cbBarMoveStyleIn.SelectedIndex, cmbAlign.SelectedIndex);
            if (nRetCode == 0)
            {
                MessageBox.Show("设置成功！");
            }
            else
            {
                MessageBox.Show("设置失败！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.SendStrToLed(2, int.Parse(tbBarAddress2.Text), "01", 0, 1, tbBarContent.Text);
            if (nRetCode == 0)
            {
                MessageBox.Show("发送成功！");
            }
            else
            {
                MessageBox.Show("发送失败！");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int nDeviceStatus = 0;
            nRetCode = QYLED_DLL.GetDeviceStatus(ref nDeviceStatus);
            if (nRetCode == 0)
            {
                MessageBox.Show("获取成功！");
            }
            else
            {
                MessageBox.Show("获取失败！");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            nRetCode = QYLED_DLL.SendAudio(1, 1, tbBarContent.Text, int.Parse(tbBarAddress2.Text));
            if (nRetCode == 0)
            {
                MessageBox.Show("播放成功！");
            }
            else
            {
                MessageBox.Show("播放失败！");
            }
        }

        public byte[] UdpSendBuffer(byte[] byArrSendBuffer, string strCardIP)
        {
            Array.Clear(_byArrRecBuffer, 0, _byArrRecBuffer.Length);
            //_udpClient.ReceiveTimeout = 300;
            try
            {
                //使用socket发送数据
                /*************************************************************************/
                _udpSocket.BeginSendTo(
                    byArrSendBuffer,                                                            //buff[]
                    0,                                                                  //int offset
                    byArrSendBuffer.Length,                                                   //int size
                    SocketFlags.None,                                                   //SockrtFlag
                    new IPEndPoint(IPAddress.Parse(strCardIP), 8800),                         //EndPoint
                    null,                                                      //AsyncCallback
                    _udpSocket);                                                        //state
                /*************************************************************************/

            }
            catch (Exception)
            { }
            //启用异步接收数据
            try
            {
                _udpSocket.BeginReceive(_byArrRecBuffer, 0, _byArrRecBuffer.Length, SocketFlags.None, ReceiveCallback, _udpSocket);
            }
            catch (Exception)
            {
                //接收异常时，关闭socket，重新加载
                _udpSocket.Close();
                _udpSocket = new Socket(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0).Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            }
            return _byArrRecBuffer;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                int nLen = socket.EndReceive(ar);
                if (nLen > 0)
                {
                    int nIndex = 0;
                    if ((_byArrRecBuffer[nIndex] == 0xFE) && (_byArrRecBuffer[nIndex + 1] == 0x5C) && (_byArrRecBuffer[nIndex + 2] == 0x4B) && (_byArrRecBuffer[nIndex + 3] == 0x89)
                        && (_byArrRecBuffer[nIndex + 8] == 0x81) && (_byArrRecBuffer[nIndex + 17] == 0x31))
                    {
                        //MessageBox.Show("发布成功！");
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
