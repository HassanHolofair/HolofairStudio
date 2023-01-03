using UnityEngine;
using System.Collections.Generic;

namespace HolofairStudio
{
    public class SceneResources : MonoBehaviour
    {
        [SerializeField] private SceneResourceView _viewPrefab;
        [SerializeField] private Transform _viewsHolder;

        private SceneJSON _sceneJSON;
        private int _currentResourcesIndex;
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
            SceneResourcesModel model = new(_currentResourcesIndex, modelUrl, imageUrl, _viewPrefab, _viewsHolder, CreateItemModel);
            Resources.Add(model);
            _currentResourcesIndex++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void CreateItemModel(int index)
        {
            SceneResourcesModel resource = Resources[index];
            ItemModel itemModel = new (resource.ModelURL, _sceneJSON.ViewPrefab);
            _sceneJSON.AddItemAsync(itemModel);
        }
    }
}
