namespace startup_timer {
	class OverflowUpdateTimer : ITimerHandler {
		private TimerDictionary timers;

		public OverflowUpdateTimer(TimerDictionary timers) {
			this.timers = timers;
		}

		public void Update() {
			foreach (var time in timers.Collection) {
				time.CheckOverflow();
			}
		}
	}
}
