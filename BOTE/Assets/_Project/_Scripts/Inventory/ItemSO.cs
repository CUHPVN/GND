using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemSO")]
public class ItemSO : ScriptableObject
{
    
    [Header("Only gameplay")]
    public TileBase tileBase;
    public ItemType itemType;
    public ActionType actionType;
    public Vector2Int range;
    [Header("Only UI")]
    public bool stackable;
    public int maxStack;

    [Header("Both")]
    public Sprite image;
    
}
public enum ItemType
{
    None=0,
    Grass=1,
    Dirt=2,
    Stone=3,
    Wood=4,
    Pickaxe=5,
    Axe=6,
    Shovel=7,
    Hoe=8,
    EmptyWateringCan=9,
    FullWateringCan=10,
    WheatSeed=11,
    CornSeed=12,
    TomatoSeed=13,
    PotatoSeed=14,
    Vaccine=15,
    Wheat=16,
    Corn=17,
    Tomato=18,
    Potato=19,
}
public enum ActionType
{
    None=0,
    Use=1,
    Place=2,
    Remove=3
}
