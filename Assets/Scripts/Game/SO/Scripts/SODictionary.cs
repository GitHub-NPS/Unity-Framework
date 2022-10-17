using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ScriptableObjectArchitecture
{
    public class SODictionary<T1, T2> : SOArchitectureBaseObject
    {
        public int Count => data.Count;

        [ShowInInspector] protected Dictionary<T1, T2> data = new Dictionary<T1, T2>();

        [Button]
        public virtual void Clear()
        {
            data.Clear();
        }

        public void Add(T1 key, T2 value)
        {
            data.Add(key, value);
        }

        public void Remove(T1 key)
        {
            data.Remove(key);
        }

        public T2 Get(T1 key)
        {
            if (data.ContainsKey(key)) return data[key];
            return default;
        }
    }
}