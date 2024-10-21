using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    public event Action StartButtonClicked;

    private void Start() => _startButton.onClick.AddListener(() => StartButtonClicked?.Invoke());
}