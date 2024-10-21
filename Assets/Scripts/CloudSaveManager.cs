using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Core;
using UnityEngine;

public class CloudSaveManager : SaveManager
{
    private const string PROGRESS_KEY = "progress";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public override async UniTask<PlayerProgress> Load()
    {
        Dictionary<string, Item> playerData =
            await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {PROGRESS_KEY});

        try
        {
            return playerData.TryGetValue(PROGRESS_KEY, out Item item)
                ? item.Value.GetAs<PlayerProgress>()
                : PlayerProgress.Empty;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return PlayerProgress.Empty;
        }
    }

    public override async UniTask Save(PlayerProgress progress)
    {
        Dictionary<string, object> playerData = new()
        {
            {PROGRESS_KEY, progress}
        };

        Dictionary<string,string> saveAsync = await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
    }
}