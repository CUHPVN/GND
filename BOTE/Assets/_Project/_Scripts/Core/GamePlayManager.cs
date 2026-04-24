using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    private int money=0;
    private int health=100;
    private int day=1;

    public bool BlockInput;

    public int Money=> money;
    public int Health=> health;
    public int Day=> day;
    public event Action<int> OnDayChange;
    public event Action<int> OnHealthChange;
    public event Action<int> OnMoneyChange;

    public void StartGame()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NextDay()
    {
        day++;
        OnDayChange?.Invoke(day);
    }
}
