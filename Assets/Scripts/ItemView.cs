using UnityEngine;

namespace HolofairStudio
{
    /// <summary>
    /// Represent an object in the scene
    /// </summary>
    public class ItemView : View<GameObject, int>
    {
        public bool reportMissingKeys;

        public void SetItemAsset(GameObject itemAsset)
        {
            Instantiate(itemAsset, transform);

            _queued.SetActive(false);
            _loading.SetActive(false);

            AddCollider();
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

        private void AddCollider()
        {
            var objects = transform.GetComponentsInChildren<MeshRenderer>();

            foreach (var item in objects)
                item.gameObject.AddComponent<MeshCollider>();
        }
    }
}
