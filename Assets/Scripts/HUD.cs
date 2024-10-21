using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    
    public event Action OnExitClicked;

    private void Awake() => _exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
}