using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : IGameInitializer
{
    private readonly ICharacterFactory _characterFactory;

    public GameInitializer(ICharacterFactory characterFactory)
    {
        _characterFactory = characterFactory;
    }

    public async UniTask InitializeAsync(CancellationToken cancellation)
    {
        await SceneManager.LoadSceneAsync(2, LoadSceneMode.Single).ToUniTask(cancellationToken: cancellation);
        _characterFactory.Create(new PlayerProgress {DestinationPoints = Enumerable.Empty<Vector3>()});
    }
}