using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public class RefConfig : MonoBehaviour
    {
        public Config Config;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Config == null)
            {
                Config = Resources.Load<Config>($"NPS/Pooling/{this.name}");
                if (!Config) Config = Resources.Load<Config>("NPS/Pooling/Default");
            }
        }
#endif
    }
}