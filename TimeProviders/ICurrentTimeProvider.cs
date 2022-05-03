using System;

namespace StartupTimer.TimeProviders {
    public interface ICurrentTimeProvider {
        DateTime GetTime();
    }
}
