using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Injection;

namespace startup_timer {
    static class Program {
        static void Main() {
            var container = new UnityContainer();
            container.RegisterType<ITimeGetter, LogonTimeGetter>();
            container.RegisterSingleton<TimeContainer>(new InjectionConstructor(
                new ResolvedParameter<ITimeGetter>()
             ));

            RegisterTimers(container);

            container.RegisterType<NotifyWidget>();
            container.Resolve<WorkTime>();
            container.ResolveAll<ITimerHandler>();
            container.ResolveAll<TimerHandler>();
            Application.Run();
        }

        static void RegisterTimers(IUnityContainer container) {
            RegisterTimer<OverflowUpdateTimer>(container, "OverflowTimer", 60 * 6000);
            RegisterTimer<WidgetUpdateTimer>(container, "8HourTimer", 1000);
        }

        static void RegisterTimer<T>(IUnityContainer container, string name, int interval) where T : ITimerHandler {
            container.RegisterType<ITimerHandler, T>(name);
            container.RegisterType<TimerHandler>(name, new InjectionConstructor(
                interval,
                new ResolvedParameter<ITimerHandler>(name)
            ));
        }
    }
}
