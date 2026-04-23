using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantSO plantSO=null;
    [SerializeField] private int lifeTime=1;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ResetPlant()
    {
        plantSO = null;
        lifeTime=1;
        spriteRenderer.color = new Color(1,1,1,0);
    }
    public void SetPlantSO(PlantSO plantSO)
    {
        this.plantSO = plantSO;
    }
    public PlantSO GetPlantSO()
    {
        return plantSO;
    }
    public void UpdatePlant(int lifeTime)
    {
        this.lifeTime = lifeTime;
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = plantSO.images[lifeTime-1];
    }
}
