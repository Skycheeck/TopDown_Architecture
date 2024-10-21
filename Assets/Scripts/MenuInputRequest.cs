using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class MenuInputRequest
{
    private UniTaskCompletionSource _gameStarted;
    private readonly Menu _menuPrefab;
    private readonly IObjectResolver _objectResolver;

    public MenuInputRequest(Menu menuPrefab, IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        _menuPrefab = menuPrefab;
    }
    
    public UniTask Request()
    {
        _gameStarted = new UniTaskCompletionSource();
        Menu menu = _objectResolver.Instantiate(_menuPrefab);
        SceneManager.MoveGameObjectToScene(menu.gameObject, SceneManager.GetActiveScene());
        menu.StartButtonClicked += MenuOnStartButtonClicked;
        return _gameStarted.Task;
    }

    private void MenuOnStartButtonClicked() => _gameStarted.TrySetResult();
}