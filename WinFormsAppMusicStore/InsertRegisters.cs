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
        private ILogger _logger;
        private ConcurrentQueue<Register> _queue = new ConcurrentQueue<Register>(); 
        private Thread _thread;

        public InsertRegisters(IServices services, ILogger logger)
        {
            _services = services;
            _logger = logger;
            _thread = new Thread(() => { DoWork(); });
            _thread.Start();
        }

        public void Add(Register register)
        {
            _queue.Enqueue(register);
        }

        private async void DoWork()
        {
            while(true)
            {
                Thread.Sleep(100);
                if(_queue.Count > 0)
                {
                    _queue.TryDequeue(out Register register);
                    var result = await _services.RegisterService.RegisterInsert(register);
                    if(result.status == false)
                    {
                        _logger.Error("Error al insertar registro Thread, " + result.statusMessage);
                    }
                }
            }
        }
    }
}
