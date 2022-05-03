using System;
using System.Management;
using System.Security.Principal;

namespace StartupTimer.TimeProviders {
    internal class LoginTimeProvider : ITimeProvider {
        const string TIME_FORMAT = "yyyyMMdd000000.000000-000";

        public DateTime GetTime() {
            var query = CreateQuery();
            var searcher = new ManagementObjectSearcher(query);
            return FindResult(searcher);
        }

        SelectQuery CreateQuery() {
            var today = GetTodayTime();
            return new SelectQuery(
                $"SELECT * FROM Win32_NTLogEvent WHERE Logfile='System' AND SourceName='Microsoft-Windows-Winlogon' AND EventCode='7001' AND TimeWritten > '{today}'");
        }

        DateTime FindResult(ManagementObjectSearcher searcher) {
            var userSid = GetUserSid();
            var result = DateTime.MaxValue;
            foreach (var mo in searcher.Get()) {
                var time = GetTime(mo, userSid);
                if (time < result)
                    result = time;
            }

            return result;
        }

        static string GetUserSid() {
            var user = WindowsIdentity.GetCurrent().User;
            return user?.Value;
        }

        static string GetTodayTime() {
            var date = DateTime.UtcNow;
            return date.ToString(TIME_FORMAT);
        }

        static DateTime GetTime(ManagementBaseObject management, string sid) {
            var data = management.Properties["InsertionStrings"];
            var time = management.Properties["TimeWritten"];

            if (data.Value is string[] dataList) {
                if (dataList[1] != sid)
                    return DateTime.MaxValue;
            } else {
                return DateTime.MaxValue;
            }

            if (time.Value is string timeTxt)
                return ManagementDateTimeConverter.ToDateTime(timeTxt);

            return DateTime.MaxValue;
        }
    }
}
