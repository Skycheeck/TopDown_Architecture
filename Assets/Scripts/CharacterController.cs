using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class CharacterController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Queue<Vector3> _destinationPoints;

    [Inject]
    private void Construct(Queue<Vector3> destinationPoints) => _destinationPoints = destinationPoints;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    private void Update()
    {
        // no destination points
        if (_destinationPoints.Count < 1) return;
        
        // no path yet
        if (!_agent.hasPath)
        {
            DequeueDestinationPoint();
            return;
        }
        
        // distance is too long
        if (_agent.remainingDistance > _agent.stoppingDistance) return;
        
        DequeueDestinationPoint();
    }

    public void AddDestinationPoint(Vector3 destinationPoint) => _destinationPoints.Enqueue(destinationPoint);

    private void DequeueDestinationPoint() => _agent.destination = _destinationPoints.Dequeue();
}