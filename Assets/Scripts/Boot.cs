using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Boot : IAsyncStartable 
{
    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await UniTask.WaitForSeconds(2, cancellationToken: cancellation);
        await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
    }
}