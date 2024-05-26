using UnityEngine;

[CreateAssetMenu(menuName = "Config/Create SceneConfig", fileName = "SceneConfig", order = 0)]
public class SceneConfig : ScriptableObject
{
    [field: SerializeField] public int MenuSceneIndex { get; private set; }
    [field: SerializeField] public int GameSceneIndex { get; private set; }
}