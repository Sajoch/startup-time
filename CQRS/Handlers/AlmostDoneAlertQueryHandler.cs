using MediatR;
using StartupTimer.CQRS.Requests;
using StartupTimer.Models;
using StartupTimer.Resources;
using StartupTimer.Views.Report;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupTimer.CQRS.Handlers {
    public class AlmostDoneAlertQueryHandler : IRequestHandler<AlmostDoneAlertRequest> {
        readonly ResourceManager resource = new ResourceManager(typeof(LocalizationResource));
        readonly TimeReport timeReport;
        readonly WorkTimeProvider workTimeProvider;

        public AlmostDoneAlertQueryHandler(WorkTimeProvider workTimeProvider, TimeReport timeReport) {
            this.workTimeProvider = workTimeProvider;
            this.timeReport = timeReport;
        }

        public Task<Unit> Handle(AlmostDoneAlertRequest request, CancellationToken cancellationToken) {
            var caption = resource.GetString("AppName") ?? "AppName";
            var format = resource.GetString("AlmostDoneNotify.Format") ?? "AlmostDoneNotify.Format";
            var workTime = workTimeProvider.GetWorkTime();
            var message = string.Format(format, timeReport.GetLeftTime(workTime));
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            return Task.FromResult(new Unit());
        }
    }
}
