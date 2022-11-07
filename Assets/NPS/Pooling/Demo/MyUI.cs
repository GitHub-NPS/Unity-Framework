using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public class MyUI : MonoBehaviour
    {
        public void Clear()
        {
            foreach (var item in FindObjectsOfType<MyCube>())
            {
                if (item.gameObject.activeSelf) item.ForceDestroy();
            }
        }
    }
}