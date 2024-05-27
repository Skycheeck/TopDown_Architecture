using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    protected override async void Configure(IContainerBuilder builder)
    {
        GameObject level = CreateLevel();
        Canvas hud = CreateHUD();
        CharacterController characterController = await CreateCharacter();

        CreateChild(builder =>
        {
            builder.RegisterInstance(hud);
            builder.RegisterInstance(characterController);
            builder.Register<PlayerController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        });
    }

    private Canvas CreateHUD()
    {
        Canvas hud = Parent.Container.Resolve<IHUDFactory>().Create();
        SceneManager.MoveGameObjectToScene(hud.gameObject, SceneManager.GetActiveScene());
        return hud;
    }

    private GameObject CreateLevel()
    {
        GameObject level = Parent.Container.Instantiate(Parent.Container.Resolve<GameConfig>().Level);
        SceneManager.MoveGameObjectToScene(level, SceneManager.GetActiveScene());
        return level;
    }

    private async UniTask<CharacterController> CreateCharacter()
    {
        // PlayerProgress playerProgress = new() {DestinationPoints = Array.Empty<Vector3>()};
        PlayerProgress playerProgress = await Parent.Container.Resolve<SaveManager>().Load();

        CharacterController characterController =
            Parent.Container.Resolve<ICharacterFactory>().Create(playerProgress);

        SceneManager.MoveGameObjectToScene(characterController.gameObject, SceneManager.GetActiveScene());
        return characterController;
    }
}