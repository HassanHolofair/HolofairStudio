using UnityEngine;

namespace HolofairStudio
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private GameObject _queued;
        [SerializeField] private GameObject _loading;

        public bool reportMissingKeys;

        public void SetItemAsset(GameObject itemAsset)
        {
            Instantiate(itemAsset, transform);

            Destroy(_queued);
            Destroy(_loading);
        }

        public void Enqueue()
        {
            _queued.SetActive(true);
            _loading.SetActive(false);
        }

        public void Dequeue()
        {

        }
    }
}
