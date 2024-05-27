using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHelper
{
    private readonly SaveManager _saveManager;
    private readonly CharacterController _characterController;
    private readonly SceneConfig _sceneConfig;

    public ExitHelper(SaveManager saveManager, CharacterController characterController, SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
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
        SceneManager.LoadScene(_sceneConfig.MenuSceneIndex);
    }
}