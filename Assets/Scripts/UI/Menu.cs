using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _newButton;
    [SerializeField] private Button _loadButton;
    
    public event Action NewButtonClicked;
    public event Action LoadButtonClicked;

    private void Start()
    {
        _newButton.onClick.AddListener(() => NewButtonClicked?.Invoke());
        _loadButton.onClick.AddListener(() => LoadButtonClicked?.Invoke());
    }
}