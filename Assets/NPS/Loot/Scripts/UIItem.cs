using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPS.Loot
{
    public abstract class UIItem : MonoBehaviour
    {
        [SerializeField] protected Image imgIcon;
        [SerializeField] protected Image imgFrame;
        [SerializeField] protected TextMeshProUGUI txtAmount;

        public abstract void Set(object data);
    }
}