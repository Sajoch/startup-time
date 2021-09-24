using System;
using System.Windows.Forms;

namespace startup_timer {
    class WorkTime : IObserver<TimeContainer> {
        private readonly TimeContainer container;
        private readonly NotifyWidget widget;

        public WorkTime(TimeContainer container, NotifyWidget widget) {
            this.container = container;
            this.widget = widget;
            container.Subscribe(this);
            OnNext(container);
            widget.SetClickListener(ShowInfo);
        }

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(TimeContainer value) {
            widget.SetOverworkBadge(value.IsTimeElapsed);
            widget.SetInfo(value.ShortFormat);
        }

        void ShowInfo() {
            MessageBox.Show(container.GetInfo(), "Startup-Timer");
        }
    }
}
