using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HolofairStudio
{
    [CreateAssetMenu(menuName = "DownloadHandle/AddressableHandler")]
    public class AddressablesItemDownloadHandler : ItemDownloadHandler
    {
        public override async Task<bool> EnqueueAsync(ItemModel itemModel)
        {
            bool isExisit = await AddressableResourceExists(itemModel.URL);

            if (isExisit)
            {
                itemModels.Enqueue(itemModel);
                itemModel.ItemView.Enqueue();
                return true;
            }

            return false;
        }

        public async Task<bool> AddressableResourceExists(string key)
        {
            var operation = Addressables.LoadResourceLocationsAsync(key);
            await operation.Task;
            return operation.Status == AsyncOperationStatus.Succeeded;
        }

        private async void GetNext()
        {
            if (itemModels.Count <= 0)
                return;

            var model = itemModels.Dequeue();

            AsyncOperationHandle<GameObject> opHandle;

            opHandle = Addressables.LoadAssetAsync<GameObject>(model.URL);

            await opHandle.Task;

            model.ItemView.SetItemAsset(opHandle.Result);

            GetNext();
        }

        public override async void DownloadAsync()
        {
            while (itemModels.Count > 0)
            {
                var item = itemModels.Dequeue();

                var operation = Addressables.LoadAssetAsync<GameObject>(item.URL);

                await operation.Task;

                item.ItemView.SetItemAsset(operation.Result);
            }
        }
    }
}
