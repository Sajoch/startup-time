using MediatR;
using StartupTimer.CQRS.Requests;
using StartupTimer.Models;
using StartupTimer.Resources;
using StartupTimer.TimeProviders;
using StartupTimer.Timers;
using StartupTimer.Views.Report;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace StartupTimer.Views {
    internal class NotifyWidget : IDisposable, ITimerHandler {
        readonly Container components = new Container();
        readonly ContextMenu contextMenu = new ContextMenu();
        readonly ICurrentTimeProvider currentTimeProvider;
        readonly IMediator mediator;
        readonly ResourceManager resource = new ResourceManager(typeof(LocalizationResource));
        readonly TimeReport timeReport;
        readonly NotifyIcon widget;
        readonly WorkTimeProvider workTimeProvider;

        public NotifyWidget(IMediator mediator, WorkTimeProvider workTimeProvider, TimeReport timeReport, ICurrentTimeProvider currentTimeProvider) {
            this.mediator = mediator;
            this.workTimeProvider = workTimeProvider;
            this.timeReport = timeReport;
            this.currentTimeProvider = currentTimeProvider;
            InitializeContextMenu();

            widget = new NotifyIcon(components) {
                ContextMenu = contextMenu
            };
            UpdateView();
            widget.DoubleClick += NotifyIconOnClick;
            widget.Visible = true;
        }

        public void Dispose() {
            components?.Dispose();
            widget.Dispose();
        }

        public void Update() {
            UpdateView();
        }

        void UpdateView() {
            var workTime = workTimeProvider.GetWorkTime();
            var isOverwork = workTime.IsOverwork(currentTimeProvider.GetTime());
            widget.Icon = GetIcon(isOverwork);
            widget.Text = timeReport.GetWorkTimeText(workTime);
        }

        void InitializeContextMenu() {
            var exitItem = new MenuItem {
                Index = 0,
                Text = GetText("ContextMenu.Exit.Text")
            };
            exitItem.Click += ExitItemOnClick;

            contextMenu.MenuItems.Add(exitItem);
        }

        void NotifyIconOnClick(object sender, EventArgs e) {
            mediator.Send(new DisplayWorkReportRequest());
        }

        void ExitItemOnClick(object sender, EventArgs e) {
            mediator.Send(new ExitRequest());
        }

        static Icon GetIcon(bool isOverwork) {
            return isOverwork ? Properties.Resources.Icon2 : Properties.Resources.Icon1;
        }

        string GetText(string key) {
            return resource.GetString(key) ?? key;
        }
    }
}
