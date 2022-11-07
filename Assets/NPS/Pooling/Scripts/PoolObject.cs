using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace NPS.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private Config config;

        private IObjectPool<GameObject> pool;
        private GameObject prefab;
        private Transform parent;

        public void Set(GameObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            LoadConfig();
            InitPool();
        }

        private void LoadConfig()
        {
            var refConfig = prefab.GetComponent<RefConfig>();
            config = refConfig ? refConfig.Config : Resources.Load<Config>($"NPS/Pooling/{prefab.name}");
            if (!config) config = Resources.Load<Config>("NPS/Pooling/Default");
        }

        private void InitPool()
        {
            switch (config.Type)
            {
                case TypePool.Stack:
                    pool = new ObjectPool<GameObject>(Create, Take, Return, Dispose, config.Check, config.Capacity, config.Max);
                    break;
                case TypePool.LinkedList:
                    pool = new LinkedPool<GameObject>(Create, Take, Return, Dispose, config.Check, config.Max);
                    break;
            }
        }

        private GameObject Create()
        {
            return Instantiate(prefab);
        }

        private void Take(GameObject obj)
        {
            if (obj)
            {
                obj.SetActive(true);
                if (parent) obj.transform.SetParent(parent);
            }
        }

        private void Return(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
        }

        private void Dispose(GameObject obj)
        {
            Destroy(obj);
        }

        public GameObject Get(GameObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            var obj = pool.Get();

            obj.transform.localPosition = prefab.transform.localPosition;
            obj.transform.localRotation = prefab.transform.localRotation;
            obj.transform.localScale = prefab.transform.localScale;

            return obj;
        }

        public void Release(GameObject obj, TypeDestroy type)
        {
            switch (type)
            {
                case TypeDestroy.Return:
                    pool.Release(obj);
                    break;
                    //case TypeDestroy.Dispose:
                    //    Destroy(obj);
                    //    break;
            }
        }
    }
}