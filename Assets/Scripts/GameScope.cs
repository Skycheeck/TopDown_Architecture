using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        Parent.Container.Instantiate(Parent.Container.Resolve<GameConfig>().Level);
        
        Canvas hud = Parent.Container.Resolve<IHUDFactory>().Create();
        SceneManager.MoveGameObjectToScene(hud.gameObject, SceneManager.GetActiveScene());

        CharacterController characterController = Parent.Container.Resolve<ICharacterFactory>()
            .Create(new PlayerProgress {DestinationPoints = Enumerable.Empty<Vector3>()});
        SceneManager.MoveGameObjectToScene(characterController.gameObject, SceneManager.GetActiveScene());

        builder.RegisterInstance(hud);
        builder.RegisterInstance(characterController);
        builder.Register<PlayerController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }
}
