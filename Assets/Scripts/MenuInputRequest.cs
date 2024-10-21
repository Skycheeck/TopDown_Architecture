using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class MenuInputRequest
{
    private UniTaskCompletionSource<Result> _gameStarted;
    private readonly Menu _menuPrefab;
    private readonly IObjectResolver _objectResolver;

    public MenuInputRequest(Menu menuPrefab, IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        _menuPrefab = menuPrefab;
    }
    
    public UniTask<Result> Request()
    {
        _gameStarted = new UniTaskCompletionSource<Result>();
        Menu menu = _objectResolver.Instantiate(_menuPrefab);
        SceneManager.MoveGameObjectToScene(menu.gameObject, SceneManager.GetActiveScene());
        menu.NewButtonClicked += () => _gameStarted.TrySetResult(Result.New);
        menu.LoadButtonClicked += () => _gameStarted.TrySetResult(Result.Load);
        return _gameStarted.Task;
    }

    public enum Result
    {
        New,
        Load
    }
}