using UnityEngine;
using HolofairStudio.AvailableItems;

namespace HolofairStudio
{
    public class App : MonoBehaviour
    {
        [SerializeField] private string _addressablesURL;
        [SerializeField] private string _remoteURL;

        private SceneResourceFactory[] _factories;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var Resources = FindObjectOfType<SceneResources>();
            _factories = new SceneResourceFactory[]
            {
                new AddressablesSceneResourceFactory(Resources, _addressablesURL),
                new RemoteJSONSceneResourceFactory(Resources, _remoteURL)
            };

            foreach (var factory in _factories)
                factory.StartLoading();
        }
    }
}
