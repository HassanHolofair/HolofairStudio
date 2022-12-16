using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HolofairStudio
{
    public abstract class ItemDownloadHandler : ScriptableObject
    {
        public Queue<ItemModel> itemModels = new();

        public abstract Task<bool> EnqueueAsync(ItemModel itemModel);

        public abstract void DownloadAsync();
    }
}
