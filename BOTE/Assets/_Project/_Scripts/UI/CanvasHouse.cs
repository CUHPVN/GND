using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHouse : UICanvas
{
    [Header("House")] 
    [SerializeField] private Image heath;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemCount;
    [SerializeField] private Button healthButton;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private ItemSO syringe;
    public override void Open()
    {
        base.Open();
        GamePlayManager.Instance.BlockInput = true;
        ItemData itemData = CircularManager.Instance.FindItem(syringe);
        itemCount.text = itemData!=null? itemData.count.ToString(): "0";
    }
    public void Start()
    {
        InitEvent();
    }
    public void InitEvent()
    {
        nextDayButton.onClick.AddListener(() => NextDayButton());
        healthButton.onClick.AddListener(() => HealthButton());
        closeButton.onClick.AddListener(() => CloseButton());
    }
    public void OnDisable()
    {
        nextDayButton.onClick.RemoveListener(NextDayButton);
        healthButton.onClick.RemoveListener(HealthButton);
        closeButton.onClick.RemoveListener(CloseButton);
    }
    private void HealthButton()
    {
        ItemData itemData = CircularManager.Instance.FindItem(syringe);
        if(itemData != null)
        {
            if(itemData.count > 0)
            {
                //health++;
                CircularManager.Instance.UseItem();
            }
        }
    }
    public override void CloseDirectly()
    {
        GamePlayManager.Instance.BlockInput = false;
        base.CloseDirectly();
    }

    public void CloseButton()
    {
        Close(0);
    }
    public void NextDayButton()
    {
        GamePlayManager.Instance.NextDay();
        Close(0);
    }
}

