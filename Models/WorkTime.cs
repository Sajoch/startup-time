using System;

namespace StartupTimer.Models {
    public class WorkTime {
        public WorkTime(DateTime beginTime, TimeSpan workTime, TimeSpan maxWorkTime) {
            BeginTime = beginTime;
            RequiredTime = workTime;
            ExpiresAfter = beginTime.Add(maxWorkTime);
            EndTime = beginTime.Add(workTime);
        }

        public DateTime EndTime { get; }
        public DateTime BeginTime { get; }
        public TimeSpan RequiredTime { get; }
        public DateTime ExpiresAfter { get; }

        public bool IsOverwork(DateTime time) {
            return time > EndTime;
        }
    }
}
