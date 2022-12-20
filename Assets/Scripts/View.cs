using System;
using UnityEngine;

namespace HolofairStudio
{
    public abstract class View<T1, T2> : MonoBehaviour
    {
        [SerializeField] protected GameObject _queued;
        [SerializeField] protected GameObject _loading;

        public Action<T1> OnSelect;

        public void SetView(T2 view)
        {

        }
    }
}
