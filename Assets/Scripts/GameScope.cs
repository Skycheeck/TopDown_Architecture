using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private Canvas _hudPrefab;
    protected override async void Configure(IContainerBuilder builder)
    {
        GameObject level = CreateLevel();
        CharacterController characterController = await CreateCharacter();

        CreateChild(builder =>
        {
            builder.RegisterInstance(characterController);
            builder.Register<PlayerController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<ExitHelper>(Lifetime.Scoped);
            builder.Register<Canvas>(CreateHUD, Lifetime.Scoped);
            builder.RegisterBuildCallback(resolver => resolver.Resolve<Canvas>());
        });
    }

    private Canvas CreateHUD(IObjectResolver objectResolver)
    {
        Canvas hud = objectResolver.Instantiate(_hudPrefab);
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
        PlayerProgress playerProgress = await Parent.Container.Resolve<SaveManager>().Load();

        CharacterController characterController =
            Parent.Container.Resolve<ICharacterFactory>().Create(playerProgress);

        SceneManager.MoveGameObjectToScene(characterController.gameObject, SceneManager.GetActiveScene());
        return characterController;
    }
}