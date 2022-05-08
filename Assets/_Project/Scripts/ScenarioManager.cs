using UnityEngine;
using UnityEngine.SceneManagement;

namespace stuartmillman.dissertation
{
    public class ScenarioManager : MonoBehaviour
    {
        public static ScenarioManager Instance { get; private set; }

        private static int ScenarioIterations = 3;
        private static int RunIndex = 1;
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
            var trees = FindObjectsOfType<Tree>();
            if (trees == null || trees.Length == 0)
                SetNoTrees();
            var rocks = FindObjectsOfType<Rock>();
            if (rocks == null || rocks.Length == 0)
                SetNoRocks();
            var sticks = FindObjectsOfType<Sticks>();
            if (sticks == null || sticks.Length == 0)
                SetNoSticks();

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
            var fileName = "benchmarks_" + sceneName + "_" + RunIndex + ".txt";
            BenchmarkManager.Instance.OutputBenchmarks(fileName);

            RunIndex++;
            if (RunIndex <= ScenarioIterations)
            {
                print("Next Scenario Run " + RunIndex);
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (RunIndex > ScenarioIterations)
            {
                if (!FirstScenario)
                {
                    print("Quiting Application");
                    Application.Quit();
                }
                
                print("Next Scenario");
                RunIndex = 1;
                FirstScenario = false;

                var sceneIndex = SceneManager.GetActiveScene().buildIndex;
                sceneIndex = (sceneIndex + 1) % 2;
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}