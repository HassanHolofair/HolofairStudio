using UnityEngine;
using System.Collections.Generic;

namespace HolofairStudio.AvailableItems
{
    [CreateAssetMenu]
    public class SceneResourcePack : ScriptableObject
    {
        public List<SceneResourcePackModel> Catalog;
    }
}
