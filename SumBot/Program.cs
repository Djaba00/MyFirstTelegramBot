using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using SumBot.Configuration;
using SumBot.Controllers;
using VoiceTexterBot.Controllers;
using SumBot.Services;

namespace SumBot
{
    class Program
    {
        public static async Task Main()
        {
            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Настройки приложения
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            // Регистрация хранилища сессий
            services.AddSingleton<IStorage, MemoryStorage>();

            // Регистрация контроллеров
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<DefaultMessageController>();

            // Регистрирация объекта TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));

            // Регистрирация постоянно активного сервиса бота
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6036555773:AAHoEDe3BphyGA2GFrV6Zh7jQgu2LgEftnA"
            };
        }
    }
}