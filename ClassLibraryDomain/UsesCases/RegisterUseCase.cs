using ClassLibraryDomain.Models;
using ClassLibraryDomain.Ports.Driven;
using ClassLibraryDomain.Ports.Driving;

namespace ClassLibraryDomain.UsesCases
{
    public class RegisterUseCase : IRegisterDriving
    {
        private readonly IRegisterPersistencePort _registerPersistencePort;
        private readonly IRegisterLocalPersistencePort _registerLocalPersistencePort;

        public RegisterUseCase(IRegisterPersistencePort registerPersistencePort, IRegisterLocalPersistencePort registerLocalPersistencePort)
        {
            _registerPersistencePort = registerPersistencePort;
            _registerLocalPersistencePort = registerLocalPersistencePort;
        }

        public async Task<GeneralAnswer<object>> InsertAsync(Register register)
        {
            try
            {
                await _registerLocalPersistencePort.InsertRegisterLocal(register);

                var registers = await _registerLocalPersistencePort.GetUncommitedRegisterLocal();
                registers.ForEach(x => x.Id = 0);
                var result = await _registerPersistencePort.InsertAsync(registers);
                if (result.status)
                {
                    await _registerLocalPersistencePort.MarkCommitedRegisterLocal();
                }
                return result;
               
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<object> { status = false, statusMessage = "Error registros. Excepcion: " + ex.Message, data = null };
            }
            
        }

        public async Task<GeneralAnswer<object>> DeleteOldLocalRegistersAsync()
        {
            try
            {
                await _registerLocalPersistencePort.DeletePeriodicallyRegisterLocal();
                return new GeneralAnswer<object> { status = true, statusMessage = "Registros menores de 60 dias eliminados", data = null};
            }
            catch (Exception ex)
            {
                return new GeneralAnswer<object> { status = false, statusMessage = "Error al eliminar registros de menos de 60 dias. Excepcion: " + ex.Message, data = null };
            }
        }
    }
}
