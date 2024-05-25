using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private CharacterController _characterControllerPrefab;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance<ICharacterFactory, CharacterFactory>(new CharacterFactory(_characterControllerPrefab, this));
        builder.RegisterEntryPoint<Boot>();
    }
}
