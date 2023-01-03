using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HolofairStudio
{
    /// <summary>
    /// Hold the data for an object in the scene, the actual object is ItemView
    /// </summary>
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

        public ItemModel(string url, ItemView itemView)
        {
            URL = url;
            ItemView = GameObject.Instantiate(itemView);
        }
    }
}
