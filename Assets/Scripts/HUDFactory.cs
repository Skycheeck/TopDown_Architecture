using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class HUDFactory : IHUDFactory
{
    private readonly Canvas _prefab;
    private readonly IObjectResolver _objectResolver;

    public HUDFactory(Canvas prefab, IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        _prefab = prefab;
    }

    public Canvas Create()
    {
        Canvas instance = _objectResolver.Instantiate(_prefab);
        SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
        return instance;
    }
}