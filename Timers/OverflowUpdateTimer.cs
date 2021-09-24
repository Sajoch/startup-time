namespace startup_timer {
    class OverflowUpdateTimer : ITimerHandler {
        private TimeContainer timer;

        public OverflowUpdateTimer(TimeContainer timer) {
            this.timer = timer;
        }

        public void Update() {
            timer.CheckOverflow();
        }
    }
}
