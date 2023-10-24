using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driving;
using LiveChartsCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMusicStoreAdmin.DrivingAdapters.Winforms
{
    public class Registers
    {
        private IRegisterDriving _registerDriving;
        private int _storeId;
        private ILogger _logger;
        private int _timeRegisterInterval;
        private Thread _thread;
        public int Activity { get; set; }
        public string Message { get; set; }
        private static bool EraseOldRegisters = true;
        private bool _isOnlineMode;

        public enum STATUS { NO_REPRODUCING = 0, REPRODUCING = 1 };

        public Registers(IRegisterDriving registerDriving, ILogger logger, int timeRegisterInterval)
        {
            _registerDriving = registerDriving;
            _logger = logger;
            _timeRegisterInterval = timeRegisterInterval;
            _thread = new Thread(() => { DoWork(); });
        }

        public void StarRegisteringInStore(int storeId)
        {
            _storeId = storeId;
            _thread.Start();
        }

        public void SetRegister(STATUS activity, string message)
        {
            Activity = (int)activity;
            Message = message;
        }

        private async void DoWork()
        {
            while (true)
            {
                if(EraseOldRegisters)
                {
                    EraseOldRegisters = false;
                    var resultDelete = await _registerDriving.DeleteOldLocalRegistersAsync();
                    if (resultDelete.status == false)
                    {
                        _logger.Error("Error al eliminar registros Thread, " + resultDelete.statusMessage);
                    }
                }

                var result = await _registerDriving.InsertAsync(new Register { StoreId = _storeId, Activity = Activity, Message = Message, CreationDateTime = DateTime.Now });
                if (result.status == false)
                {
                    _logger.Error("Error al insertar registro Thread, " + result.statusMessage);
                }
                Thread.Sleep(_timeRegisterInterval * 60000); 
            }
        }


        public void StopWork()
        {
            try
            {
                _thread.Abort();
            }
            catch
            {
                ;
            }
        }
    }
}
