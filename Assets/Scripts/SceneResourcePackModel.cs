using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HolofairStudio
{
    [CreateAssetMenu]
    public class SceneResourcePackModel : ScriptableObject
    {
        public AssetReferenceGameObject Prefab;
        public AssetReferenceTexture2D Image;
    }
}
