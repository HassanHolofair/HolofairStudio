using UnityEngine;
using UnityEngine.UI;

namespace HolofairStudio
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SceneResourceView : View<int, string>
    {
        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }


        public void Setup(int index, string imageURL)
        {
            // todo: use network image system to show the image

            _button.onClick.AddListener(delegate {
                OnSelect(index);
            });
        }
    }
}
