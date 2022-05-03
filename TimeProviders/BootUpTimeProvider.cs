using System;
using System.Management;

namespace StartupTimer.TimeProviders {
    internal class BootUpTimeProvider : ITimeProvider {
        public DateTime GetTime() {
            //https://stackoverflow.com/a/7407346/4729107
            var query = new SelectQuery(@"SELECT LastBootUpTime FROM Win32_OperatingSystem WHERE Primary='true'");
            var searcher = new ManagementObjectSearcher(query);
            foreach (var mo in searcher.Get()) {
                var timeTxt = mo.Properties["LastBootUpTime"].Value.ToString();
                return ManagementDateTimeConverter.ToDateTime(timeTxt);
            }

            return DateTime.MinValue;
        }
    }
}
