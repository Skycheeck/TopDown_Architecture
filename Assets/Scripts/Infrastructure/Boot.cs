using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Boot : IAsyncStartable
{
    private readonly GameStarter _gameStarter;
    private readonly SceneConfig _sceneConfig;

    public Boot(GameStarter gameStarter, SceneConfig sceneConfig)
    {
        _gameStarter = gameStarter;
        _sceneConfig = sceneConfig;
    }

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        while (true)
        {
            await _gameStarter.StartAsync(cancellation);
            await SceneManager.LoadSceneAsync(_sceneConfig.BootSceneIndex);
        }
    }
}