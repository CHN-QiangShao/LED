using Microsoft.AspNetCore.Mvc;
using LED.DLL;

namespace LED.Web.API.Controllers;

[ApiController]             // ����һ�� API ר�ÿ����������� API �ض���Ϊ
[Route("[controller]")]     // ����·��ģ�壬ʹ�ÿ���������(ȥ��"Controller"��׺)��ʵ��·��·��Ϊ /LED
public class LEDController : ControllerBase
{
    private readonly ILogger<LEDController> _logger;

    /// <summary>
    /// ���캯��������ע����־��¼��
    /// </summary>
    /// <param name="logger">��־</param>
    public LEDController(ILogger<LEDController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name = "LED")]     // ���� GET ����˵㣬������·��Ϊ"LED"
    public IEnumerable<LedDto> Get()
    {
        return new LedDto[]
        {
            new LedDto
            {
                taskType = TaskType.inbound,
                endPickupName = "��ʰ��С�� A",
                endPickupCode = "Car A",
                location = "A-1-1-1-1"
            }
        };
    }
}
