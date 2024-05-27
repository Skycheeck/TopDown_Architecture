using UnityEngine;
using UnityEngine.AI;
using VContainer;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterController : MonoBehaviour
{
    [field: SerializeField] public NavMeshAgent NavMeshAgent;
    private FixedQueue<Vector3> _destinationPoints;

    [Inject]
    private void Construct(FixedQueue<Vector3> destinationPoints) => _destinationPoints = destinationPoints;

    private void Update()
    {
        // no destination points
        if (_destinationPoints.Count < 1) return;
        
        // no path yet
        if (!NavMeshAgent.hasPath)
        {
            DequeueDestinationPoint();
            return;
        }
        
        // distance is too long
        if (NavMeshAgent.remainingDistance > NavMeshAgent.stoppingDistance) return;
        
        DequeueDestinationPoint();
    }

    public void AddDestinationPoint(Vector3 destinationPoint) => _destinationPoints.Enqueue(destinationPoint);

    private void DequeueDestinationPoint() => NavMeshAgent.destination = _destinationPoints.Dequeue();
}