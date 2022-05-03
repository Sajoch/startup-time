using System;
using System.Windows.Forms;

namespace StartupTimer.Timers {
    internal class TimerHandler : IDisposable {
        readonly ITimerHandler handler;
        readonly Timer timer;

        public TimerHandler(TimeSpan interval, ITimerHandler handler) {
            this.handler = handler;
            timer = new Timer {
                Interval = (int)interval.TotalMilliseconds
            };
            timer.Tick += TimerOnTick;
            timer.Start();
            handler?.Update();
        }

        public void Dispose() {
            timer.Enabled = false;
            timer.Dispose();
        }

        void TimerOnTick(object sender, EventArgs e) {
            handler?.Update();
            timer.Enabled = true;
        }
    }
}
