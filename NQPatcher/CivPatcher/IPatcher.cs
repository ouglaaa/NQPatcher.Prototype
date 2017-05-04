using NQPatcher.Ui;
using System.Threading.Tasks;

namespace NQPatcher
{
    public interface IPatcher
    {
        void Init(PatcherConfig config, ProgressReporter reporter);
        Task<bool> DownloadPatch();
        Task<bool> BackupMod();
        Task<bool> ApplyPatch();
    }
}