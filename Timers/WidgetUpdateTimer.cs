namespace startup_timer {
    class WidgetUpdateTimer : ITimerHandler {
        private TimeContainer timer;

        public WidgetUpdateTimer(TimeContainer timer) {
            this.timer = timer;
            Update();
        }

        public void Update() {
            timer.Update();
        }
    }
}
