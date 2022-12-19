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

        public abstract void AddItem(ItemModel itemModel);
        public abstract List<ItemModel> FromJSON();
        public abstract string ToJSON();

        protected async Task EnqueueAllItems()
        {
            foreach (var item in Items)
            {
                foreach (var handler in _itemDownloadHandlers)
                {
                    bool hasEnqueued = await handler.EnqueueAsync(item);

                    if (hasEnqueued)
                        break;
                }
            }
        }
    }
}
