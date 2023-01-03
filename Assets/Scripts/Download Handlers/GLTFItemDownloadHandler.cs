using UnityEngine;
using GLTFast;
using System.Threading.Tasks;

namespace HolofairStudio
{
    [CreateAssetMenu(menuName ="DownloadHandle/gltfHandler")]
    public class GLTFItemDownloadHandler : ItemDownloadHandler
    {
        [SerializeField] private GltfAsset _gltfPrefab;
        private const string EXTENTION = ".gltf";

        public override async void DownloadAsync()
        {
            while (itemModels.Count > 0)
            {
                var item = itemModels.Dequeue();
                item.ItemView.ShowDownloadingIndicator();

                GltfAsset asset = Instantiate(_gltfPrefab, item.ItemView.transform);
                await asset.Load(item.URL);

                item.ItemView.SetItemAsset(asset.gameObject);

                OnItemDownloadFinish?.Invoke(item.ItemView.gameObject);
            }
        }

        public override async Task<bool> EnqueueAsync(ItemModel itemModel)
        {
            // just to remove the compiler worning!
            //await Task.Delay(1);

            if (!itemModel.URL.Contains(EXTENTION))
                return false;

            itemModels.Enqueue(itemModel);
            itemModel.ItemView.ShowEnqueueIndicator();
            return true;
        }
    }
}
