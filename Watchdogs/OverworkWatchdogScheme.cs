using MediatR;
using StartupTimer.CQRS.Requests;
using StartupTimer.Models;
using StartupTimer.TimeProviders;

namespace StartupTimer.Watchdogs {
    internal class OverworkWatchdogScheme : IWatchdogScheme {
        readonly ICurrentTimeProvider currentTimeProvider;

        public OverworkWatchdogScheme(ICurrentTimeProvider currentTimeProvider) {
            this.currentTimeProvider = currentTimeProvider;
        }

        public bool IsTriggered(WorkTime workTime) {
            return workTime.IsOverwork(currentTimeProvider.GetTime());
        }

        public IRequest CreateRequest() {
            return new OverworkAlertRequest();
        }
    }
}
