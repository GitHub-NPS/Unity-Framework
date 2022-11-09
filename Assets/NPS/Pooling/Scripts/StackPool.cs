using System;
using System.Collections.Generic;

namespace NPS.Pooling
{
    public class StackPool<T> : AObjectPool<T> where T : class
    {
        internal readonly Stack<T> m_Stack;

        public override int CountInactive { get => this.m_Stack.Count; set { } }

        public StackPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000,
            int defaultCapacity = 10) : base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, maxSize)
        {
            this.m_Stack = new Stack<T>(defaultCapacity);
        }

        protected override T iGet()
        {
            T obj;
            if (this.m_Stack.Count == 0)
            {
                obj = this.m_CreateFunc();
                ++this.CountAll;
            }
            else
                obj = this.m_Stack.Pop();

            return obj;
        }

        protected override bool CollectionCheck(T element)
        {
            return base.CollectionCheck(element) && this.m_Stack.Contains(element);
        }

        protected override void iRelease(T element)
        {
            this.m_Stack.Push(element);
        }

        public override void Clear()
        {
            foreach (T obj in this.m_Stack)
                this.m_ActionOnDestroy(obj);

            m_Stack.Clear();

            base.Clear();
        }
    }
}