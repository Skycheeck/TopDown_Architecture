using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootScope : LifetimeScope
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private SceneConfig _sceneConfig;
    [SerializeField] private CharacterController _characterControllerPrefab;
    [SerializeField] private SaveManager _saveManagerPrefab;
    
    protected override void Configure(IContainerBuilder builder)
    {
        RegisterSaveManager(builder);
        builder.RegisterInstance(_gameConfig);
        builder.RegisterInstance(_sceneConfig);
        builder.RegisterInstance<ICharacterFactory, CharacterFactory>(new CharacterFactory(_characterControllerPrefab, this, _gameConfig));
        builder.RegisterEntryPoint<Boot>();
    }

    private void RegisterSaveManager(IContainerBuilder builder)
    {
        SaveManager saveManager = Instantiate(_saveManagerPrefab);
        DontDestroyOnLoad(saveManager);
        builder.RegisterInstance(saveManager);
    }
}
