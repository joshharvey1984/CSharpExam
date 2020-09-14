using System;

namespace EventsAndDelegates {
    internal class Alarm {
        public event EventHandler OnAlarmRaised = delegate {  };

        public class AlarmEventArgs : EventArgs {
            public string Location { get; set; }
            public AlarmEventArgs(string location) {
                Location = location;
            }
        }

        public void RaiseAlarm(string location) {
            OnAlarmRaised(this, new AlarmEventArgs(location));
        }
    }

    internal static class Program {
        private static void Main(string[] args) {
            var alarm = new Alarm();
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;
            
            alarm.RaiseAlarm("London");
        }

        private static void AlarmListener1(object sender, EventArgs e) {
            Console.WriteLine("Alarm 1 called by " + sender);
        }

        private static void AlarmListener2(object sender, EventArgs e) {
            Console.WriteLine("Alarm 2 called by " + sender);
        }
    }
}