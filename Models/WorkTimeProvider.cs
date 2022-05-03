using Microsoft.Extensions.Options;
using StartupTimer.TimeProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupTimer.Models {
    public class WorkTimeProvider {
        readonly WorkConfiguration configuration;
        readonly ICurrentTimeProvider currentTimeProvider;
        readonly List<ITimeProvider> timeProviders;

        WorkTime lastWorkTime;

        public WorkTimeProvider(IOptions<WorkConfiguration> configuration, IEnumerable<ITimeProvider> timeProviders, ICurrentTimeProvider currentTimeProvider) {
            this.currentTimeProvider = currentTimeProvider;
            this.timeProviders = timeProviders.ToList();
            this.configuration = configuration.Value;
        }

        public WorkTime GetWorkTime() {
            var now = currentTimeProvider.GetTime();
            if (lastWorkTime == null || lastWorkTime.ExpiresAfter < now) {
                var beginTime = GetBeginTime(now);
                lastWorkTime = new WorkTime(beginTime, configuration.WorkTime, configuration.MaxWorkTime);
            }

            return lastWorkTime;
        }

        DateTime GetBeginTime(DateTime sameDay) {
            var entries = timeProviders.Select(a => a.GetTime())
                .Where(a => a.Date == sameDay.Date)
                .ToList();
            return entries.Any() ? entries.Min() : DateTime.MinValue;
        }
    }
}
