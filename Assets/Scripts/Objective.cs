using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "Objectives/New Objective")]
public class Objective : ScriptableObject
{
    public string objectiveName;
    public Sprite objectiveSprite;
}
