using UnityEngine;
using UnityEngine.SceneManagement;

namespace stuartmillman.dissertation
{
    public class ScenarioManager : MonoBehaviour
    {
        public static ScenarioManager Instance { get; private set; }

        private static int ScenarioIterations = 3;
        private static int RunCount = 0;
        private static bool FirstScenario = true;

        private bool _noTrees;
        private bool _noRocks;
        private bool _noSticks;

        private bool _isComplete;

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
            if (!_isComplete && _noTrees && _noRocks && _noSticks)
            {
                _isComplete = true;
                EndScenario();
            }
        }

        public void SetNoTrees() => _noTrees = true;
        public void SetNoRocks() => _noRocks = true;
        public void SetNoSticks() => _noSticks = true;

        private void EndScenario()
        {
            print("Scenario has ended!");
            BenchmarkManager.Instance.StopBenchmark("scenario_time");

            var sceneName = SceneManager.GetActiveScene().name;
            var fileName = "benchmarks_" + sceneName + "_" + RunCount + ".txt";
            BenchmarkManager.Instance.OutputBenchmarks(fileName);

            if (!FirstScenario)
            {
                print("Quiting Application");
                Application.Quit();
            }

            if (RunCount < ScenarioIterations - 1)
            {
                print("Next Scenario Run");

                RunCount++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (RunCount == ScenarioIterations)
            {
                print("Next Scenario");
                RunCount = 0;
                FirstScenario = false;

                var sceneIndex = SceneManager.GetActiveScene().buildIndex;
                sceneIndex = (sceneIndex + 1) % 2;
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}