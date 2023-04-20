using System;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using NPS.Pattern.Observer;
using Sirenix.OdinInspector;

[System.Serializable]
public class GeneralSave : IDataSave
{
    public string Key => key;
    public string key;

    public GeneralSave(string key)
    {
        this.key = key;
    }

    [Button]
    public void Save()
    {
        SaveGame.Save(Key, this);
    }

    public bool IsShowRewardAds => Reward;
    public bool IsShowInterAds => Ads && (UnbiasedTime.UtcNow - LastTimeShowRewardAds).TotalSeconds >= 60;

    public void Fix()
    {
        CheckInTime = DateTime.UtcNow;
    }

    public float Sound = 1.0f;
    public float Music = 1.0f;

    public bool Ads = true;
    public DateTime LastTimeShowRewardAds = default;

    public int CountInterAds = 0;
    public int CountRewardAds = 0;
    public bool Reward = true;

    public void SetSound(float value)
    {
        Sound = value;
    }

    public void SetMusic(float value)
    {
        Music = value;
    }

    public void SetAds(bool value)
    {
        Ads = value;

        Observer.S?.PostEvent(EventID.ChangeAds, value);
    }

    public void SetReward(bool value)
    {
        Reward = value;
    }

    public void ShowRewardAds()
    {
        LastTimeShowRewardAds = UnbiasedTime.UtcNow;
    }

    public DateTime CheckInTime = DateTime.UtcNow;
    public TimeSpan TimeIn
    {
        get
        {
            if ((UnbiasedTime.UtcNow - CheckInTime).TotalMinutes <= 0)
            {
                return new TimeSpan(0, 0, 0, 0);
            }
            return UnbiasedTime.UtcNow - CheckInTime;
        }
    }
}
