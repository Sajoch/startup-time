using System;

namespace StartupTimer.TimeProviders {
    public class CurrentTimeProvider : ICurrentTimeProvider {
        public DateTime GetTime() {
            return DateTime.Now;
        }
    }
}
