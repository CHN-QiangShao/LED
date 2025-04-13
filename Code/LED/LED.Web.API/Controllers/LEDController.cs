using System.Text;
using Microsoft.AspNetCore.Mvc;
using LED.DLL;

namespace LED.Web.API.Controllers;

/// <summary>
/// LED ���������ඨ�壬�̳�����ͼ֧�ֵĴ� API ������ ControllerBase�������� ApiController �� Route ����
/// </summary>
[ApiController]                 // ���� API ���������ԣ��Զ�����ģ����֤����400��Ӧ���������������� 400 ����
[Route("API/[Controller]")]		// ����·��ģ�����ԣ�ʵ��·��·��Ϊ API/LED (��������ȥ�� "Controller" ��׺)
public class LedController : ControllerBase
{
    // ���棺���˳����캯��ʱ������Ϊ null ���ֶα�������� null ֵ
    // ԭ��C# 8.0 �����ɿ������������ԣ���ĿĬ�����ÿ�Ϊ���������� <Nullable>enable</Nullable>����ʱ�������͵ı���Ĭ�ϲ���Ϊ null
    // �����������һ���ֶζ������ʺţ�����������Ϊ����ֶ��ڹ��캯�����֮ǰ���뱻����һ���� null ��ֵ�������ڹ��캯������ʱ���������ֶ�û�б���ʼ�����ͻᴥ�����棬��Ϊ��������Ϊ������Ϊ null��Υ������Ϊ null ��Լ��
    // ������� 1������ֶ�ȷʵ����Ϊ null����� ? ���η������ֶ�Ϊ null
    // ������� 2������ֶ�ȷʵ����Ϊ null��ʹ�ù��캯����ȷ���ֶα���ֵ
    private readonly LedValue _ledValue;
    private readonly ILogger<LedController>? _logger;

    /// <summary>
    /// ���캯��
    /// <br>readonly �ֶ�ֻ���ڹ��캯���г�ʼ��</br>
    /// </summary>
    /// <param name="ledValue">LedValue ʵ��</param>
    public LedController(LedValue ledValue/*, ILogger<LedController> logger*/)
    {
        _ledValue = ledValue;   // û������ ?��ȷ�� ledValue �� null
        //_logger = logger;     // ��Ϊ���� ?������ logger Ϊ null
    }

    /// <summary>
    /// API �˵㶨�壺���岢���������������飬���� SendInternalText_Net()�����ݷ��ص�״̬���ж��Ƿ�ɹ�������¼���
    /// </summary>
    /// <param name="requestBody">RequestBody ��� json ��ʽ��������</param>
    /// <returns>ResponseBody ����ֶ�</returns>
    [HttpPost("Display")]                                           // ���� HttpPost ���ԣ�              ���� POST �˵�Ϊ ip:port/API/Led/Display
    [ProducesResponseType(typeof(ResponseBody), 200)]               // ���� ProducesResponseType ���ԣ�  ����ɹ���Ӧ���ͣ�200+ResponseBody �Զ�����Ӧ��
    [ProducesResponseType(typeof(ValidationProblemDetails), 422)]   // ���� ProducesResponseType ���ԣ�  ����ʧ����Ӧ���ͣ�422+ValidationProblemDetails ��׼��Ӧ��
    [ProducesResponseType(typeof(ProblemDetails), 500)]             // ���� ProducesResponseType ���ԣ�  ����ʧ����Ӧ���ͣ�500+ProblemDetails ��׼��Ӧ��
    public IActionResult Display([FromBody] RequestBody requestBody)
    {
        // �� WMS ��ȡ��������Դ���������Ϊ��/��ʽ���󣬻����ģ�Ͱ󶨣��Զ� 400 ���󣬲������ӿ�
        string[] showContent = {
                requestBody.taskType.ToString(),
                requestBody.endPickupName,
                requestBody.endPickupCode,
                requestBody.location
            };
        bool isLongText;
        int ret;
        var retDetails = new List<string>();

        // LED ��ʾ�߼�
        for (int i = 0; i < showContent.Length; i++)
        {
            // ���������Ļ������ʾ�ַ�������Ϊ���������ƶ�������ʾ������Ϊ������ʾ
            isLongText = Encoding.GetEncoding("GBK").GetByteCount(showContent[i]) > _ledValue.longText;
            try
            {
                ret = QYLED_DLL.SendInternalText_Net(
                    showContent[i],
                    _ledValue.controlCardIP,
                    _ledValue.UDP,
                    _ledValue.areaWidth,
                    _ledValue.areaHeight,
                    _ledValue.uid + i,
                    _ledValue.singleFundamentalColor,
                    isLongText ? _ledValue.moveFromRightToLeft : _ledValue.showNow,
                    _ledValue.showSpeedSlowest,
                    _ledValue.cyclicShow,
                    _ledValue.red,
                    _ledValue.songFont,
                    _ledValue.twelveSquared,
                    _ledValue.updatedThisImmediately,
                    _ledValue.saveFalse,
                    _ledValue.nonRotate);
            }
            catch (Exception ex)
            {
                if (ex is BadImageFormatException)
                {
                    retDetails = new List<string> {
                            "ԭ���޷����� x86 �� dll �� x64 ������",
                            "�����������������һ�����̨���ó�������ԡ����ɡ�ѡ����ȷ��Ŀ��ƽ̨" };
                }
                else if (ex is DllNotFoundException)
                {
                    retDetails = new List<string> {
                            "ԭ���ⲿ��̬�ⲻ���� bin/Release/net8.0 Ŀ¼",
                            "�������Ѷ�̬�⸴�Ƶ� bin/Release/net8.0 Ŀ¼"};
                }
                else
                {
                    retDetails = new List<string> { "δ֪�쳣���������ڴ治��" };
                }
                // BadImageFormatException �� DllNotFoundException ���ڷ��������û�������
                // 400  Bad Request             �ͻ��˴���   ����������﷨�������Ч����
                // 422  Unprocessable Entity    �ͻ��˴���   �����������﷨��ȷ��������󣬵���ҵ���߼�У��ʧ��
                // 500  Internal Server Error   ����˴���   �������ڴ�������ʱ�����������
                return StatusCode(500, new ProblemDetails
                {
                    Detail = string.Join("��", retDetails),
                    Instance = $"{ex}"
                });
            }
            retDetails.Add(ret == 0 ? $"{showContent[i]} �·��ɹ�" : $"{showContent[i]} �·�ʧ��"); // ��¼ÿ����Ϣ�ķ��ͽ��
            // ֻҪ����һ�ε��÷��йܷ���ʧ�ܣ��������� 422 ����
            if (ret != 0)
            {
                return StatusCode(422, new ValidationProblemDetails 
                {
                    Detail = string.Join("��", retDetails),
                    Errors = new Dictionary<string, string[]>
                    {
                        { $"�� {i + 1} ������", new string[] { $"{retDetails[i]}" } }
                    }
                });
            }
        }
        //return Ok("������Ϣ�ѳɹ������� LED ��ʾ��");
        return StatusCode(200, new ResponseBody
        {
            // LINQ ��ѯ����� results �б����Ƿ�����κΰ�����ʧ�ܡ��������ַ�����
            // Any() ��������������е�ÿ��Ԫ�أ�ֻҪ�ҵ�һ������������Ԫ�أ�Any() �ͻ᷵�� true��
            isSuccess = !retDetails.Any(r => r.Contains("ʧ��")),
            details = retDetails
        });
    }
}
