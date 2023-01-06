using com.outrealxr.networkimages;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HolofairStudio
{
    /// <summary>
    /// Represent a button in the menu
    /// </summary>
    [RequireComponent(typeof(NetworkImageUIImage))]
    public class SceneResourceView : MonoBehaviour
    { 

        public Action<SceneResourcesModel> OnSelect;
        public SceneResourcesModel Model { get; set; }
        public NetworkImage NetworkImage { get; private set; }

        private void Awake()
        {
            NetworkImage = GetComponent<NetworkImageUIImage>();
        }

        public void Select()
        {
            OnSelect?.Invoke(Model);
        }
    }
}
