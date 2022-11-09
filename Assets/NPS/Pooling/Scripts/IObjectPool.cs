using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public interface IObjectPool<T> where T : class
    {
        int CountInactive { get; set; }
        T Get();
        void Release(T element);
        void Destroy(T element);
        void Clear();
    }
}