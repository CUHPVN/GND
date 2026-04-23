using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Config/SeedToPlantConfigSO")]

public class SeedToPlantConfigSO : ScriptableObject
{
    public List<ItemSO> seeds=new ();
    public List<PlantSO> plants=new ();
    
    public List<ItemSO> crops=new ();
    public PlantSO GetPlant(ItemSO seed)
    {
        for (int i = 0; i < seeds.Count; i++)
        {
            if (seeds[i] == seed)
            {
                return plants[i];
            }
        }
        return null;
    }
    public ItemSO GetCrop(PlantSO seed)
    {
        for (int i = 0; i < plants.Count; i++)
        {
            if (plants[i] == seed)
            {
                return crops[i];
            }
        }
        return null;
    }
}
