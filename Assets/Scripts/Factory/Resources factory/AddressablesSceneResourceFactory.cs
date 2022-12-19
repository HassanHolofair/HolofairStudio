using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HolofairStudio
{

    public class AddressablesSceneResourceFactory : SceneResourceFactory
    {
        public AddressablesSceneResourceFactory(SceneResources resources, string url) : base(resources, url) { }

        public override async void StartLoading()
        {
            base.StartLoading();

            var operation = Addressables.LoadAssetAsync<SceneResourcePack>(_url);

            await operation.Task;

            var resources = operation.Result;

            for (int i = 0; i < resources.Catalog.Count; i++)
            {
                SceneResourcePackModel item = resources.Catalog[i];
                SceneResources.AddResource(i, item.ImageURl, item.PrefabURL);
            }
        }
    }
}
