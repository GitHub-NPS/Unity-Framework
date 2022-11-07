using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public static class Pool
    {
        public static Dictionary<GameObject, PoolObject> Links = new Dictionary<GameObject, PoolObject>();
        public static Dictionary<GameObject, GameObject> Maps = new Dictionary<GameObject, GameObject>();

        public static GameObject Spawn(GameObject prefab, Transform parent)
        {
            if (!Links.ContainsKey(prefab))
            {
                var pool = new GameObject($"Pool ({prefab.name})").AddComponent<PoolObject>();
                pool.Set(prefab, parent);
                Links.Add(prefab, pool);
            }
            var obj = Links[prefab].Get(prefab, parent);
            Maps.Add(obj, prefab);
            return obj;
        }

        public static void Despawn(GameObject obj, TypeDestroy type)
        {
            if (!Maps.ContainsKey(obj))
            {
                MonoBehaviour.Destroy(obj);
                return;
            }

            Links[Maps[obj]].Release(obj, type);
            Maps.Remove(obj);
        }

        public static void Clear()
        {
            Links.Clear();
            Maps.Clear();
        }
    }
}
