using System.Collections.Generic;

namespace startup_timer {
    class TimerDictionary {
        Dictionary<string, TimeContainer> items = new Dictionary<string, TimeContainer>();

        public IEnumerable<TimeContainer> Collection => items.Values;

        public void Add(string name, TimeContainer timer) {
            items.Add(name, timer);
        }

        public TimeContainer Get(string name) {
            return items[name];
        }
    }
}
