using System;
using System.Windows.Forms;

namespace startup_timer {
    class NotifyWidget {
        readonly NotifyIcon widget;
        Action onClickFunc = null;

        public NotifyWidget() {
            var components = new System.ComponentModel.Container();

            widget = new NotifyIcon(components) {
                Icon = Properties.Resources.Icon1,
                Text = "",
                Visible = true
            };
            widget.Click += new EventHandler(OnClick);
        }

        public void SetOverworkBadge(bool value) {
            widget.Icon = value ? Properties.Resources.Icon2 : Properties.Resources.Icon1;
        }

        public void SetInfo(string info) {
            widget.Text = info;
        }

        public void SetClickListener(Action func) {
            onClickFunc = func;
        }

        private void OnClick(object sender, EventArgs e) {
            onClickFunc?.Invoke();
        }
    }
}
