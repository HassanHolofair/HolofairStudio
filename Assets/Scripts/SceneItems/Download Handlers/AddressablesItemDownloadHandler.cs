using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HolofairStudio.SceneItems
{
    public class AddressablesItemDownloadHandler : ItemDownloadHandler
    {
        public override async Task<bool> EnqueueAsync(ItemModel itemModel)
        {
            bool isExisit = await AddressableResourceExists(itemModel.URL);

            if (isExisit)
            {
                itemModels.Enqueue(itemModel);
                itemModel.ItemView.ShowEnqueueIndicator();
                return isExisit;
            }

            return false;
        }

        public async Task<bool> AddressableResourceExists(string key)
        {
            var operation = Addressables.LoadResourceLocationsAsync(key);

            await operation.Task;
            return operation.Status == AsyncOperationStatus.Succeeded;
        }

        public override async void DownloadAsync()
        {
            while (itemModels.Count > 0)
            {
                var item = itemModels.Dequeue();
                item.ItemView.ShowDownloadingIndicator();

                var operation = Addressables.LoadAssetAsync<GameObject>(item.URL);

                await operation.Task;

                item.ItemView.SetItemAsset(operation.Result);
            }
        }
    }
}
