using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SaveManager : MonoBehaviour
{
    public abstract UniTask<PlayerProgress> Load();
    public abstract UniTask Save(PlayerProgress progress);
}