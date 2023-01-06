using System.Linq;
using UnityEngine.AddressableAssets;

namespace HolofairStudio
{

    public class AddressablesSceneResourceFactory : SceneResourceFactory
    {
        public AddressablesSceneResourceFactory(SceneResources resources, string url) : base(resources, url) { }

        public override void StartLoading()
        {
            base.StartLoading();

            LoadAdressable();
        }

        private async void LoadAdressable()
        {
            var operation = Addressables.LoadAssetAsync<SceneResourcePack>(_url);

            await operation.Task;

            var resources = operation.Result;

            for (int i = 0; i < resources.Catalog.Count; i++)
            {
                SceneResourcePackModel item = resources.Catalog[i];

                string imageURL = Addressables.LoadResourceLocationsAsync(item.Image).WaitForCompletion().First()?.PrimaryKey;
                string prefabURL = Addressables.LoadResourceLocationsAsync(item.Prefab).WaitForCompletion().First()?.PrimaryKey;
                _sceneResources.AddResource(modelUrl: prefabURL, imageUrl: imageURL);
            }
        }
    }
}
