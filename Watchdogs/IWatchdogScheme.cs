using MediatR;
using StartupTimer.Models;

namespace StartupTimer.Watchdogs {
    internal interface IWatchdogScheme {
        bool IsTriggered(WorkTime workTime);

        IRequest CreateRequest();
    }
}
