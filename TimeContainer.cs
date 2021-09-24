using System;
using System.Collections.Generic;

namespace startup_timer {
    class TimeContainer : IObservable<TimeContainer> {
        public bool IsTimeElapsed { get; private set; }
        public string ShortFormat => $"WorkTime: {dateFormat.FormatTimeSpan(DateTime.Now - startTime)}";

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
            var diff = DateTime.Now - startTime;
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
            var leftTime = dateFormat.FormatTimeSpan(endTime - DateTime.Now);
            var doneDayAt = endTime.ToString("HH:mm");
            return $"{ShortFormat}\nLeft: {leftTime}\n8 hours: {doneDayAt}";
        }

        public IDisposable Subscribe(IObserver<TimeContainer> observer) {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<TimeContainer>(observers, observer);
        }

        void Notify() {
            observers.ForEach(a => a.OnNext(this));
        }
    }
}
