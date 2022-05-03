using MediatR;
using StartupTimer.Models;

namespace StartupTimer.Watchdogs {
    internal class TimeWatchdogFactory {
        readonly IMediator mediator;
        readonly WorkTimeProvider workTimeProvider;

        public TimeWatchdogFactory(IMediator mediator, WorkTimeProvider workTimeProvider) {
            this.mediator = mediator;
            this.workTimeProvider = workTimeProvider;
        }

        public TimeWatchdog Create(IWatchdogScheme scheme) {
            return new TimeWatchdog(mediator, workTimeProvider, scheme);
        }
    }
}
