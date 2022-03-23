using System;
using System.Resources;
using System.Windows.Forms;

namespace startup_timer {
    class WorkTime : IObserver<TimeContainer> {
        private readonly TimeContainer container;
        private readonly NotifyWidget widget;
        private readonly ResourceManager resource = new ResourceManager(typeof(Resources.Resource));
        private bool isAlertShown = false;

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

            if (!isAlertShown && value.IsTimeElapsed) {
                ShowOverworkAlert();
                isAlertShown = true;
            } else if (isAlertShown && !value.IsTimeElapsed)
                isAlertShown = false;
        }

        void ShowInfo() {
            var caption = resource.GetString("appName");
            MessageBox.Show(container.GetInfo(), caption);
        }

        void ShowOverworkAlert() {
            var caption = resource.GetString("appName");
            var format = resource.GetString("overworkAlert");
            var message = string.Format(format, container.ShortWorkTime);
            MessageBox.Show(message, caption);
        }
    }
}
