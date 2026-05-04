using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public event Action<string> OnAchievementUnlocked;

    [SerializeField] private List<AchievementSO> allAchievements; // Kéo tất cả SO vào đây
    
    private Dictionary<string, AchievementSO> _achievementDatabase = new Dictionary<string, AchievementSO>();
    private HashSet<string> _completedAchievements = new HashSet<string>();
    private const string SaveKey = "UserAchievements";

    public void Awake()
    {
        AchievementSO[] achievements = Resources.LoadAll<AchievementSO>("Achievement/");
        for(int i=0; i<achievements.Length; i++)
        {
            allAchievements.Add(achievements[i]);
        }
        foreach (var ach in allAchievements)
        {
            _achievementDatabase[ach.achievementID] = ach;
        }
    }
    public bool IsCompleted(string id) => _completedAchievements.Contains(id);

    public void TriggerAchievement(string id)
    {
        // Nếu đã hoàn thành rồi thì return ngay, cực nhẹ
        if (_completedAchievements.Contains(id)) return;
        if (!_achievementDatabase.ContainsKey(id)) return;

        AchievementSO currentAch = _achievementDatabase[id];

        // KIỂM TRA ĐIỀU KIỆN TIÊN QUYẾT
        if (currentAch.prerequisite != null)
        {
            if (!_completedAchievements.Contains(currentAch.prerequisite.achievementID))
            {
                Debug.Log($"[Locked] Cần hoàn thành {currentAch.prerequisite.achievementID} trước!");
                return; 
            }
        }

        // Nếu thỏa mãn hoặc không có prerequisite
        Unlock(id);

    }
    public void TriggerAchievement(AchievementSO currentAch)
    {
        // Nếu đã hoàn thành rồi thì return ngay, cực nhẹ
        if(currentAch == null) return;
        if (_completedAchievements.Contains(currentAch.achievementID)) return;
        if (!_achievementDatabase.ContainsKey(currentAch.achievementID)) return;


        if (currentAch.prerequisite != null)
        {
            if (!_completedAchievements.Contains(currentAch.prerequisite.achievementID))
            {
                Debug.Log($"[Locked] Cần hoàn thành {currentAch.prerequisite.achievementID} trước!");
                return; 
            }
        }

        // Nếu thỏa mãn hoặc không có prerequisite
        Unlock(currentAch.achievementID);

    }
    private void Unlock(string id)
    {
        _completedAchievements.Add(id);
        Debug.Log($"[Achievement]: {id}");
        OnAchievementUnlocked?.Invoke(id);
        //CheckChainReaction(id);
        //SaveData();
        
    }
    private void CheckChainReaction(string id)
    {
        foreach (var ach in allAchievements)
        {
            if (ach.prerequisite != null && ach.prerequisite.achievementID ==id)
            {
                TriggerAchievement(ach.achievementID);
            }
        }
    }
    public AchievementSO GetAchievement(string id)
    {
        if (_achievementDatabase.ContainsKey(id))
        {
            AchievementSO ach = _achievementDatabase[id];
            return ach;
        }
        else
        {
            Debug.LogWarning($"[Achievement]: {id} không tồn tại");
            return null;
        }
    }
    private void SaveData()
    {
        // Lưu danh sách id cách nhau bởi dấu phẩy
        string data = string.Join(",", _completedAchievements);
        PlayerPrefs.SetString(SaveKey, data);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        string savedData = PlayerPrefs.GetString(SaveKey, "");
        if (string.IsNullOrEmpty(savedData)) return;

        string[] ids = savedData.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            _completedAchievements.Add(ids[i]);
        }
    }
}