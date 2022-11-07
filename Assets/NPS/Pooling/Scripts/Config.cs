using UnityEngine;

namespace NPS.Pooling
{
    [CreateAssetMenu(fileName = "Config", menuName = "NPS/Pooling")]
    public class Config : ScriptableObject
    {
        public TypePool Type = TypePool.Stack;
        public bool Check = false;
        public int Capacity = 10;
        public int Max = 20;
    }
}