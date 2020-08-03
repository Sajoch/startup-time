using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace startup_timer {
	static class Program {
		static void Main() {
			var watchDog = new WorkTime();
			Application.Run();
		}
	}
}
