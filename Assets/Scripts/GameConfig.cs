using UnityEngine;

[CreateAssetMenu(menuName = "Config/Create GameConfig", fileName = "GameConfig", order = 0)]

public class GameConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Level { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float AngularSpeed { get; private set; }
    [field: SerializeField] public int MaxDestinationPoints { get; private set; }
}