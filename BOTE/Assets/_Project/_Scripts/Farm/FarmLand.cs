using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUHP;
using System;
using Unity.VisualScripting;

public class FarmLand : MonoBehaviour,IInteractable
{
    public IsometricGrid<FarmLand> grid;
    public int x;
    public int y;
    public Vector2Int position => new Vector2Int(x,y);
    //
    [SerializeField] private Plant plant;
    [SerializeField] private PlantSO plantSO;
    [SerializeField] private int lifeTime=1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite dry;
    [SerializeField] private Sprite plowed;
    [SerializeField] private Sprite wet;
    [SerializeField] private ItemSO fullWateringCan;
    [SerializeField] private ItemSO hoe;
    [SerializeField] private SeedToPlantConfigSO plantSOConfig;
    [SerializeField] private ItemSO[] seeds;
    private bool isDry=true;
    private bool isPlowed=false;
    private bool canHarvest=false;

    public void Start()
    {
        GamePlayManager.Instance.OnDayChange += NextDay;
        plant.ResetPlant();
    }
    public void ResetFarmLand()
    {
        isDry=true;
        isPlowed=false;
        canHarvest=false;
        lifeTime=1;
        //Plan = null;
        plantSO = null;
        plant.ResetPlant();
        ChangeVisual();
    }
    public void NextDay(int day)
    {
        if(plantSO == null) {
            isDry = true;
            ChangeVisual();
            return;
        }
        if (isDry)
        {
            ChangeVisual();
            return;
        }
        if(lifeTime<plantSO.growTime) lifeTime++;
        if(lifeTime >= plantSO.growTime)
        {
            //Has Plant Grow 
            canHarvest=true;
        }
        isDry=true;
        plant.UpdatePlant(lifeTime);
        ChangeVisual();
    }
    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        isItemChanged=false;
        if (canHarvest)
        {
            canHarvest=false;
            //INv
            Harvest();
            ResetFarmLand();
            ChangeVisual();
            return false;
        }else
        if(isPlowed && isDry && currentItem.itemSO == fullWateringCan)
        {
            isDry=false;
            ChangeVisual();
            return true;
        }else
        if(isPlowed==false && currentItem.itemSO == hoe)
        {
            isPlowed=true;
            ChangeVisual();
            return false;
        }if(!isDry && plant.GetPlantSO() == null && CheckSeed(currentItem.itemSO,out ItemSO seed))
        {
            PlantSO plantSO = plantSOConfig.GetPlant(seed);
            this.plantSO = plantSO;
            plant.SetPlantSO(plantSO);
            plant.UpdatePlant(lifeTime);
            ChangeVisual();
            return true;
        }
        return false;
    }
    private void Harvest()
    {
        ItemSO cropSO = plantSOConfig.GetCrop(this.plantSO);
        CircularManager.Instance.AddItem(cropSO,plantSO.count);
    }
    private bool CheckSeed(ItemSO item,out ItemSO seed)
    {
        for(int i=0; i<seeds.Length; i++)
        {
            if(seeds[i] == item)
            {
                seed = seeds[i];
                return true;
            }
        }
        seed = null;
        return false;
    }
    private void ChangeVisual()
    {
        if (!isPlowed && isDry)
        {
            spriteRenderer.sprite = dry;
        }
        if (isPlowed && isDry)
        {
            spriteRenderer.sprite = plowed;
        }
        if (isPlowed && !isDry)
        {
            spriteRenderer.sprite = wet;
        }
    }
    // public FarmLand(IsometricGrid<FarmLand> grid, int x, int y)
    // {
    //     this.grid = grid;
    //     this.x = x;
    //     this.y = y;
    // }
}

