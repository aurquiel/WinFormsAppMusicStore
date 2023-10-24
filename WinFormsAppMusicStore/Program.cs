using System.Reflection;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;
using ClassLibraryDomain.UsesCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.FileManager;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.LocalPersistence;
using WinFormsAppMusicStoreAdmin.DrivenAdapters.WebserviceAdapters;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Player;
using WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms;

namespace WinFormsAppMusicStoreAdmin
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "music-store-grupototal99-egomez");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
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
                                        .WriteTo.File(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AudioStore" + "\\logger.txt")
                                        .CreateLogger();

                    Log.Logger = serilogLogger;
                    services.AddSingleton(Log.Logger);
                    services.AddAutoMapper(Assembly.Load(typeof(Program).Assembly.GetName().Name!));
                    services.AddDbContext<AudioStoreLocalDbContext>(x => x.UseSqlite(AudioStoreLocalDbContext.PATH_FOLDER_DATABASE));

                    services.AddTransient<IRegisterLocalPersistencePort, RegisterLocalPersistenceAdapter>();
                    services.AddTransient<IRegisterPersistencePort, RegisterPersistenceAdapter>();
                    services.AddTransient<IAudioListLocalPersistencePort, AudioListLocalPersistenceAdapter>();
                    services.AddTransient<IAudioListPersistencePort, AudioListPersistenceAdapter>();    
                    services.AddTransient<IAudioPersistencePort, AudioPersistenceAdapter>();    
                    services.AddTransient<IFileManagerPersistencePort, FileManagerPersistenceAdapter>();    
                    services.AddTransient<IUserAccessLocalPersistencePort, UserAccessLocalPersistenceAdapter>();
                    services.AddTransient<IUserAccessPersistencePort, UserAccessPersistenceAdapter>();

                    services.AddTransient<IPlayerDriving, Player>();
                    services.AddTransient<IFileManagerDriving, FileManagerUseCase>();
                    services.AddTransient<IUserAccessDriving, UserAccessUseCase>();
                    services.AddTransient<IRegisterDriving, RegisterUseCase>();
                    services.AddTransient<IAudioDriving, AudioUseCase>();
                    services.AddTransient<IAudioListDriving, AudioListUseCase>();
                    services.AddTransient<IAudioListLocalDriving, AudioListLocalUseCase>();

                    services.AddTransient<FormLogin>();
                    services.AddTransient<FormMain>();
                    services.AddTransient<FormOperationAndWait>();
                });
        }
    }
}