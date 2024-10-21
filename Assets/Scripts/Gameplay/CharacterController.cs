using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterController : MonoBehaviour
{
    [field: SerializeField] public NavMeshAgent NavMeshAgent;
    public IEnumerable<Vector3> DestinationPoints => _destinationPoints.GetImmutable();
    
    private FixedQueue<Vector3> _destinationPoints;

    [Inject]
    private void Construct(FixedQueue<Vector3> destinationPoints) => _destinationPoints = destinationPoints;

    private void Update()
    {
        if (_destinationPoints.Count < 1) return;
        if (NavMeshAgent.hasPath) return;
        
        DequeueDestinationPoint();
    }

    public void AddDestinationPoint(Vector3 destinationPoint) => _destinationPoints.Enqueue(destinationPoint);

    private void DequeueDestinationPoint() => NavMeshAgent.destination = _destinationPoints.Dequeue();
}