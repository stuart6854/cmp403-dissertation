using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace stuartmillman.dissertation
{
    public class BenchmarkManager : MonoBehaviour
    {
        public static BenchmarkManager Instance { get; private set; }

        private Dictionary<string, BenchmarkTime> _benchmarks = new Dictionary<string, BenchmarkTime>();
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void StartBenchmark(string benchmarkName)
        {
            var benchmark = new BenchmarkTime();
            benchmark.Start();
            _benchmarks.Add(benchmarkName, benchmark);
        }

        public void StopBenchmark(string benchmarkName)
        {
            _benchmarks[benchmarkName].Stop();
        }

        public void OutputBenchmarks(string fileName)
        {
            using var writer = new StreamWriter(fileName);
            foreach (var pairs in _benchmarks)
            {
                var benchmark = pairs.Value;
                writer.WriteLine(pairs.Key + " - " + benchmark.GetTimeSeconds() + "s / " +
                                 benchmark.GetTimeMilliseconds() + "ms");
            }

            writer.Flush();
            writer.Close();

            print("Benchmarks have been written to file: " + fileName);
        }
    }
}