using System;

namespace startup_timer {
	class TimeContainer {
		public bool IsTimeElapsed { get; private set; }
		public string ShortFormat { get; private set; }

		DateTime startTime;
		DateTime endTime;
		Formatter dateFormat = new Formatter();

		public TimeContainer(ITimeGetter getter) {
			startTime = getter.GetTime();
			endTime = startTime.AddHours(8);
		}

		public void Update() {
			var diff = DateTime.Now - startTime;
			ShortFormat = $"WorkTime: {dateFormat.FormatTimeSpan(diff)}";
			if (diff.Hours >= 8 && !IsTimeElapsed)
				IsTimeElapsed = true;
		}

		public void CheckOverflow() {

		}

		public string GetInfo() {
			var doneDayAt = endTime.ToString("HH:mm");
			return $"{ShortFormat}\n8 hours: {doneDayAt}";
		}

	}
}
