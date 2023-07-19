using ClassLibraryModels;

namespace ClassLibraryServices
{
    public interface IStoreService
    {
        public Task<GeneralAnswer<List<Store>>> StoreGetAll();
        public Task<GeneralAnswer<object>> StoreCrete(Store stroe);
        public Task<GeneralAnswer<object>> StoreUpdate(Store store);
        public Task<GeneralAnswer<object>> StoreDelete(Store store);
    }
}
