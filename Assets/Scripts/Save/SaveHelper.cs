using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveHelper
{
    private readonly SaveManager _saveManager;
    private readonly CharacterController _characterController;

    public SaveHelper(SaveManager saveManager, CharacterController characterController)
    {
        _saveManager = saveManager;
        _characterController = characterController;
    }

    public void Exit()
    {
        IEnumerable<Vector3> destinationPoints = _characterController.NavMeshAgent.hasPath
            ? _characterController.DestinationPoints.Prepend(_characterController.NavMeshAgent.destination)
            : _characterController.DestinationPoints;

        _saveManager.Save(new PlayerProgress
        {
            Position = _characterController.transform.position,
            Rotation = _characterController.transform.rotation,
            DestinationPoints = destinationPoints.ToArray()
        });
    }
}