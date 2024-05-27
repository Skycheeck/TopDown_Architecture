using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

public class PlayerController : ITickable
{
    private readonly CharacterController _characterController;
    private const string WALKABLE_AREA = "Walkable Area";

    public PlayerController(CharacterController characterController) => _characterController = characterController;

    public void Tick()
    {
        if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) return;
        if (!hit.collider.gameObject.CompareTag(WALKABLE_AREA)) return;

        _characterController.AddDestinationPoint(hit.point);
    }
}