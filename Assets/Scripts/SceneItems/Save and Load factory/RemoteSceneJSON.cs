using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace HolofairStudio.SceneItems
{
    public class RemoteSceneJSON : SceneJSON
    {
        [SerializeField] private string _url;

        protected override List<ItemModel> FromJSON()
        {
            throw new System.NotImplementedException();
        }

        protected override JToken ToJSON()
        {
            throw new System.NotImplementedException();
        }

        public void Upload()
        {

        }

        public override void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
