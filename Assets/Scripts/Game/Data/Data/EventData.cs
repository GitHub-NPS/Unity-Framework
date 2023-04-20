using System;
using UnityEngine;
using EventType = NPS.Remote.EventType;

[System.Serializable]
public class EventData
{
    public EventType Type = EventType.None;
    public int Day = 7;
}