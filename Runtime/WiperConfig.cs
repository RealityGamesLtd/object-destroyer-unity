using UnityEngine;

namespace ObjectDestroyer
{
    [CreateAssetMenu(fileName = nameof(WiperConfig), menuName = "Object Destroyer/Config")]
    public class WiperConfig : ScriptableObject
    {
        public float TimestampOffset;
        public int DeleteAmount;
    }
}