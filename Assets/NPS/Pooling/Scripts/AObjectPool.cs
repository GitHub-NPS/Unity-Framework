using System;
using System.Collections.Generic;

namespace NPS.Pooling
{
    public abstract class AObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        protected readonly Func<T> m_CreateFunc;
        protected readonly Action<T> m_ActionOnGet;
        protected readonly Action<T> m_ActionOnRelease;
        protected readonly Action<T> m_ActionOnDestroy;
        protected readonly int m_MaxSize;
        protected readonly bool m_CollectionCheck;

        public int CountAll { get; protected set; }

        public int CountActive => this.CountAll - this.CountInactive;

        public abstract int CountInactive { get; set; }

        public AObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000)
        {
            this.m_CreateFunc = createFunc;
            this.m_ActionOnGet = actionOnGet;
            this.m_ActionOnRelease = actionOnRelease;
            this.m_ActionOnDestroy = actionOnDestroy;
            this.m_CollectionCheck = collectionCheck;
            this.m_MaxSize = maxSize;
        }

        protected abstract T iGet();

        public T Get()
        {
            T obj = iGet();
            m_ActionOnGet(obj);
            return obj;
        }

        protected virtual bool CollectionCheck(T element) => this.m_CollectionCheck && this.CountInactive > 0;
        protected abstract void iRelease(T element);

        public void Release(T element)
        {
            if (CollectionCheck(element))
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");

            m_ActionOnRelease(element);
            if (this.CountInactive < this.m_MaxSize)
                iRelease(element);
            else
                m_ActionOnDestroy(element);
        }

        public void Destroy(T element)
        {
            m_ActionOnRelease(element);
            m_ActionOnDestroy(element);
        }

        public virtual void Clear()
        {
            this.CountAll = 0;
        }

        public void Dispose() => this.Clear();
    }
}