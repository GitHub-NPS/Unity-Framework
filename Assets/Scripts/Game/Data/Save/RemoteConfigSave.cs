using Sirenix.OdinInspector;
using System.Collections.Generic;
using EventType = NPS.Remote.EventType;

[System.Serializable]
public class RemoteConfigSave : ADataSave
{
    public bool NoelEnable => Event.Type == EventType.Noel && Event.IsValid;

    [ShowInInspector] public Dictionary<string, string> Configs = new Dictionary<string, string>();

    public RemoteConfigSave(string key) : base(key)
    {
        Configs.Add(RemoteConfigKey.TypeEvent, "Noel");
        Configs.Add(RemoteConfigKey.TimeEvent, "2023-2-27 23:59");
    }

    private EventData eventData = null;

    public EventData Event
    {
        get
        {
            if (eventData == null)
            {
                eventData = new EventData(Configs[RemoteConfigKey.TypeEvent], Configs[RemoteConfigKey.TimeEvent]);
            }

            return eventData;
        }
    }
}

public class RemoteConfigKey
{
    public const string TypeEvent = "ab_type_event";
    public const string TimeEvent = "ab_time_event";
}