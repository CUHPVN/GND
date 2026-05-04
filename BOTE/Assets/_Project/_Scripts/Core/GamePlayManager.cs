using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : Singleton<GamePlayManager>
{
    private int money=10;
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
        OnMoneyChange?.Invoke(money);
        OnDayChange?.Invoke(day);
        OnHealthChange?.Invoke(health);
    }

    void Update()
    {
        
    }
    public void AddMoney(int money)
    {
        this.money += money;
        OnMoneyChange?.Invoke(this.money);
    }
    public void NextDay()
    {
        if(AchievementManager.Instance == null) return;
        if(!AchievementManager.Instance.IsCompleted("open_house")) return;
        AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("next_day_"+day));
        day++;
        OnDayChange?.Invoke(day);
    }
}
