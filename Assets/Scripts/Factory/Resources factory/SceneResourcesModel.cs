using UnityEngine;

namespace HolofairStudio
{
    public class SceneResourcesModel
    {
        [field: SerializeField] public string ModelURL { get; set; }
        [field: SerializeField] public string ImageURL { get; set; }
        [field: SerializeField] public SceneResourceView View { get; set; }

        public SceneResourcesModel(int index, string modelURL, string imageURL, SceneResourceView prefab, Transform viewHolder)
        {
            ImageURL = imageURL;
            ModelURL = modelURL;

            SceneResourceView view = GameObject.Instantiate(prefab, viewHolder);
            view.Setup(index, imageURL);
        }
    }
}
