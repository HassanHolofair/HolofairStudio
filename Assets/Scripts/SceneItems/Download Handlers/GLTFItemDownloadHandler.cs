using UnityEngine;
using GLTFast;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HolofairStudio.SceneItems
{
    public class GLTFItemDownloadHandler : ItemDownloadHandler
    {
        private GltfAsset _gltfPrefab;
        private const string EXTENTION = ".gltf";
        private readonly Dictionary<string, GameObject> _gltfItems = new();

        public GLTFItemDownloadHandler(GltfAsset gltfPrefab)
        {
            _gltfPrefab = gltfPrefab;
        }

        public override async void DownloadAsync()
        {
            while (itemModels.Count > 0)
            {
                var item = itemModels.Dequeue();

                GameObject gltfItem;
                if (_gltfItems.ContainsKey(item.URL))
                {
                    gltfItem = _gltfItems[item.URL];
                }
                else
                {
                    item.ItemView.ShowDownloadingIndicator();
                    GltfAsset asset = GameObject.Instantiate(_gltfPrefab, item.ItemView.transform);
                    await asset.Load(item.URL);
                    gltfItem = asset.gameObject;
                    _gltfItems.Add(item.URL, gltfItem);
                }
                
                item.ItemView.SetItemAsset(gltfItem);
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
