﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class Boot : IAsyncStartable 
{
    private readonly LifetimeScope _lifetimeScope;
    private readonly SceneConfig _sceneConfig;
    private readonly GameConfig _gameConfig;
    private readonly SaveManager _saveManager;
    private readonly MenuInputRequest _menuInputRequest;
    private readonly IHUDFactory _hudFactory;
    private readonly ICharacterFactory _characterFactory;
    private readonly IObjectResolver _objectResolver;

    public Boot(LifetimeScope lifetimeScope, SceneConfig sceneConfig, GameConfig gameConfig, SaveManager saveManager, MenuInputRequest menuInputRequest,
        IHUDFactory hudFactory, ICharacterFactory characterFactory, IObjectResolver objectResolver)
    {
        _menuInputRequest = menuInputRequest;
        _lifetimeScope = lifetimeScope;
        _sceneConfig = sceneConfig;
        _gameConfig = gameConfig;
        _saveManager = saveManager;
        _hudFactory = hudFactory;
        _characterFactory = characterFactory;
        _objectResolver = objectResolver;
    }

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await UniTask.WaitForSeconds(2, cancellationToken: cancellation); //fake loading
        await _menuInputRequest.Request();
        await SceneManager.LoadSceneAsync(_sceneConfig.MenuSceneIndex, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
        
        CreateLevel();
        CharacterController characterController = CreateCharacter(await _saveManager.Load());
        HUD hud = CreateHUD(_hudFactory);

        _lifetimeScope.CreateChild(builder =>
        {
            builder.RegisterInstance(characterController);
            builder.RegisterInstance(hud);
            builder.Register<PlayerController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<ExitHelper>(Lifetime.Scoped);
            builder.RegisterBuildCallback(resolver => resolver.Resolve<HUD>().ExitButtonClicked += resolver.Resolve<ExitHelper>().Exit);
        });
    }
    
    private GameObject CreateLevel()
    {
        GameObject level = _objectResolver.Instantiate(_gameConfig.Level);
        SceneManager.MoveGameObjectToScene(level, SceneManager.GetActiveScene());
        return level;
    }

    private CharacterController CreateCharacter(PlayerProgress playerProgress)
    {
        CharacterController characterController = _characterFactory.Create(playerProgress);
        SceneManager.MoveGameObjectToScene(characterController.gameObject, SceneManager.GetActiveScene());
        return characterController;
    }
    private HUD CreateHUD(IHUDFactory hudFactory)
    {
        HUD hud = hudFactory.Create();
        SceneManager.MoveGameObjectToScene(hud.gameObject, SceneManager.GetActiveScene());
        return hud;
    }
}