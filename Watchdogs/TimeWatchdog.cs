using MediatR;
using StartupTimer.Models;
using StartupTimer.Timers;

namespace StartupTimer.Watchdogs {
    internal class TimeWatchdog : ITimerHandler {
        readonly IMediator mediator;
        readonly IWatchdogScheme scheme;
        readonly WorkTimeProvider workTimeProvider;
        bool isAlreadyTriggered;
        WorkTime lastWorkTime;

        public TimeWatchdog(IMediator mediator, WorkTimeProvider workTimeProvider, IWatchdogScheme scheme) {
            this.mediator = mediator;
            this.workTimeProvider = workTimeProvider;
            this.scheme = scheme;
        }

        public void Update() {
            var currentWorkTime = workTimeProvider.GetWorkTime();
            if (lastWorkTime != currentWorkTime) {
                lastWorkTime = currentWorkTime;
                isAlreadyTriggered = false;
            }

            if (scheme.IsTriggered(currentWorkTime) && !isAlreadyTriggered) {
                mediator.Send(scheme.CreateRequest());
                isAlreadyTriggered = true;
            }
        }
    }
}
