using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public class Manager : MonoBehaviour
    {
        public static Manager S;

        private void Awake()
        {
            if (!S) S = this;
        }

        private void Start()
        {

        }

        public T Spawn<T>(T prefab, Transform parent = null) where T : Component
        {
            return iSpawn(prefab.gameObject, parent).GetComponent<T>();
        }

        public GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            return iSpawn(prefab, parent);
        }

        private GameObject iSpawn(GameObject prefab, Transform parent)
        {
            return Pool.Spawn(prefab, parent);
        }

        public void Despawn<T>(T obj, TypeDestroy type = TypeDestroy.Return) where T : Component
        {
            Pool.Despawn(obj.gameObject, type);
        }

        public void Despawn(GameObject obj, TypeDestroy type = TypeDestroy.Return)
        {
            Pool.Despawn(obj, type);
        }
    }
}