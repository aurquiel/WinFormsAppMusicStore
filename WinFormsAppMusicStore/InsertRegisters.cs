using ClassLibraryModels;
using ClassLibraryServices;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStore
{
    internal class InsertRegisters
    {
        private IServices _services;
        private int _storeId;
        private ILogger _logger;
        private Thread _thread;
        public string Message { get; set; }

        public InsertRegisters(IServices services, int storeId, ILogger logger)
        {
            _services = services;
            _storeId = storeId;
            _logger = logger;
            _thread = new Thread(() => { DoWork(); });
        }

        public void StarRegister()
        {
            _thread.Start();
        }

        private async void DoWork()
        {
            while(true)
            {
                var result = await _services.RegisterService.RegisterInsert(new Register { storeId = _storeId, operation = Message, creationDateTime = DateTime.Now});
                if(result.status == false)
                {
                    _logger.Error("Error al insertar registro Thread, " + result.statusMessage);
                }
                Thread.Sleep(1800000); //30 min de espera para el proximo registro
            }
        }
    }
}
