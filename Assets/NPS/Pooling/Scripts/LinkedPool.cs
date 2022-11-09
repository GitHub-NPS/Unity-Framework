using System;
using System.Collections.Generic;

namespace NPS.Pooling
{
    public class LinkedPool<T> : AObjectPool<T> where T : class
    {
        internal LinkedPool<T>.LinkedPoolItem m_PoolFirst;
        internal LinkedPool<T>.LinkedPoolItem m_NextAvailableListItem;

        internal int countInactive = 0;

        public override int CountInactive { get => countInactive; set { countInactive = value; } }

        public LinkedPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000)
            : base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, maxSize)
        {
            countInactive = 0;
        }

        protected override T iGet()
        {
            T obj;
            if (this.m_PoolFirst == null)
            {
                obj = this.m_CreateFunc();
            }
            else
            {
                LinkedPool<T>.LinkedPoolItem poolFirst = this.m_PoolFirst;
                obj = poolFirst.value;
                this.m_PoolFirst = poolFirst.poolNext;
                poolFirst.poolNext = this.m_NextAvailableListItem;
                this.m_NextAvailableListItem = poolFirst;
                this.m_NextAvailableListItem.value = default(T);
                --this.CountInactive;
            }

            return obj;
        }

        protected override bool CollectionCheck(T element)
        {
            for (LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_PoolFirst;
                     linkedPoolItem != null;
                     linkedPoolItem = linkedPoolItem.poolNext)
            {
                if ((object)linkedPoolItem.value == (object)element) return true;
            }

            return base.CollectionCheck(element);
        }

        protected override void iRelease(T element)
        {
            LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_NextAvailableListItem;
            if (linkedPoolItem == null)
                linkedPoolItem = new LinkedPool<T>.LinkedPoolItem();
            else
                this.m_NextAvailableListItem = linkedPoolItem.poolNext;
            linkedPoolItem.value = element;
            linkedPoolItem.poolNext = this.m_PoolFirst;
            this.m_PoolFirst = linkedPoolItem;
            ++this.CountInactive;
        }

        public override void Clear()
        {
            for (LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_PoolFirst;
                     linkedPoolItem != null;
                     linkedPoolItem = linkedPoolItem.poolNext)
                this.m_ActionOnDestroy(linkedPoolItem.value);

            this.m_PoolFirst = (LinkedPool<T>.LinkedPoolItem)null;
            this.m_NextAvailableListItem = (LinkedPool<T>.LinkedPoolItem)null;
            this.CountInactive = 0;

            base.Clear();
        }

        internal class LinkedPoolItem
        {
            internal LinkedPool<T>.LinkedPoolItem poolNext;
            internal T value;
        }
    }
}