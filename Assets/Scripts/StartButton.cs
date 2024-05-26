using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private IGameInitializer _gameInitializer;

    [Inject]
    private void Construct(IGameInitializer gameInitializer) => _gameInitializer = gameInitializer;

    private void Awake() => GetComponent<Button>().onClick.AddListener(() => _gameInitializer.InitializeAsync(CancellationToken.None));
}