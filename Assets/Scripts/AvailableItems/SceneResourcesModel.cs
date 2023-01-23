using System;
using UnityEngine;

namespace HolofairStudio.AvailableItems
{
    /// <summary>
    /// Hold the data for button model
    /// </summary>
    public class SceneResourcesModel
    {
        private SceneResourceView _view;
        

        public string PrefabURL { get; private set; }

        public SceneResourcesModel(string modelURL, string imageURL, SceneResourceView prefab, Transform viewHolder, Action<SceneResourcesModel> OnButtonClicked)
        {
            PrefabURL = modelURL;

            _view = GameObject.Instantiate(prefab, viewHolder);
            _view.Model = this;
            _view.NetworkImage.SetAndEnqueue(imageURL);
            _view.NetworkImage.queued.SetActive(true);
            _view.OnSelect += OnButtonClicked;
        }
    }
}
