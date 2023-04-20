using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using EventType = NPS.Remote.EventType;
using BayatGames.SaveGameFree;

[System.Serializable]
public class RemoteConfigSave : IDataSave
{
    public string Key => key;
    public string key;

    public RemoteConfigSave(string key)
    {
        this.key = key;

        CheckInit();
    }

    [Button]
    public void Save()
    {
        SaveGame.Save(Key, this);
    }

    public bool EnableEvent => Event.Type != EventType.None && EventTime.TotalSeconds > 0;

    public EventData Event
    {
        get
        {
            if (_event == null)
                _event = GetEvent();
            return _event;
        }
    }

    private EventData _event = null;

    [ShowInInspector] public Dictionary<string, string> Configs = new Dictionary<string, string>();

    public void Fix()
    {
        if (Configs == null) Configs = new Dictionary<string, string>();

        CheckInit();
    }

    public void CheckInit()
    {
        if (Configs == null) Configs = new Dictionary<string, string>();

        if (!Configs.ContainsKey(RemoteConfigKey.Season))
            Configs.Add(RemoteConfigKey.Season,
                "11,0.02|5,0.02|1,0.02|10,0.02|0,0.02|3,0.02|4,0.02|2,0.02|9,0.02|6,0.02|7,0.02|8,0.02");
        if (!Configs.ContainsKey(RemoteConfigKey.Multiplier))
            Configs.Add(RemoteConfigKey.Multiplier, "0");
        if (!Configs.ContainsKey(RemoteConfigKey.ElonMusk))
            Configs.Add(RemoteConfigKey.ElonMusk, "a");
        if (!Configs.ContainsKey(RemoteConfigKey.Event))
            Configs.Add(RemoteConfigKey.Event, "Jerwelry,7|Farm,7|Lotto,7|Moon,7");
    }

    public EventType CurrentEvent = EventType.None;
    public int currentEvent = -1;
    public DateTime LastEventTime = default;
    public TimeSpan EventTime => LastEventTime - UnbiasedTime.UtcNow;
    private List<EventData> events = null;

    public List<EventData> Events { get
        {
            if (events == null)
            {
                events = new List<EventData>();

                string[] items = Configs[RemoteConfigKey.Event].Split('|');
                foreach (var item in items)
                {
                    string[] str = item.Split(',');
                    Enum.TryParse(str[0], out EventType ctype);
                    if (ctype != EventType.None)
                    {
                        events.Add(new EventData()
                        {
                            Type = ctype,
                            Day = int.Parse(str[1])
                        });
                    }
                }
            }

            return events;
        } 
    }

    private EventData GetEvent()
    {
        if (CurrentEvent == EventType.None)
            CurrentEvent = Events[0].Type;

        currentEvent = Events.FindIndex(x => x.Type == CurrentEvent);
        if (currentEvent == -1)
        {
            currentEvent = 0;
            CurrentEvent = Events[0].Type;
        }

        return Events[currentEvent];
    }

    public void CheckEvent()
    {
        if (!DataManager.Save.Tutorial.Complete.Contains(101)) return;

        if (LastEventTime == default)
            LastEventTime = UnbiasedTime.UtcNow.AddDays(Event.Day);

        if ((LastEventTime - UnbiasedTime.UtcNow).TotalSeconds < 0)
        {
            if (events == null)
                _event = GetEvent();
            currentEvent++;
            if (currentEvent >= Events.Count)
                currentEvent = 0;

            CurrentEvent = Events[currentEvent].Type;
            _event = GetEvent();

            switch (Event.Type)
            {
                case EventType.Noel:
                    DataManager.Save.User.Save();
                    break;
            }

            LastEventTime = UnbiasedTime.UtcNow.AddDays(Event.Day);
        }

        Save();
    }
}

public class RemoteConfigKey
{
    public const string Season = "ab_season";
    public const string Multiplier = "ab_multiplier";
    public const string Event = "ab_event";
    public const string ElonMusk = "ab_elon_musk";
}