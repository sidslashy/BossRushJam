using UnityEngine;
using UnityEngine.Events;

namespace BossRushJam.Scripts
{
    public class BeatManager : MonoBehaviour
    {
        [SerializeField] private float bpm;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Intervals[] intervals;


        private void Update()
        {
            foreach (var interval in intervals)
            {
                var sampledTime = audioSource.timeSamples / (audioSource.clip.frequency * interval.GetIntervalLength(bpm));
                interval.CheckForNewInterval(sampledTime);

            }
        }

        public void BeatTrigger()
        {
            print("Beat Triggered");
        }
    }

    [System.Serializable]
    public class Intervals
    {
        [SerializeField] private float steps;
        [SerializeField] private UnityEvent trigger;

        private int _lastInterval;

        public float GetIntervalLength(float bpm)
        {
            return 60f / (bpm * steps);
        }

        public void CheckForNewInterval(float interval)
        {
            if (Mathf.FloorToInt(interval) != _lastInterval)
            {
                _lastInterval = Mathf.FloorToInt(interval);
                trigger.Invoke();
            }
        }
    }
}