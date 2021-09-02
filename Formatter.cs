using System;

namespace startup_timer {
	class Formatter {
		public string FormatTimeSpan(TimeSpan span) {
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
