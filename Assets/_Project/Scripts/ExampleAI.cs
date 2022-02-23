using System;
using stuartmillman.dissertation.goap;
using UnityEngine;

namespace stuartmillman.dissertation
{
    [RequireComponent(typeof(GAgent))]
    public class ExampleAI : MonoBehaviour
    {
        private GAgent _gAgent;

        private void Awake()
        {
            _gAgent = GetComponent<GAgent>();
        }

        private void Start()
        {
            _gAgent.AddGoal("wood", 5);

            _gAgent.Plan();
        }
    }
}