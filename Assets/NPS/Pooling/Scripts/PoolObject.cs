using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPS.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private Config config;

        private IObjectPool<GameObject> pool;
        private Dictionary<GameObject, DateTime> scans = new Dictionary<GameObject, DateTime>();

        private GameObject prefab;
        private Transform parent;

        public void Set(GameObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            LoadConfig();
            InitPool();
            Scan();
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
                    pool = new StackPool<GameObject>(Create, Take, Return, Dispose, config.Check, config.Max,
                        config.Capacity);
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

            GameObject obj = null;
            while (!obj)
            {
                obj = pool.Get();
            }

            if (config.IsScan)
            {
                if (scans.ContainsKey(obj))
                    scans.Remove(obj);
            }

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

                    if (config.IsScan)
                    {
                        if (!scans.ContainsKey(obj))
                            scans.Add(obj, DateTime.Now);
                        else scans[obj] = DateTime.Now;
                    }
                    break;
                case TypeDestroy.Dispose:
                    if (config.IsScan)
                    {
                        if (scans.ContainsKey(obj))
                            scans.Remove(obj);
                    }

                    pool.Destroy(obj);                    
                    break;
            }
        }

        private void Scan()
        {
            scans.Clear();

            if (config.IsScan)
            {
                InvokeRepeating("iScan", 0, config.TimeScan);
            }
        }

        private void iScan()
        {
            foreach (var item in scans.Keys.ToList())
            {
                if (item && !item.activeSelf && (DateTime.Now - scans[item]).TotalSeconds > config.LifeTime)
                {
                    //Debug.Log($"Scan Destroy: {item.name}_{item.GetInstanceID()}");
                    Release(item, TypeDestroy.Dispose);
                }
            }
        }
    }
}