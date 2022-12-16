using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HolofairStudio
{
    public class ItemModel
    {
        public string URL { get; set; }
        public ItemView ItemView { get; set; }

        public ItemModel(JObject json, ItemView itemView)
        {
            URL = json.GetValue("url").ToObject<string>();

            ItemView = GameObject.Instantiate(itemView);
            json.ToTransform(ItemView);
        }
    }
}
