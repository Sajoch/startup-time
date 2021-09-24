using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Injection;

namespace startup_timer {
    static class Program {
        static void Main() {
            var container = new UnityContainer();
            container.RegisterType<ITimeGetter, LogonTimeGetter>();
            container.RegisterType<TimeContainer>(new InjectionConstructor(
                new ResolvedParameter<ITimeGetter>(),
                8
             ));

            RegisterTimers(container);

            container.RegisterType<NotifyWidget>();
            container.Resolve<WorkTime>();
            container.ResolveAll<TimerHandler>();
            Application.Run();
        }

        static void RegisterTimers(IUnityContainer container) {
            container.RegisterType<ITimerHandler, OverflowUpdateTimer>("OverflowTimer");
            container.RegisterType<ITimerHandler, WidgetUpdateTimer>("8HourTimer");
            container.RegisterType<TimerHandler>("OverflowTimer", new InjectionConstructor(
                60 * 60000,
                new ResolvedParameter<ITimerHandler>("OverflowTimer")
            ));
            container.RegisterType<TimerHandler>("8HourTimer", new InjectionConstructor(
                1000,
                new ResolvedParameter<ITimerHandler>("8HourTimer")
            ));
        }
    }
}
