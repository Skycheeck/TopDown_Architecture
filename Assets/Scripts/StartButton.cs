using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private SceneConfig _sceneConfig;

    [Inject]
    private void Construct(SceneConfig sceneConfig) => _sceneConfig = sceneConfig;

    private void Awake() => GetComponent<Button>().onClick.AddListener(
            () => SceneManager.LoadScene(_sceneConfig.GameSceneIndex));
}