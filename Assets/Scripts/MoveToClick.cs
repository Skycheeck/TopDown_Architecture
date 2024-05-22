using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour
{
    private NavMeshAgent _agent;
    private readonly Queue<Vector3> _destinationPoints = new();
    
    private const string WALKABLE_AREA = "Walkable Area";

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckForInput();
        
        // no destination points
        if (_destinationPoints.Count < 1) return;
        
        // no path yet
        if (!_agent.hasPath)
        {
            SetDestinationPoint();
            return;
        }
        
        // distance is too long
        if (_agent.remainingDistance > _agent.stoppingDistance) return;
        
        SetDestinationPoint();
    }

    private void SetDestinationPoint()
    {
        _agent.destination = _destinationPoints.Dequeue();
    }

    private void CheckForInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) return;
        if (!hit.collider.gameObject.CompareTag(WALKABLE_AREA)) return;

        _destinationPoints.Enqueue(hit.point);
    }
}