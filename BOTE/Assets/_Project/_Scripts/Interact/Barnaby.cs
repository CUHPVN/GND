using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barnaby : MonoBehaviour,IInteractable
{
    public Vector2Int position;
    Vector2Int IInteractable.position => position;
    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        if(AchievementManager.Instance != null) AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("interact_with_barnaby"));
        if(UIManager.Instance != null) UIManager.Instance.OpenUI<CanvasBarnaby>();
        isItemChanged = false;
        return false;
    }
}
