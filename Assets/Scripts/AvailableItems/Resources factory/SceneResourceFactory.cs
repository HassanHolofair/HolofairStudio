using UnityEngine;

namespace HolofairStudio.AvailableItems
{
    public abstract class SceneResourceFactory
    {
        protected SceneResources _sceneResources;
        protected string _url;

        public SceneResourceFactory(SceneResources resources, string url)
        {
            _sceneResources = resources;
            _url = url;
        }

        public virtual void StartLoading()
        {
            if (string.IsNullOrEmpty(_url))
            {
                Debug.Log("Invalid key");
                return;
            }
        }
    }
}
