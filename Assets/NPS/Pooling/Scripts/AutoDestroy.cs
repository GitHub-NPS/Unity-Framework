using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private bool enable = true;
        [SerializeField] private float time = 1f;
        [SerializeField] private TypeDestroy type;

        private Coroutine handle;

        private void OnEnable()
        {
            if (enable) Auto();
        }

        public AutoDestroy Set(float time)
        {
            this.time = time;
            return this;
        }

        public AutoDestroy Set(TypeDestroy type)
        {
            this.type = type;
            return this;
        }

        public void Auto()
        {
            if (handle != default) StopCoroutine(handle);
            handle = default;

            handle = StartCoroutine(_Auto());
        }

        private IEnumerator _Auto()
        {
            yield return new WaitForSeconds(time);
            iDestroy();

            handle = default;
        }

        public void ForceDestroy()
        {
            if (handle != default) StopCoroutine(handle);
            handle = default;

            iDestroy();
        }

        private void iDestroy()
        {
            Manager.S.Despawn(this.gameObject, type);            
        }
    }

    public static class AutoDestroyExtension
    {
        public static void Set(this AutoDestroy ad, float time)
        {
            ad.Set(time);
        }

        public static void Set(this AutoDestroy ad, TypeDestroy type)
        {
            ad.Set(type);
        }
    }
}