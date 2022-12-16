using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HolofairStudio
{
    public class RemoteSceneJSON : SceneJSON
    {
        [SerializeField] private string _url;

        public override void AddItem(ItemModel itemModel)
        {
            throw new System.NotImplementedException();
        }

        public override List<ItemModel> FromJSON()
        {
            throw new System.NotImplementedException();
        }

        public override string ToJSON()
        {
            throw new System.NotImplementedException();
        }

        public void Upload()
        {

        }
    }
}
