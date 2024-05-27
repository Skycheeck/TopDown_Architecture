using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CharacterFactory : ICharacterFactory
{
    private readonly CharacterController _prefab;
    private readonly LifetimeScope _currentScope;
    private readonly GameConfig _gameConfig;

    public CharacterFactory(CharacterController prefab, LifetimeScope currentScope, GameConfig gameConfig)
    {
        _currentScope = currentScope;
        _prefab = prefab;
        _gameConfig = gameConfig;
    }
    
    public CharacterController Create(PlayerProgress playerProgress)
    {
        using LifetimeScope childScope = _currentScope.CreateChild(
            builder => builder.RegisterInstance(
                new FixedQueue<Vector3>(playerProgress.DestinationPoints.Take(_gameConfig.MaxDestinationPoints), _gameConfig.MaxDestinationPoints)));
        
        CharacterController instance = childScope.Container.Instantiate(_prefab);
        instance.transform.position = playerProgress.Position;
        instance.transform.rotation = playerProgress.Rotation;
        instance.NavMeshAgent.speed = _gameConfig.Speed;
        instance.NavMeshAgent.angularSpeed = _gameConfig.AngularSpeed;
        return instance;
    }
}