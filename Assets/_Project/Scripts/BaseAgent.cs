using System;
using UnityEngine;
using UnityEngine.AI;

namespace stuartmillman.dissertation
{
    public class BaseAgent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private NavMeshAgent _navAgent;
        private Inventory _inventory;

        public Inventory Inventory => _inventory;

        public string OriginalAgentName { get; private set; }

        protected virtual void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
            _inventory = GetComponent<Inventory>();

            OriginalAgentName = this.gameObject.name;
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", _navAgent.velocity.magnitude);
        }

        public void TriggerChopAnim()
        {
            _animator.SetTrigger("Chop");
        }

        public void TriggerFarmAnim()
        {
            _animator.SetTrigger("Farm");
        }

        public void MoveTo(Vector3 position)
        {
            _navAgent.destination = position;
            _navAgent.isStopped = false;
        }

        public bool AtDestination()
        {
            if (_navAgent.pathPending) return false;
            if (_navAgent.remainingDistance >= _navAgent.stoppingDistance) return false;
            if (_navAgent.pathStatus == NavMeshPathStatus.PathComplete)
                return true;

            return false;
        }
    }
}