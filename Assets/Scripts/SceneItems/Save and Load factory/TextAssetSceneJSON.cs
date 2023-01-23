using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace HolofairStudio.SceneItems
{
    public class TextAssetSceneJSON : SceneJSON
    {
        [SerializeField] private TextAsset _source;

        public override void Save()
        {
            string json = ToJSON().ToString();
            Debug.Log(json);
        }

        protected override List<ItemModel> FromJSON()
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

        protected override JToken ToJSON()
        {
            JArray array = new();
            foreach (var item in _items)
            {
                array.Add(item.ToJson());
            }

            return array;
        }
    }
}
