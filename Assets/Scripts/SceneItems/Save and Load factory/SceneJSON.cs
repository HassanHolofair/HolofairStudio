using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HolofairStudio.SceneItems
{
    public abstract class SceneJSON : MonoBehaviour
    {
        [field: SerializeField]  public ItemView ViewPrefab { get; private set; }
        [SerializeField] protected ItemDownloadHandler[] _itemDownloadHandlers;

        protected readonly List<ItemModel> _items = new();
        
        protected abstract List<ItemModel> FromJSON();
        protected abstract JToken ToJSON();
        public abstract void Save();

        public virtual async void AddAndEnqueueItemAsync(ItemModel itemModel)
        {
            _items.Add(itemModel);

            await EnqueueItemAsync(itemModel);

            HandleItems();
        }

        public void RemoveItem(ItemModel itemModel) => _items.Remove(itemModel);

        protected async Task EnqueueItemsAsync()
        {
            foreach (var item in _items)
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

        protected void HandleItems()
        {
            foreach (var handler in _itemDownloadHandlers)
                handler.DownloadAsync();
        }

        public async void Load()
        {
            ClearScene();

            _items.AddRange(FromJSON());

            await EnqueueItemsAsync();

            foreach (var handler in _itemDownloadHandlers)
                handler.DownloadAsync();
        }

        private void ClearScene()
        {
            _items.Clear();
        }

    }
}
