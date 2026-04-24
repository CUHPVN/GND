using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/PlantSO")]
public class PlantSO : ScriptableObject
{
    
    [Header("Only gameplay")]
    public PlantType plantType;

    [Header("Only UI")]
    public int growTime=3;

    [Header("Both")]
    public Sprite[] images;

    public int count=2;
    
}
public enum PlantType
{
    None=0,
    Wheat=1,
    Corn=2,
    Tomato=3,
    Potato=4,
    Carrot=5,
}
