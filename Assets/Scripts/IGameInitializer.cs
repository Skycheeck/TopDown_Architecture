using System.Threading;
using Cysharp.Threading.Tasks;

public interface IGameInitializer
{
    UniTask InitializeAsync(CancellationToken cancellation);
}