using System;
using System.IO;
using System.Runtime.Intrinsics.X86;
using ClassLibraryFiles;
using ClassLibraryServices;
using ClassLibraryServices.WebService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WinFormsAppMusicStoreAdmin
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "music-store-grupototal99-egomez");
        private static readonly string PATH_FOLDER_LOG = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore";

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                CreateLoginDirectory();

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var host = CreateHostBuilder().Build();
                ServiceProvider = host.Services;

                Application.Run(ServiceProvider.GetRequiredService<FormLogin>());
            }
            else
            {
                MessageBox.Show("Ya existe una isntancia de esta aplicacion corriendo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {

            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {

                    var serilogLogger = new LoggerConfiguration()
                                        .WriteTo.Console(Serilog.Events.LogEventLevel.Verbose)
                                        .WriteTo.File(PATH_FOLDER_LOG + "//logger.txt")
                                        .CreateLogger();

                    Log.Logger = serilogLogger;
                    services.AddSingleton(Log.Logger);
                    services.AddSingleton<IFileManager, FileManager>();
                    services.AddSingleton<FormLogin>();
                });
        }

        private static void CreateLoginDirectory()
        {
            if(!Directory.Exists(PATH_FOLDER_LOG))
            {
                Directory.CreateDirectory(PATH_FOLDER_LOG);
            }
        }

    }
}