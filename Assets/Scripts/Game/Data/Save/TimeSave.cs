using BayatGames.SaveGameFree;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

[System.Serializable]
public class TimeSave : IDataSave
{
    public string Key => key;
    public string key;

    public TimeSave(string key)
    {
        this.key = key;
    }

    [Button]
    public void Save()
    {
        SaveGame.Save(Key, this);
    }

    public DateTime LastTimeOut = DateTime.UtcNow;

    public TimeSpan TimeOut
    {
        get
        {
            if ((UnbiasedTime.UtcNow - LastTimeOut).TotalMinutes <= 0)
            {
                return new TimeSpan(0, 0, 0, 0);
            }
            return UnbiasedTime.UtcNow - LastTimeOut;
        }
    }

    public void Fix()
    {

    }

    public void SetLastTimeOut()
    {
        LastTimeOut = UnbiasedTime.S ? UnbiasedTime.UtcNow : DateTime.Now;
    }
}
