using UnityEngine;

namespace ObjectDestroyer
{
    public class Destroyer : MonoBehaviour
    {
        private const float TIMESTAMP_OFFSET = 2f;
        private const int DELETE_AMOUNT = 10;

        private static Wiper wiper;

        public static new void Destroy(Object obj)
        {
            if(wiper == null)
            {
                Debug.LogError("No componet for wiping objects, initializing!");
                Initialize();
            }

            wiper.AddTo(obj);
        }

        public static void Stop(Object obj)
        {
            if (wiper == null)
            {
                Debug.LogError("No componet for wiping objects, initializing!");
                Initialize();
            }

            wiper.Stop(obj);
        }

        public static void Initialize()
        {
            var go = new GameObject("Wiper");
            wiper = go.AddComponent<Wiper>();

            var config = Resources.Load<WiperConfig>("WiperConfig");
            if (config != null) wiper.Initialize(config.TimestampOffset, config.DeleteAmount, config.IsLogging);
            else
            {
                Debug.LogError("No config for object destroyer! Initializing with default values!");
                wiper.Initialize(TIMESTAMP_OFFSET, DELETE_AMOUNT, false);
            }
        }
    }
}
