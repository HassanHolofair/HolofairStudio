using UnityEngine;
using System.Collections.Generic;

namespace HolofairStudio
{
    public class SceneResources : MonoBehaviour
    {
        [SerializeField] private SceneResourceView _viewPrefab;
        [SerializeField] private Transform _viewsHolder;

        public List<SceneResourcesModel> Resources { get; private set; } = new List<SceneResourcesModel>();

        public void AddResource(int index, string modelUrl, string imageUrl)
        {
            SceneResourcesModel model = new(index, modelUrl, imageUrl, _viewPrefab, _viewsHolder);
            Resources.Add(model);
        }

        private void CreateItemModel()
        {

        }
    }
}
