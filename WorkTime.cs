using System.Linq;
using System.Windows.Forms;

namespace startup_timer {
	class WorkTime {
		TimerDictionary timers = new TimerDictionary();
		TimerHandler widgetTimer;
		TimerHandler overflowTimer;

		public WorkTime() {
			var widget = new NotifyWidget();
			timers.Add("logon", new TimeContainer(new LogonTimeGetter()));
			widgetTimer = new TimerHandler(60000, new WidgetUpdateTimer(widget, timers));
			overflowTimer = new TimerHandler(60 * 60000, new OverflowUpdateTimer(timers));

            widget.SetClickListener(() => {
                var txt = string.Join("\n\n\n", timers.Collection.Select(a => $"{a.GetInfo()}"));
                MessageBox.Show(txt, "Startup-Timer");
            });
        }
	}
}
