using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Host����Ӧ�õ������������ڹ������÷�������������ܵ�
        // Ĭ��������־��¼��������ϵ��ע�������
        // Host��һ����װ��Ӧ����Դ�Ķ���
        // .net core (����api)
        // .net extensions Դ�� (.net core ��չ��)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // .net core ������host������(ͨ��)host
            // web host (ͨ��host����չ���ṩ����web���ܣ�֧��http��������kestrel������IIS����)
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // ����ǰ׺Ϊ ASPNETCORE_ �Ļ�����������web host����
                    // Ĭ�Ͻ�kestrel����Ϊweb���������������Ĭ������/֧��IIS����
                    // �Զ������� ����web host

                    // ������ã�������host ��host����(��չ���ṩ�ķ���)
                    // webBuilder.ConfigureKestrel(options=> options.Limits)
                    // webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));

                    webBuilder.UseStartup<Startup>();
                });
    }
}
