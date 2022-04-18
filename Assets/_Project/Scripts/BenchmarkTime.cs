using UnityEngine;

namespace stuartmillman.dissertation
{
    public class BenchmarkTime
    {
        private float _startTime = 0;
        private float _endTime = 0;

        public void Start()
        {
            _startTime = Time.realtimeSinceStartup;
        }

        public void Stop()
        {
            _endTime = Time.realtimeSinceStartup;
        }

        public float GetTimeSeconds()
        {
            return _endTime - _startTime;
        }

        public float GetTimeMilliseconds()
        {
            return (_endTime - _startTime) * 1000;
        }
    }
}