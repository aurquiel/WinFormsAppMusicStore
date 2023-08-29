using ClassLibraryModels;
using ClassLibraryServices;
using Serilog;

namespace ClassLibraryRegister
{
    public class Registers
    {
        private IServices _services;
        private int _storeId;
        private ILogger _logger;
        private Thread _thread;
        public int Activity { get; set; }
        public string Message { get; set; }

        public Registers(IServices services, int storeId, ILogger logger)
        {
            _services = services;
            _storeId = storeId;
            _logger = logger;
            _thread = new Thread(() => { DoWork(); });
        }

        public void StarRegistering()
        {
            _thread.Start();
        }

        public void SetRegister(int activity, string message)
        {
            Activity = activity;
            Message = message;
        }

        private async void DoWork()
        {
            while (true)
            {
                var result = await _services.RegisterService.RegisterInsert(new Register { storeId = _storeId, activity = Activity, message = Message, creationDateTime = DateTime.Now });
                if (result.status == false)
                {
                    _logger.Error("Error al insertar registro Thread, " + result.statusMessage);
                }
                Thread.Sleep(1800000); //30 min de espera para el proximo registro
            }
        }
    }
}