using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Boot : IAsyncStartable 
{
    private SceneConfig _sceneConfig;

    public Boot(SceneConfig sceneConfig) => _sceneConfig = sceneConfig;

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await UniTask.WaitForSeconds(2, cancellationToken: cancellation);
        await SceneManager.LoadSceneAsync(_sceneConfig.MenuSceneIndex, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
    }
}