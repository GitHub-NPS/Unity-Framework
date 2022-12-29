using System;
using System.Linq;
using System.Collections.Generic;
using com.unimob.mec;
using com.unimob.pattern.singleton;

namespace com.unimob.timer
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        private Dictionary<TimerKey, TimerHandle> handle = new Dictionary<TimerKey, TimerHandle>();

        public void RegisterTick(TickData data)
        {
            RemoveTick(data);

            TimerKey key;
            if (!FindKey(data, out key))
            {
                handle.Add(key, new TimerHandle());
                RunTick(key);
            }

            handle[key].Data.Add(data);

            data.IsRuning = true;
            data.Action?.Invoke();
        }

        private bool FindKey(TickData data, out TimerKey key)
        {
            key = handle.Keys.ToList().Find(x => x.Same(data));
            if (key == null)
            {
                key = new TimerKey(data);
                return false;
            }
            return true;
        }

        public void RemoveTick(TickData data)
        {
            TimerKey key;
            if (FindKey(data, out key))
            {
                if (handle[key].Data.Contains(data))
                {
                    handle[key].Data.Remove(data);
                    if (handle[key].Data.Count == 0)
                    {
                        StopTick(key);
                        handle.Remove(key);
                    }
                    data.IsRuning = false;
                }
            }
        }

        public bool IsValid(TickData data)
        {
            TimerKey key;
            if (FindKey(data, out key))
                return data != null && data.Action != null && data.IsRuning && handle[key].Data.Contains(data);
            return false;
        }

        public void Clear()
        {
            handle.Clear();
        }

        private void RunTick(TimerKey key)
        {
            handle[key].Handle = Timing.RunCoroutine(_Tick(key), type2Segment[key.Type]);
        }

        private void StopTick(TimerKey key)
        {
            Timing.KillCoroutines(handle[key].Handle);
        }

        private IEnumerator<float> _Tick(TimerKey key)
        {
            while (true)
            {
                if (key.Delay == 0)
                    yield return Timing.WaitForOneFrame;
                else
                    yield return Timing.WaitForSeconds(key.Delay);

                foreach (var data in handle[key].Data.ToList())
                {
                    if (data.IsRuning)
                        data.Action?.Invoke();
                }
            }
        }

        private Dictionary<TimerType, Segment> type2Segment = new Dictionary<TimerType, Segment>()
        {
            { TimerType.Update, Segment.Update },
            { TimerType.FixedUpdate, Segment.FixedUpdate },
            { TimerType.LateUpdate, Segment.LateUpdate },
            { TimerType.RealtimeUpdate, Segment.RealtimeUpdate },
            { TimerType.SlowUpdate, Segment.SlowUpdate },
        };
    }

    public static class TimerExtension
    {
        public static void RegisterTick(this TickData data)
        {
            TimerManager.S?.RegisterTick(data);
        }

        public static void RemoveTick(this TickData data)
        {
            TimerManager.S?.RemoveTick(data);
        }

        public static bool IsValid(this TickData data)
        {
            if (TimerManager.S) return TimerManager.S.IsValid(data);
            return false;
        }
    }

    public class TickData
    {
        public Action Action = null;
        public TimerType Type = TimerType.Update;
        public float Delay = 0f;
        public bool IsRuning = false;

        public TickData(TimerType Type = TimerType.Update, float Delay = 0f)
        {
            this.Type = Type;
            this.Delay = Delay;
        }
    }

    public class TimerKey
    {
        public TimerType Type = TimerType.Update;
        public float Delay = 0f;

        public TimerKey(TickData data)
        {
            this.Type = data.Type;
            this.Delay = data.Delay;
        }

        public bool Same(TickData data)
        {
            return this.Type == data.Type && this.Delay == data.Delay;
        }
    }

    public enum TimerType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        RealtimeUpdate,
        SlowUpdate,
    }

    public class TimerHandle
    {
        public CoroutineHandle Handle;
        public List<TickData> Data = new List<TickData>();
    }
}