using System;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class ScenarioManager : MonoBehaviour
    {
        public static ScenarioManager Instance { get; private set; }

        private bool _noTrees;
        private bool _noRocks;
        private bool _noSticks;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            BenchmarkManager.Instance.StartBenchmark("scenario_time");
        }

        private void Update()
        {
            if (_noTrees && _noRocks && _noSticks)
            {
                BenchmarkManager.Instance.StopBenchmark("scenario_time");
                BenchmarkManager.Instance.OutputBenchmarks();
            }
        }

        public void SetNoTrees() => _noTrees = true;
        public void SetNoRocks() => _noRocks = true;
        public void SetNoSticks() => _noSticks = true;
    }
}