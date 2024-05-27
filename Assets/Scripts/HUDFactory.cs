using UnityEngine;
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

    public Canvas Create() => _objectResolver.Instantiate(_prefab);
}