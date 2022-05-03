using StartupTimer.Timers;
using StartupTimer.Views;
using StartupTimer.Watchdogs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StartupTimer {
    internal class Startup {
        readonly ListTimerHandlers handlers = new ListTimerHandlers();
        readonly NotifyWidget notifyWidget;
        readonly TimerHandler updateTimer;

        public Startup(NotifyWidget notifyWidget, TimeWatchdogFactory timeWatchdogFactory, IEnumerable<IWatchdogScheme> watchdogSchemes) {
            this.notifyWidget = notifyWidget;
            updateTimer = new TimerHandler(TimeSpan.FromSeconds(30), handlers);

            handlers.Add(notifyWidget);
            foreach (var scheme in watchdogSchemes) {
                var watchdog = timeWatchdogFactory.Create(scheme);
                handlers.Add(watchdog);
            }
        }

        public void Run() {
            Application.Run();
            Cleanup();
        }

        void Cleanup() {
            notifyWidget.Dispose();
            updateTimer.Dispose();
        }
    }
}
