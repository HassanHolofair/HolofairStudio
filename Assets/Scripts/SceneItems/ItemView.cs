using UnityEngine;

namespace HolofairStudio.SceneItems
{
    /// <summary>
    /// Represent an object in the scene
    /// </summary>
    public class ItemView : MonoBehaviour
    {
        public ItemModel Model { get; set; }

        [SerializeField] private GameObject _queued;
        [SerializeField] private GameObject _loading;


        private void OnDestroy()
        {
            var jsonScene = FindObjectOfType<SceneJSON>();

            if (jsonScene && Model != null)
                jsonScene.RemoveItem(Model);
        }

        public void ShowEnqueueIndicator()
        {
            _queued.SetActive(true);
            _loading.SetActive(false);
        }

        public void ShowDownloadingIndicator()
        {
            _queued.SetActive(false);
            _loading.SetActive(true);
        }

        public void SetItemAsset(GameObject itemAsset)
        {
            Instantiate(itemAsset, transform);

            _queued.SetActive(false);
            _loading.SetActive(false);

            AddCollider();
        }

        private void AddCollider()
        {
            var objects = transform.GetComponentsInChildren<MeshRenderer>();

            foreach (var item in objects)
                item.gameObject.AddComponent<MeshCollider>();
        }

    }
}
