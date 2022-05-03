using System;

namespace StartupTimer.Models {
    public class WorkConfiguration {
        public const string OPTION_NAME = "Work";

        public TimeSpan WorkTime { get; set; } = TimeSpan.FromHours(1);
        public TimeSpan MaxWorkTime { get; set; } = TimeSpan.FromHours(16);
        public TimeSpan? NotifyBeforeEnd { get; set; } = null;
    }
}
