using UnityEngine;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolofairStudio.SceneItems
{
    public abstract class ItemDownloadHandler
    {
        protected readonly Queue<ItemModel> itemModels = new();

        public abstract Task<bool> EnqueueAsync(ItemModel itemModel);

        public abstract void DownloadAsync();
    }
}
