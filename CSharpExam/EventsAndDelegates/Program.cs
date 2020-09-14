using System;
using System.Collections.Generic;
using System.Reflection;

namespace EventsAndDelegates
{
    internal class Alarm
    {
        public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };

        public class AlarmEventArgs : EventArgs
        {
            public string Location { get; }

            public AlarmEventArgs(string location)
            {
                Location = location;
            }
        }

        public void RaiseAlarm(string location)
        {
            var exceptionList = new List<Exception>();
            foreach (var @delegate in OnAlarmRaised.GetInvocationList())
            {
                try
                {
                    @delegate.DynamicInvoke(this, new AlarmEventArgs(location));
                }
                catch (TargetInvocationException e)
                {
                    exceptionList.Add(e.InnerException);
                }
            }

            if (exceptionList.Count > 0)
            {
                throw new AggregateException(exceptionList);
            }

            OnAlarmRaised(this, new AlarmEventArgs(location));
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var alarm = new Alarm();
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            try
            {
                alarm.RaiseAlarm("London");
            }
            catch (AggregateException agg)
            {
                foreach (var exception in agg.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private static void AlarmListener1(object sender, Alarm.AlarmEventArgs e)
        {
            Console.WriteLine("Alarm 1 called by " + e.Location);
            throw new Exception("boom");
        }

        private static void AlarmListener2(object sender, Alarm.AlarmEventArgs e)
        {
            Console.WriteLine("Alarm 2 called by " + e.Location);
        }
    }
}