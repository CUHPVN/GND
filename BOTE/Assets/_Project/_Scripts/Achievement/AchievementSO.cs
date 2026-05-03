using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement/Definition")]
public class AchievementSO : ScriptableObject
{
    public string achievementID;
    public string description;
    [Header("Requirements")]
    public AchievementSO prerequisite;
}