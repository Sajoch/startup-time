using System;
using System.Collections.Generic;
using System.Resources;

namespace startup_timer {
    class TimeContainer : IObservable<TimeContainer> {
        public bool IsTimeElapsed { get; private set; }
        public string ShortFormat => GetWorkTimeText();
        public string ShortWorkTime => dateFormat.FormatTimeSpan(DateTime.Now - startTime);

        private readonly DateTime startTime;
        private readonly DateTime endTime;
        private readonly Formatter dateFormat = new Formatter();
        private readonly List<IObserver<TimeContainer>> observers = new List<IObserver<TimeContainer>>();
        private readonly ResourceManager resource = new ResourceManager(typeof(Resources.Resource));


        public TimeContainer(ITimeGetter getter) {
            var dailyWorkHours = resource.GetString("dailyTimeSpan") ?? "08:00:00";
            var workHours = TimeSpan.Parse(dailyWorkHours);
            startTime = getter.GetTime();
            endTime = startTime + workHours;
            Update();
        }

        public void Update() {
            if (DateTime.Now >= endTime && !IsTimeElapsed) {
                IsTimeElapsed = true;
            }
            Notify();
        }

        public void CheckOverflow() {
            //if (diff.Hours >= 8 && !IsTimeElapsed) {
            //    IsTimeElapsed = true;
            //    Notify();
            //}
            Notify();
        }

        public string GetInfo() {
            var elements = new List<string>() {
                GetWorkTimeText(),
                GetLeftTimeText(),
                GetBeginTimeText(),
                GetNormalEndTimeText(),
            };

            return string.Join("\n", elements);
        }

        public IDisposable Subscribe(IObserver<TimeContainer> observer) {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<TimeContainer>(observers, observer);
        }

        void Notify() {
            observers.ForEach(a => a.OnNext(this));
        }

        string GetWorkTimeText() {
            var format = resource.GetString("workTime");
            return string.Format(format, ShortWorkTime);
        }

        string GetLeftTimeText() {
            var time = dateFormat.FormatTimeSpan(endTime - DateTime.Now);
            var format = resource.GetString("leftTime");
            return string.Format(format, time);
        }

        string GetNormalEndTimeText() {
            var time = FormatTime(endTime);
            var format = resource.GetString("endTime");
            return string.Format(format, time);
        }

        string GetBeginTimeText() {
            var time = FormatTime(startTime);
            var format = resource.GetString("startTime");
            return string.Format(format, time);
        }

        string FormatTime(DateTime time) {
            var timeFormat = resource.GetString("timeFormat") ?? "HH:mm";
            return time.ToString(timeFormat);
        }
    }
}
