using System.Collections.Generic;
using UnityEngine;

namespace ObjectDestroyer
{
    public class Wiper : MonoBehaviour
    {
        private float timestampOffset;
        private int deleteAmount;
        private bool isLogging;
        private float timer = 0f;

        internal List<Object> ToDestroy { get; } = new List<Object>();

        internal void AddTo(Object obj) => ToDestroy.Add(obj);
        internal void Stop(Object obj)
        {
            if(ToDestroy.Contains(obj)) ToDestroy.Remove(obj);
        }

        internal void Initialize(float offset, int amount, bool logger)
        {
            timestampOffset = offset;
            deleteAmount = amount;
            isLogging = logger;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > timestampOffset)
            {
                Wipe();
                timer = 0f;
            }
        }

        private void Wipe()
        {
            long beforeBytes = 0;
            if (isLogging) beforeBytes = System.GC.GetTotalMemory(false);

            for(int i = 0; i < deleteAmount; ++i)
            {
                if(i < ToDestroy.Count)
                {
                    var obj = ToDestroy[i];
                    ToDestroy.Remove(obj);
                    if (obj != null) Destroy(obj);
                }
            }

            System.GC.Collect();

            if (isLogging)
            {
                var afterBytes = System.GC.GetTotalMemory(false);
                Debug.LogWarning($"Wiping from memory: {beforeBytes - afterBytes} bytes");
            }
        }
    }
}