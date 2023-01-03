using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HolofairStudio
{
    public abstract class ItemDownloadHandler : ScriptableObject
    {
        public static Action<GameObject> OnItemDownloadFinish;

        public Queue<ItemModel> itemModels = new();

        public abstract Task<bool> EnqueueAsync(ItemModel itemModel);

        public abstract void DownloadAsync();
    }
}
