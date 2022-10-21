using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPS
{
    namespace Loot
    {
        public abstract class UIItem : MonoBehaviour
        {
            [SerializeField] protected Image imgIcon;
            [SerializeField] protected Image imgFrame;
            [SerializeField] protected TextMeshProUGUI txtAmount;

            [SerializeField] private Material matGray;

            [SerializeField] private GameObject objCheck;
            [SerializeField] private GameObject objLock;
            [SerializeField] private GameObject objLight;

            private void OnDisable()
            {
                Gray(false);
                Check(false);
                Lock(false);
                Light(false);
            }

            public abstract void Set(object data);

            private void Gray(bool isGray = true)
            {
                if (imgIcon) imgIcon.material = isGray ? matGray : null;
                if (imgFrame) imgFrame.material = isGray ? matGray : null;
            }

            public void Check(bool isShow = true)
            {
                if (!objCheck) return;
                objCheck.SetActive(isShow);
            }

            public void Lock(bool isShow = true)
            {
                if (!objLock) return;
                objLock.SetActive(isShow);
            }

            public void Light(bool isShow = true)
            {
                if (!objLight) return;
                objLight.SetActive(isShow);
            }
        }
    }
}