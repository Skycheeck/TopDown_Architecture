using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    
    public event Action ExitButtonClicked;

    private void Start() => _exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
}