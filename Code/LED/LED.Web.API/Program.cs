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

        // 3������ web Ӧ�ó���
        var app = builder.Build();

        // 4������ http ������ܵ��м�����м������������˳��ģ�
        if (app.Environment.IsDevelopment())
        {
            // ��������ר�����ã�������������/��������
            app.UseSwagger();       // ���� swagger json �ĵ��˵��м��
            app.UseSwaggerUI();     // ���� swagger UI ���������м����Ĭ��·��Ϊ /swagger
        }
        app.UseHttpsRedirection();  // ���� https �ض����м���� ǿ�ƽ����� http �����ض��� https����ȫ��������
        app.UseAuthorization();     // ���� ��Ȩ�м����         �����֤/��Ȩ����Ȩ���ض���֮ǰ����
        app.MapControllers();       // ���� ӳ�������·���м�����Զ����� Controller ����·��

        // 5����������Ӧ�ó������� Web ����������ʼ�������󣬻��������ֱ��Ӧ�ùرա�
        app.Run();
    }
}
