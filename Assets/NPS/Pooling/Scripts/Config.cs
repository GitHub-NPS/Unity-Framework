using UnityEngine;

namespace NPS.Pooling
{
    [CreateAssetMenu(fileName = "Config", menuName = "NPS/Pooling")]
    public class Config : ScriptableObject
    {
        public TypePool Type = TypePool.Stack;
        public bool Check = false;
        public int Max = 20;

        public int Capacity = 10;        

        public bool IsScan = false;
        public float LifeTime = 30f;
        public float TimeScan = 10f;
    }
}