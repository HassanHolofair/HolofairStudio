using com.outrealxr.networkimages;
using UnityEngine;
using UnityEngine.UI;

namespace HolofairStudio
{
    /// <summary>
    /// Represent a button in the menu
    /// </summary>
    [RequireComponent(typeof(NetworkImageUIImage))]
    public class SceneResourceView : View<int, string>
    {
        public NetworkImage NetworkImage { get; private set; }
        public int Index { get; set; }

        private void Awake()
        {
            NetworkImage = GetComponent<NetworkImageUIImage>();
        }

        public void Select()
        {
            OnSelect(Index);
        }
    }
}
