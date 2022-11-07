using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace NPS
{
    namespace Loot
    {
        public class Item : MonoBehaviour
        {
            private Collider2D cl;
            private LootData data;

            protected virtual void Awake()
            {
                cl = GetComponent<Collider2D>();
            }

            public void Set(LootData data)
            {
                this.data = data;
                EnableCollider();
            }

            public void EnableCollider()
            {
                cl.enabled = true;
            }

            public void OnEnter(Collider2D collision)
            {
                ILoot iLoot = collision.GetComponent<ILoot>();
                if (iLoot != null)
                {
                    iLoot.Loot(data);
                    cl.enabled = false;
                    NPS.Pooling.Manager.S.Despawn(this.gameObject);
                }
            }

            public void Jump(float height = 0.35f)
            {
                this.transform.DOJump(this.transform.position + new Vector3(0, height), 2, 1, 0.5f).OnComplete(EnableCollider);
            }
        }
    }
}