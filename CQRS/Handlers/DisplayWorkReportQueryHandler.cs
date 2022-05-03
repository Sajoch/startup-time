using MediatR;
using StartupTimer.CQRS.Requests;
using StartupTimer.Models;
using StartupTimer.Resources;
using StartupTimer.Views.Report;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupTimer.CQRS.Handlers {
    public class DisplayWorkReportQueryHandler : IRequestHandler<DisplayWorkReportRequest> {
        readonly ResourceManager resource = new ResourceManager(typeof(LocalizationResource));
        readonly TimeReport timeReport;
        readonly WorkTimeProvider workTimeProvider;

        public DisplayWorkReportQueryHandler(WorkTimeProvider workTimeProvider, TimeReport timeReport) {
            this.workTimeProvider = workTimeProvider;
            this.timeReport = timeReport;
        }

        public Task<Unit> Handle(DisplayWorkReportRequest request, CancellationToken cancellationToken) {
            var caption = resource.GetString("AppName") ?? "AppName";
            var workTime = workTimeProvider.GetWorkTime();
            var elements = GetElements(workTime).ToArray();
            var message = string.Join("\n", elements);
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            return Task.FromResult(new Unit());
        }

        IEnumerable<string> GetElements(WorkTime workTime) {
            yield return timeReport.GetWorkTimeText(workTime);
            yield return timeReport.GetLeftTimeText(workTime);
            yield return timeReport.GetBeginTimeText(workTime);
            yield return timeReport.GetNormalEndTimeText(workTime);
            yield return timeReport.GetOverTimeText(workTime);
        }
    }
}
