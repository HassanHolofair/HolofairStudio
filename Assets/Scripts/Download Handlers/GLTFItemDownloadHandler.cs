using UnityEngine;
using System.Threading.Tasks;
using GLTFast;

namespace HolofairStudio
{
    [CreateAssetMenu(menuName ="DownloadHandle/gltfHandler")]
    public class GLTFItemDownloadHandler : ItemDownloadHandler
    {
        private const string EXTENTION = ".gltf";

        public override async void DownloadAsync()
        {
            while (itemModels.Count > 0)
            {
                var item = itemModels.Dequeue();

                var gltfHolder = new GameObject("gltf holder");
                gltfHolder.transform.parent = item.ItemView.transform;
                gltfHolder.transform.position = default;

                var gltfAsset = gltfHolder.AddComponent<GltfAsset>();
                await gltfAsset.Load(item.URL);
            }
        }

        public override async Task<bool> EnqueueAsync(ItemModel itemModel)
        {
            if (!itemModel.URL.Contains(EXTENTION))
                return false;

            itemModels.Enqueue(itemModel);
            return true;
        }

    }
}
