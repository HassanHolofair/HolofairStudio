using UnityEngine;
using System.Collections.Generic;

namespace HolofairStudio
{
    [CreateAssetMenu]
    public class SceneResourcePack : ScriptableObject
    {
        public List<SceneResourcePackModel> Catalog;
    }
}
