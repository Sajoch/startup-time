using System.Collections.Generic;

namespace StartupTimer.Timers {
    internal class ListTimerHandlers : ITimerHandler {
        readonly List<ITimerHandler> collection = new List<ITimerHandler>();

        public void Update() {
            foreach (var handler in collection)
                handler.Update();
        }

        public void Add(ITimerHandler handler) {
            collection.Add(handler);
        }

        public void Remove(ITimerHandler handler) {
            collection.Remove(handler);
        }
    }
}
