using System;
using System.Collections.Generic;
using NPS.Pattern.Observer;

[System.Serializable]
public class GeneralSave : ADataSave
{
    public GeneralSave(string key) : base(key)
    {

    }

    public float Sound = 1.0f;
    public float Music = 1.0f;

    public bool Ads = true;

    public int CountInterAds = 0;
    public int CountRewardAds = 0;

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
}
