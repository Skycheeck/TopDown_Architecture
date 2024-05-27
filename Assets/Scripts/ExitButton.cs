using UnityEngine;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(Button))]
public class ExitButton : MonoBehaviour
{
    private ExitHelper _exitHelper;

    [Inject]
    private void Construct(ExitHelper exitHelper) => _exitHelper = exitHelper;

    private void Awake() => GetComponent<Button>().onClick.AddListener(() => _exitHelper.Exit());
}