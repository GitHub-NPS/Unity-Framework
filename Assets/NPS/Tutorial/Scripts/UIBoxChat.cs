using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NPS
{
    namespace Tutorial
    {
        public class UIBoxChat : MonoBehaviour
        {
            [SerializeField] TextMeshProUGUI txtMessage = null;

            public void Set(string message)
            {
                txtMessage.text = message;
            }
        }
    }
}