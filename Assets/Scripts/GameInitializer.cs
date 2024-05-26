using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : IGameInitializer
{
    private readonly ICharacterFactory _characterFactory;
    private readonly IHUDFactory _hudFactory;
    private readonly SceneConfig _sceneConfig;

    public GameInitializer(ICharacterFactory characterFactory, IHUDFactory hudFactory, SceneConfig sceneConfig)
    {
        _characterFactory = characterFactory;
        _hudFactory = hudFactory;
        _sceneConfig = sceneConfig;
    }

    public async UniTask InitializeAsync(CancellationToken cancellation)
    {
        await SceneManager.LoadSceneAsync(_sceneConfig.GameSceneIndex, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
        _hudFactory.Create();
        _characterFactory.Create(new PlayerProgress {DestinationPoints = Enumerable.Empty<Vector3>()});
    }
}