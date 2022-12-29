using System;
using System.Collections.Generic;

[System.Serializable]
public class TimeSave : ADataSave
{
    public TimeSave(string key) : base(key)
    {

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

    public void SetLastTimeOut()
    {
        LastTimeOut = UnbiasedTime.S ? UnbiasedTime.UtcNow : DateTime.Now;
    }
}
