using System;
using UnityEngine;
using UnityEngine.AI;

namespace ErrorSpace
{
    public class NavmeshExample : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;

        private NavMeshAgent agent;

        private void Start()
        {
            agent = transform.GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void Update()
        {
            agent.SetDestination(targetTransform.position);
        }
    }
}
