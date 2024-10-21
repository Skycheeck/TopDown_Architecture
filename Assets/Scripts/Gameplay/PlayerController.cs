using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

public class PlayerController : ITickable
{
    private readonly CharacterController _characterController;
    private readonly EventSystem _eventSystem;
    private const string WALKABLE_AREA = "Walkable Area";

    public PlayerController(CharacterController characterController, EventSystem eventSystem)
    {
        _eventSystem = eventSystem;
        _characterController = characterController;
    }

    public void Tick()
    {
        if (!Input.GetMouseButtonDown(0) || _eventSystem.IsPointerOverGameObject()) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) return;
        if (!hit.collider.gameObject.CompareTag(WALKABLE_AREA)) return;

        _characterController.AddDestinationPoint(hit.point);
    }
}