namespace startup_timer
{
	class WidgetUpdateTimer : ITimerHandler {
		private TimerDictionary timers;
		private NotifyWidget notifyIcon;

		public WidgetUpdateTimer(NotifyWidget notifyIcon, TimerDictionary timers) {
			this.notifyIcon = notifyIcon;
			this.timers = timers;
            Update();

        }

		public void Update() {
			foreach (var time in timers.Collection) {
				time.Update();
			}
            //notifyIcon.SetClickListener();
            var timer = timers.Get("logon");
            notifyIcon.SetInfo(timer.ShortFormat);

        }
	}
}
