using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NPS
{
    namespace Loot
    {
        public class LootItem : MonoBehaviour, ILoot
        {
            [SerializeField] private UnityEvent<LootData> OnLoot;

            public void Loot(LootData data)
            {
                OnLoot?.Invoke(data);
            }
        }

    }
}