using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace startup_timer {
	class WorkTime {
		Timer timer;
		DateTime startTime;
		bool wasAfter8hAlert = false;
		NotifyIcon notifyIcon;
		public WorkTime() {
			startTime = GetLastBootUpTime();
			CreateTimer();
			CreateNotifyIcon();
		}

		private void CreateTimer() {
			timer = new Timer {
				Interval = /*60 * */1000
			};
			timer.Tick += new EventHandler(TimerEventProcessor);
			timer.Start();
		}
		private void CreateNotifyIcon() {
			var components = new System.ComponentModel.Container();

			notifyIcon = new NotifyIcon(components) {
				Icon = Properties.Resources.Icon1,
				Text = "",
				Visible = true
			};
			notifyIcon.Click += new EventHandler(ShowWorkTimeAlert);
			UpdateNotifyIcon();
		}

		private DateTime GetLastBootUpTime() {
			//https://stackoverflow.com/a/7407346/4729107
			var query = new SelectQuery(@"SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary='true'");
			var searcher = new ManagementObjectSearcher(query);
			foreach (var mo in searcher.Get()) {
				var timeTxt = mo.Properties["LastBootUpTime"].Value.ToString();
				return ManagementDateTimeConverter.ToDateTime(timeTxt);
			}
			return DateTime.MinValue;
		}

		private void TimerEventProcessor(Object myObject, EventArgs myEventArgs) {
			var diff = DateTime.Now - startTime;
			timer.Enabled = true;

			UpdateNotifyIcon();
			if (diff.Hours >= 8 && !wasAfter8hAlert)
				Show8HAlert();
		}

		private void ShowWorkTimeAlert(Object myObject, EventArgs myEventArgs) {
			var txt = GetWorkTime();
			MessageBox.Show(txt, "Startup-Timer");
		}

		private void UpdateNotifyIcon() {
			notifyIcon.Text = GetWorkTime();
		}
		private void Show8HAlert() {
			wasAfter8hAlert = true;
			MessageBox.Show("You already worked 8 hours", "Startup-Timer");
		}

		private string GetWorkTime() {
			var diff = DateTime.Now - startTime;
			var diffTxt = FormatTimeSpan(diff);
			return string.Format("WorkTime: {0}", diffTxt);
		}
		private string FormatTimeSpan(TimeSpan span) {
			var quaters = span.Minutes / 15f;
			var roundedQuaters = (int)Math.Floor(quaters);
			var missingMinutes = span.Minutes % 15;
			var formattedMissing = string.Format(" ({0})", missingMinutes);
			var hoursTxt = span.Hours;
			var minutesTxt = FormatWithLeadingZero(roundedQuaters * 15);
			return string.Format("{0}:{1}min", hoursTxt, minutesTxt) + (missingMinutes > 0 ? formattedMissing : "");
		}
		private string FormatWithLeadingZero(int value) {
			if (value < 10)
				return "0" + value;
			return "" + value;
		}
	}
}
