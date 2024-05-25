using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class Boot : IAsyncStartable 
{
    private readonly ICharacterFactory _characterFactory;

    public Boot(ICharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
        _characterFactory.Create(new PlayerProgress {DestinationPoints = Enumerable.Empty<Vector3>()});
    }
}