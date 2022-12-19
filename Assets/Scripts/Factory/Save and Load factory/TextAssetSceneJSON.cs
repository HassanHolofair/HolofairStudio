using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HolofairStudio
{
    public class TextAssetSceneJSON : SceneJSON
    {
        [SerializeField] private TextAsset _source;

        public override void AddItem(ItemModel itemModel)
        {
            
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
        }

        private async void Awake()
        {
            Items = FromJSON();

            await EnqueueAllItems();

            foreach (var handler in _itemDownloadHandlers)
            {
                handler.DownloadAsync();
            }
        }
    }
}
