using VContainer;
using VContainer.Unity;

public class HUDFactory : IHUDFactory
{
    private readonly HUD _prefab;
    private readonly IObjectResolver _objectResolver;

    public HUDFactory(HUD prefab, IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        _prefab = prefab;
    }

    public HUD Create() => _objectResolver.Instantiate(_prefab);
}