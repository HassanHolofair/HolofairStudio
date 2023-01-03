using System;
using UnityEngine;

namespace HolofairStudio
{
    public class SceneResourcesModel
    {
        public string ModelURL { get; set; }
        public string ImageURL { get; set; }
        public SceneResourceView View { get; set; }

        public SceneResourcesModel(int index, string modelURL, string imageURL, SceneResourceView prefab, Transform viewHolder, Action<int> OnButtonClicked)
        {
            ImageURL = imageURL;
            ModelURL = modelURL;

            View = GameObject.Instantiate(prefab, viewHolder);
            View.NetworkImage.SetAndEnqueue(ImageURL);
            View.Index = index;
            View.OnSelect += OnButtonClicked;
        }

    }
}
