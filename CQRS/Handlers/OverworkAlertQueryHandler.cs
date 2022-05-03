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
    public class OverworkAlertQueryHandler : IRequestHandler<OverworkAlertRequest> {
        readonly ResourceManager resource = new ResourceManager(typeof(LocalizationResource));
        readonly TimeReport timeReport;
        readonly WorkTimeProvider workTimeProvider;

        public OverworkAlertQueryHandler(WorkTimeProvider workTimeProvider, TimeReport timeReport) {
            this.workTimeProvider = workTimeProvider;
            this.timeReport = timeReport;
        }

        public Task<Unit> Handle(OverworkAlertRequest request, CancellationToken cancellationToken) {
            var caption = resource.GetString("AppName") ?? "AppName";
            var format = resource.GetString("OverworkAlert.Format") ?? "OverworkAlert.Format";
            var workTime = workTimeProvider.GetWorkTime();
            var content = string.Format(format, timeReport.GetWorkTime(workTime));
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            return Task.FromResult(new Unit());
        }
    }
}
