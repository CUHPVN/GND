using System;
using UnityEngine;

public class SetWorldBounds : MonoBehaviour
{
    
    private void Start()
    {
        SetBound(transform.position);
    }
    public void SetBound(Vector3 center)
    {
        transform.position= center;
        var bounds = GetComponent<SpriteRenderer>().bounds;
        Globals.WorldBounds = bounds;
        Globals.bound = this;
    }
}