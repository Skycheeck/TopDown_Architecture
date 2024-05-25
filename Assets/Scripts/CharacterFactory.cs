using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CharacterFactory : ICharacterFactory
{
    private readonly CharacterController _prefab;
    private readonly LifetimeScope _currentScope;

    public CharacterFactory(CharacterController prefab, LifetimeScope currentScope)
    {
        _currentScope = currentScope;
        _prefab = prefab;
    }
    
    public CharacterController Create(PlayerProgress playerProgress)
    {
        LifetimeScope childScope = _currentScope.CreateChild(builder => builder.RegisterInstance(new Queue<Vector3>(playerProgress.DestinationPoints)));
        CharacterController instance = childScope.Container.Instantiate(_prefab);
        instance.transform.position = playerProgress.Position;
        instance.transform.rotation = playerProgress.Rotation;
        return instance;
    }
}