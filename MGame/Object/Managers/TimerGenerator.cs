using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace MGame
{
    public enum TimerType {TT_50=50, TT_100=100};
    public class TimerGenerator
    {
        private SortedList<TimerType, Timer> _timers;
        public SortedList<TimerType, List<EventHandler>> Events;

        private static TimerGenerator s_instance;

        public static TimerGenerator Instance
        {
            get 
            {
                if(s_instance == null)
                    s_instance = new TimerGenerator();
                return s_instance; 
            }
        }

        public static void AddTimerEventHandler(TimerType Type,EventHandler Event)
        {
            Timer temp = GetTimer(Type);
            if (!Instance.Events.ContainsKey(Type))
                Instance.Events.Add(Type, new List<EventHandler>());
            if (Instance.Events[Type].IndexOf(Event) != -1)
                return;
            Instance.Events[Type].Add(Event);
        }

        public static void RemoveAllTimerEvent()
        {
            foreach(var timer in Instance._timers)
            {
                timer.Value.Stop(); 
                timer.Value.Dispose(); 
            }
            Instance._timers.Clear();
            Instance.Events.Clear();
        }

        private void TT_50_Tick (object sender, EventArgs E)
        {
            //List<EventHandler> EventList = Events[TimerType.TT_50];
            foreach(EventHandler Event in Events[TimerType.TT_50])
                Event(sender, E);
        }
        private void TT_100_Tick(object sender, EventArgs E)
        {
            //List<EventHandler> EventList = Events[TimerType.TT_50];
            foreach (EventHandler Event in Events[TimerType.TT_100])
                Event(sender, E);
        }
        private static Timer GetTimer(TimerType value)
        {
            if (!Instance._timers.ContainsKey(value))
            {
                Instance._timers[value] = new Timer();
                Instance._timers[value].Interval = (int)value;
                switch(value)
                {
                    case TimerType.TT_50:
                        Instance._timers[value].Tick += new EventHandler(s_instance.TT_50_Tick);break;
                    case TimerType.TT_100:
                        Instance._timers[value].Tick += new EventHandler(s_instance.TT_100_Tick); break;

                }
                Instance._timers[value].Enabled = true;
            }
            return s_instance._timers[value];
        }
        public TimerGenerator()
        {
            _timers = new SortedList<TimerType, Timer>();
            Events = new SortedList<TimerType,List<EventHandler>>();
        }
    }
}
