using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolofairStudio
{
    public abstract class SceneJSON : MonoBehaviour
    {
        [SerializeField] protected ItemDownloadHandler[] _itemDownloadHandlers;

        [field: SerializeField] 
        public ItemView ViewPrefab { get; set; }
        public List<ItemModel> Items { get; set; }

        public abstract void AddItemAsync(ItemModel itemModel);
        public abstract List<ItemModel> FromJSON();
        public abstract string ToJSON();

        protected async Task EnqueueItemsAsync()
        {
            foreach (var item in Items)
                await EnqueueItemAsync(item);
        }

        protected async Task EnqueueItemAsync(ItemModel itemModel)
        {
            foreach (var handler in _itemDownloadHandlers)
            {
                bool hasEnqueued = await handler.EnqueueAsync(itemModel);

                if (hasEnqueued)
                    break;
            }
        }
    }
}
