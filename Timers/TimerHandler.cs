using System;
using System.Windows.Forms;

namespace startup_timer {
	class TimerHandler {
		private readonly ITimerHandler handler;
		Timer timer;

		public TimerHandler(int interval, ITimerHandler handler) {
			this.handler = handler;
            handler.Update();
            timer = new Timer {
				Interval = interval
			};
            Console.WriteLine("Start");
			timer.Tick += new EventHandler(TimerEventProcessor);
			timer.Start();
		}

		private void TimerEventProcessor(Object myObject, EventArgs myEventArgs) {
            Console.WriteLine("Tick");
            handler.Update();
			timer.Enabled = true;
		}
	}
}
