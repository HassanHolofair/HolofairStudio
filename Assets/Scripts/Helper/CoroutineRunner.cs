using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HolofairStudio
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Instance;

        private void Awake()
        {
            if (Instance == null) 
                Instance = this;
        }
    }
}
