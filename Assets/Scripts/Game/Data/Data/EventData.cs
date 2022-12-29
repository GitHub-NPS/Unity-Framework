using System;
using Sirenix.OdinInspector;
using UnityEngine;
using EventType = NPS.Remote.EventType;

[System.Serializable]
public class EventData
{
    public bool IsValid => Type != EventType.None && Time.TotalSeconds > 0;

    [ShowInInspector] public EventType Type = EventType.Noel;
    [ShowInInspector] public TimeSpan Time => lastTime - UnbiasedTime.UtcNow;
    [ShowInInspector] public DateTime lastTime;

    public EventData()
    {
    }

    public EventData(string type, string time)
    {
        DateTime.TryParse(time, out lastTime);
        Enum.TryParse(type, out EventType ctype);
        if (ctype != EventType.None) Type = ctype;
    }
}