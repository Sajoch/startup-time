using System;

namespace StartupTimer.TimeProviders {
    public interface ITimeProvider {
        DateTime GetTime();
    }
}
