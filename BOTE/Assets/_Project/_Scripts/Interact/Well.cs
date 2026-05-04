using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour,IInteractable
{
    public ItemSO emptyWateringCan;
    public ItemSO fullWateringCan;
    public Vector2Int position;
    Vector2Int IInteractable.position => position;
    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        isItemChanged = false;
        if(AchievementManager.Instance == null) return false;
        if (currentItem.itemSO == emptyWateringCan)
        {
            AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("fill_wateringcan"));
            currentItem.itemSO = fullWateringCan;
            currentItem.count=5;
            isItemChanged = true;
        }else
        if (currentItem.itemSO == fullWateringCan)
        {
            AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("fill_wateringcan"));
            currentItem.itemSO = fullWateringCan;
            currentItem.count=5;
            isItemChanged = true;
        }
        return false;
    }
}
