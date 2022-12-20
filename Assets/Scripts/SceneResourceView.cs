using com.outrealxr.networkimages;
using UnityEngine;
using UnityEngine.UI;

namespace HolofairStudio
{
    [RequireComponent(typeof(NetworkImageUIImage))]
    public class SceneResourceView : View<int, string>
    {
        public NetworkImage NetworkImage { get; private set; }
        public int Index { get; set; }

        public void Select()
        {
            OnSelect(Index);
        }

        private void Awake()
        {
            NetworkImage = GetComponent<NetworkImageUIImage>();
        }
    }
}
