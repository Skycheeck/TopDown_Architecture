using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        Canvas hud = Container.Resolve<IHUDFactory>().Create();
        SceneManager.MoveGameObjectToScene(hud.gameObject, SceneManager.GetActiveScene());

        CharacterController characterController = Container.Resolve<ICharacterFactory>()
            .Create(new PlayerProgress {DestinationPoints = Enumerable.Empty<Vector3>()});
        SceneManager.MoveGameObjectToScene(characterController.gameObject, SceneManager.GetActiveScene());
    }
}
