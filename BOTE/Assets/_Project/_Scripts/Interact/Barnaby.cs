using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barnaby : MonoBehaviour,IInteractable
{
    public Vector2Int position;
    Vector2Int IInteractable.position => position;
    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        //AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("open_barnaby"));
        UIManager.Instance.OpenUI<CanvasBarnaby>();
        isItemChanged = false;
        return false;
    }
}
