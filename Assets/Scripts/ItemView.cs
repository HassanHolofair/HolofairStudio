using System;
using UnityEngine;

namespace HolofairStudio
{
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
            // todo: add collider to each mesh renderer
            GameObject meshContainingObject = transform.GetComponentInChildren<MeshRenderer>().gameObject;

            meshContainingObject.AddComponent<MeshCollider>();

            Debug.Log("collider added");
        }
    }
}
