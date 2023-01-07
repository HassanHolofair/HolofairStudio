using UnityEngine;
using HolofairStudio.SceneItems;
using System.Collections.Generic;

namespace HolofairStudio.AvailableItems
{
    public class SceneResources : MonoBehaviour
    {
        [SerializeField] private SceneResourceView _viewPrefab;
        [SerializeField] private Transform _viewsHolder;

        private SceneJSON _sceneJSON;
        
        public List<SceneResourcesModel> Resources { get; private set; } = new List<SceneResourcesModel>();
        
        private void Start()
        {
            _sceneJSON = FindObjectOfType<SceneJSON>();
        }

        /// <summary>
        /// Add button to the menu
        /// </summary>
        /// <param name="modelUrl"></param>
        /// <param name="imageUrl"></param>
        public void AddResource(string modelUrl, string imageUrl)
        {
            SceneResourcesModel model = new(modelUrl, imageUrl, _viewPrefab, _viewsHolder, CreateItemModel);
            Resources.Add(model);
        }

        private void CreateItemModel(SceneResourcesModel model)
        {
            ItemModel itemModel = new (model.PrefabURL, _sceneJSON.ViewPrefab);
            _sceneJSON.AddAndEnqueueItemAsync(itemModel);
        }
    }
}
