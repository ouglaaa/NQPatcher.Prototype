using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQPatcher.Ui
{
    /// <summary>
    /// Util class to report progress to the view
    /// </summary>
    public class ProgressReporter
    {
        private Action<int> _onProgress;

        public ProgressReporter(Action<int> onProgress)
        {
            _onProgress = onProgress;
        }
        public void ReportProgress(int progress)
        {
            _onProgress(progress);
        }
    }
}
