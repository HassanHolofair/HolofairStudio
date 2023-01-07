using Battlehub.RTCommon;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace HolofairStudio.SceneItems
{
    /// <summary>
    /// Hold the data for an object in the scene, the actual object is ItemView
    /// </summary>
    public class ItemModel
    {
        public string URL { get; set; }
        public ItemView ItemView { get; set; }
        private IRTE _editor;

        public ItemModel(JObject json, ItemView itemView)
        {
            URL = json.GetValue("url").ToObject<string>();

            ItemView = GameObject.Instantiate(itemView);
            ItemView.Model = this;
            json.ToTransform(ItemView);

            RecordPrefab(ItemView.gameObject);
        }

        public ItemModel(string url, ItemView itemView)
        {
            _editor = IOC.Resolve<IRTE>();

            URL = url;
            ItemView = GameObject.Instantiate(itemView);
            ItemView.Model = this;

            RecordPrefab(ItemView.gameObject);
        }

        private void RecordPrefab(GameObject prefab)
        {
            if (prefab == null)
                return;

            ExposeToEditor exposeToEditor = prefab.GetComponent<ExposeToEditor>();
            _editor.Undo.BeginRecord();
            _editor.Undo.RegisterCreatedObjects(new[] { exposeToEditor });
            _editor.Selection.activeObject = prefab;
            _editor.Undo.EndRecord();
        }

        public JToken ToJson()
        {
            JObject keyValuePairs = new();
            keyValuePairs.Add("url", URL);
            keyValuePairs.Add("position", ItemView.transform.position.ToJObject());
            keyValuePairs.Add("euler", ItemView.transform.eulerAngles.ToJObject());
            keyValuePairs.Add("scale", ItemView.transform.localScale.ToJObject());

            return keyValuePairs;
        }
    }
}
