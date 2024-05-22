using UnityEngine;
using UnityEngine.AI;

public class MoveToClickPoint : MonoBehaviour
{
    private NavMeshAgent _agent;
    private const string WALKABLE_AREA = "Walkable Area";

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) return;
        if (!hit.collider.gameObject.CompareTag(WALKABLE_AREA)) return;

        _agent.destination = hit.point;
    }
}