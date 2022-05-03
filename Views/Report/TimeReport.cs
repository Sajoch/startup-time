using StartupTimer.Models;
using StartupTimer.Resources;
using StartupTimer.TimeProviders;
using System;
using System.Resources;

namespace StartupTimer.Views.Report {
    public class TimeReport {
        readonly ICurrentTimeProvider currentTimeProvider;
        readonly ResourceManager resource = new ResourceManager(typeof(LocalizationResource));
        readonly TimeSpanFormat timeSpanFormat;

        public TimeReport(ICurrentTimeProvider currentTimeProvider) {
            this.currentTimeProvider = currentTimeProvider;
            var format = GetText("TimeSpan.Format");
            timeSpanFormat = new TimeSpanFormat(format);
        }

        public string GetWorkTimeText(WorkTime workTime) {
            var time = GetWorkTime(workTime);
            var format = GetText("WorkTime.Format");
            return string.Format(format, time);
        }

        public string GetLeftTimeText(WorkTime workTime) {
            var time = GetLeftTime(workTime);
            var format = GetText("LeftTime.Format");
            return string.Format(format, time);
        }

        public string GetNormalEndTimeText(WorkTime workTime) {
            var time = FormatTime(workTime.EndTime);
            var format = GetText("EndTime.Format");
            return string.Format(format, time);
        }

        public string GetBeginTimeText(WorkTime workTime) {
            var time = FormatTime(workTime.BeginTime);
            var format = GetText("StartTime.Format");
            return string.Format(format, time);
        }

        public string GetOverTimeText(WorkTime workTime) {
            var time = GetOverTime(workTime);
            var format = GetText("OverTime.Format");
            return string.Format(format, time);
        }

        public string GetWorkTime(WorkTime workTime) {
            var span = currentTimeProvider.GetTime() - workTime.BeginTime;
            return timeSpanFormat.Format(span);
        }

        public string GetLeftTime(WorkTime workTime) {
            var span = workTime.EndTime - currentTimeProvider.GetTime();
            return timeSpanFormat.Format(span);
        }

        public string GetOverTime(WorkTime workTime) {
            var span = currentTimeProvider.GetTime() - workTime.EndTime;
            return timeSpanFormat.Format(span);
        }

        string FormatTime(DateTime time) {
            var timeFormat = GetText("TimeFormat");
            return time.ToString(timeFormat);
        }

        string GetText(string key) {
            return resource.GetString(key) ?? key;
        }
    }
}
