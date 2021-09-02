using System;
using System.Management;
using System.Security.Principal;

namespace startup_timer {
    class LogonTimeGetter : ITimeGetter {
        public DateTime GetTime() {
            var userSid = GetUserSid();
            var today = GetTodayTime();
            var query = new SelectQuery($"SELECT * FROM Win32_NTLogEvent WHERE Logfile='System' AND SourceName='Microsoft-Windows-Winlogon' AND EventCode='7001' AND TimeWritten > '{today}'");
            var searcher = new ManagementObjectSearcher(query);

            var result = DateTime.MaxValue;
            foreach (var mo in searcher.Get()) {
                var time = GetTime(mo, userSid);
                if (time < result)
                    result = time;
            }
            return result;
        }

        string GetUserSid() {
            var user = WindowsIdentity.GetCurrent().User;
            return user.Value.ToString();
        }

        string GetTodayTime() {
            var date = DateTime.UtcNow;
            return date.ToString("yyyyMMdd000000.000000-000");
        }

        DateTime GetTime(ManagementBaseObject management, string sid) {
            var data = management.Properties["InsertionStrings"];
            var time = management.Properties["TimeWritten"];
            if (data.Value is string[] dataList) {
                if (dataList[1] != sid)
                    return DateTime.MaxValue;
            } else {
                return DateTime.MaxValue;
            }
            if (time.Value is string timeTxt) {
                return ManagementDateTimeConverter.ToDateTime(timeTxt);
            } else {
                return DateTime.MaxValue;
            }
        }
    }
}
