using System;
using System.Collections.Generic;

namespace startup_timer {
    class TimeContainer : IObservable<TimeContainer> {
        public bool IsTimeElapsed { get; private set; }
        public string ShortFormat => GetWorkTimeText();

        DateTime startTime;
        DateTime endTime;
        Formatter dateFormat = new Formatter();
        List<IObserver<TimeContainer>> observers = new List<IObserver<TimeContainer>>();

        public TimeContainer(ITimeGetter getter, int workHours) {
            startTime = getter.GetTime();
            endTime = startTime.AddHours(workHours);
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
            var time = dateFormat.FormatTimeSpan(DateTime.Now - startTime);
            return $"WorkTime: {time}";
        }

        string GetLeftTimeText() {
            var time = dateFormat.FormatTimeSpan(endTime - DateTime.Now);
            return $"Left: {time}";
        }

        string GetNormalEndTimeText() {
            var time = endTime.ToString("HH:mm");
            return $"End time: {time}";
        }

        string GetBeginTimeText() {
            var time = startTime.ToString("HH:mm");
            return $"Start time: {time}";
        }
    }
}
