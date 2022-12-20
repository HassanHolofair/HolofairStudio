using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolofairStudio
{
    public class TextAssetSceneJSON : SceneJSON
    {
        [SerializeField] private TextAsset _source;

        private async void Awake()
        {
            Items = FromJSON();

            await EnqueueItemsAsync();

            foreach (var handler in _itemDownloadHandlers)
                handler.DownloadAsync();
        }

        public override async void AddItemAsync(ItemModel itemModel)
        {
            Items.Add(itemModel);

            await EnqueueItemAsync(itemModel);

            foreach (var handler in _itemDownloadHandlers)
                handler.DownloadAsync();
        }

        public override List<ItemModel> FromJSON()
        {
            List<ItemModel> itemModels = new();

            var jArray = JArray.Parse(_source.text);

            for (int i = 0; i < jArray.Count; i++)
            {
                JObject jobject = (JObject)jArray[i];

                ItemModel model = new(jobject, ViewPrefab);

                itemModels.Add(model);
            }

            return itemModels;
        }

        public override string ToJSON()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
