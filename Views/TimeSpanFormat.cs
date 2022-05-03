using System;

namespace StartupTimer.Views {
    public class TimeSpanFormat {
        readonly string format;

        public TimeSpanFormat(string format) {
            this.format = format;
        }

        public string Format(TimeSpan span, bool discardMissing = false) {
            if (span.TotalSeconds <= 0)
                return "0";

            var hours = span.Hours;
            var minutes = span.Minutes;
            var quarters = minutes / 15f;
            var missingMinutes = minutes % 15;
            var roundedQuarters = (int)Math.Floor(quarters);
            if (discardMissing)
                missingMinutes = 0;

            var roundedMinutes = FormatWithLeadingZero(roundedQuarters * 15);
            return string.Format(format, hours, roundedMinutes, FormatMissing(missingMinutes));
        }

        static string FormatWithLeadingZero(int value) {
            if (value < 10)
                return $"0{value}";

            return $"{value}";
        }

        static string FormatMissing(int value) {
            if (value <= 0)
                return string.Empty;

            return $" ({value})";
        }
    }
}
