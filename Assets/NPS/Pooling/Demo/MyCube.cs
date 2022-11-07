using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    [System.Serializable]
    public class MyCube : MonoBehaviour
    {
        [SerializeField]
        private Vector2 despawn = new Vector2(0.5f, 2);

        private AutoDestroy ad;

        private void Awake()
        {
            ad = GetComponent<AutoDestroy>();
        }

        private void Start()
        {
            ad.Set(Random.Range(despawn.x, despawn.y)).Auto();
        }

        public void ForceDestroy()
        {
            ad.ForceDestroy();
        }
    }
}