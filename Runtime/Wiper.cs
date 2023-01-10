using System.Collections.Generic;
using UnityEngine;

namespace ObjectDestroyer
{
    public class Wiper : MonoBehaviour
    {
        private float timestampOffset;
        private int deleteAmount;
        private float timer = 0f;

        internal List<Object> ToDestroy { get; } = new List<Object>();

        internal void AddTo(Object obj) => ToDestroy.Add(obj);
        internal void Stop(Object obj)
        {
            if(ToDestroy.Contains(obj)) ToDestroy.Remove(obj);
        }

        internal void Initialize(float offset, int amount)
        {
            timestampOffset = offset;
            deleteAmount = amount;
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
            for(int i = 0; i < deleteAmount; ++i)
            {
                if(i < ToDestroy.Count)
                {
                    var obj = ToDestroy[i];
                    ToDestroy.Remove(obj);
                    if (obj != null) Destroy(obj);
                }
            }
        }
    }
}