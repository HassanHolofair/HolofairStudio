using UnityEngine;

namespace HolofairStudio
{
    public abstract class SceneResourceFactory
    {
        public SceneResources SceneResources { get; private set; }
        protected string _url;

        public SceneResourceFactory(SceneResources resources, string url)
        {
            SceneResources = resources;
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
