using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour,IInteractable
{
    public Vector2Int position;

    Vector2Int IInteractable.position => position;

    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        if(AchievementManager.Instance != null) AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("open_house"));
        if(UIManager.Instance != null) UIManager.Instance.OpenUI<CanvasHouse>();
        isItemChanged = false;
        return false;
    }

  
}
