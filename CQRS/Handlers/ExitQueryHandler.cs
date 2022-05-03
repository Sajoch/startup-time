using MediatR;
using StartupTimer.CQRS.Requests;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupTimer.CQRS.Handlers {
    public class ExitQueryHandler : IRequestHandler<ExitRequest> {
        public Task<Unit> Handle(ExitRequest request, CancellationToken cancellationToken) {
            Application.Exit();
            return Task.FromResult(new Unit());
        }
    }
}
