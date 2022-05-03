using MediatR;
using Microsoft.Extensions.Options;
using StartupTimer.CQRS.Requests;
using StartupTimer.Models;
using StartupTimer.TimeProviders;

namespace StartupTimer.Watchdogs {
    internal class AlmostDoneWorkWatchdogScheme : IWatchdogScheme {
        readonly WorkConfiguration configuration;
        readonly ICurrentTimeProvider currentTimeProvider;

        public AlmostDoneWorkWatchdogScheme(IOptions<WorkConfiguration> options, ICurrentTimeProvider currentTimeProvider) {
            configuration = options.Value;
            this.currentTimeProvider = currentTimeProvider;
        }

        public bool IsTriggered(WorkTime workTime) {
            var notifyBefore = configuration.NotifyBeforeEnd;
            if (!notifyBefore.HasValue)
                return false;

            var currentTime = currentTimeProvider.GetTime();
            if (currentTime > workTime.EndTime)
                return false;

            var span = currentTime - workTime.EndTime;
            return span > notifyBefore.Value;
        }

        public IRequest CreateRequest() {
            return new AlmostDoneAlertRequest();
        }
    }
}
