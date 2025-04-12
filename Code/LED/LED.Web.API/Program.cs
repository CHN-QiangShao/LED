using LED.DLL;

namespace LED.Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        // 1������ WebApplication �����������ں�����ӷ���������м��
        var builder = WebApplication.CreateBuilder(args);

        // 2����ӷ���������
        builder.Services.AddControllers();          // ��� ����������          ���� MVC �ܹ�
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer(); // ��� API �˵�Ԫ���ݷ���  ����֧�� swagger
        builder.Services.AddSwaggerGen();           // ��� swagger �ĵ����ɷ����������� API �ĵ�

        //builder.Services.AddHostedService<ShowContent>();   // ��Ӻ�̨����
        // ��Ҫ��ӵ���ʵ�����񣬷���ᱨ�� System.InvalidOperationException: Unable to resolve service for type 'LED.DLL.LedValue' while attempting to activate 'LED.Web.API.Controllers.LedController'
        // �����ڳ��Լ��� LedController ʱ��ASP.NET Core ������ע�������޷����� LED.DLL.LedValue ���͵ķ���
        // ��������ע�������ڴ��� LedController ʵ��ʱ���Ҳ�������ע�� LedValue �����ĺ��ʷ���
        builder.Services.AddSingleton<LedValue>();  // ��� ����ʵ������
        // ������ʽ���� https �˿ڡ�������ʹ launchSettings.json û�����ã��м��Ҳ��ʹ��ָ���Ķ˿�
        //builder.Services.AddHttpsRedirection(options => {
        //    options.HttpsPort = 7836;
        //});

        // 3������ web Ӧ�ó���
        var app = builder.Build();

        // 4������ http ������ܵ��м�����м������������˳��ģ�
        if (app.Environment.IsDevelopment())
        {
            // ��������ר�����ã�������������/��������
            app.UseSwagger();       // ���� swagger json �ĵ��˵��м��
            app.UseSwaggerUI(c => { // ���� swagger UI ���������м����Ĭ��·��Ϊ /swagger
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LED API");   // ָ�� Swagger UI ���ص� OpenAPI �淶�ļ�·����������ҳ���Ͻ� Select a definition ������
            });
        }

        //app.UseHttpsRedirection();  // ���� https �ض����м���� ǿ�ƽ����� http �����ض��� https����ȫ��������
        app.UseAuthorization();     // ���� ��Ȩ�м����         �����֤/��Ȩ����Ȩ���ض���֮ǰ����
        app.MapControllers();       // ���� ӳ�������·���м�����Զ����� Controller ����·��

        // 5����������Ӧ�ó������� Web ����������ʼ�������󣬻��������ֱ��Ӧ�ùرա�
        app.Run();
    }
}
