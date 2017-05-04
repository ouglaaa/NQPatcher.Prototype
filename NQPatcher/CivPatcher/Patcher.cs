using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using NQPatcher.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
namespace NQPatcher
{
    public class PatcherConfig
    {
        public string PatchUrl { get; } = "http://cave.dedyn.io/nqpatch/NqMod.DLC.v11.1.zip";
        public string ModFolder { get; set; }
        public string Version { get; set; } = "v11.1";
    }

    public class Patcher : IPatcher
    {

        private ProgressReporter _reporter;
        private PatcherConfig _config;
        private string _patchFileName;
        public void Init(PatcherConfig config, ProgressReporter reporter)
        {
            _reporter = reporter;
            _config = config;
            _patchFileName = $"NqMod.DLC.{_config.Version}.zip";
        }

        public async Task<bool> DownloadPatch()
        {
            WebClient web = new WebClient();

            web.DownloadProgressChanged += (sender, args) =>
            {
                _reporter.ReportProgress(args.ProgressPercentage);
            };
            await web.DownloadFileTaskAsync(_config.PatchUrl, _patchFileName);
            return true;
        }


        public async Task<bool> BackupMod()
        {
            _reporter.ReportProgress(0);
            await ZipFolder();

            return true;
        }

        private Task ZipFolder()
        {
            var evt = new FastZipEvents();
            evt.Progress += (sender, args) => _reporter.ReportProgress((int)(args.PercentComplete));
            evt.DirectoryFailure += (sender, args) => { };
            FastZip zip = new FastZip(evt);

            // TODO: explore current mod version, read modinfo file ?
            return Task.Run(() => zip.CreateZip($"nqMod.v0.zip", _config.ModFolder, true, string.Empty));
        }

        public async Task<bool> ApplyPatch()
        {

            Directory.Move(_config.ModFolder, _config.ModFolder + "_old");
            await UnzipFolder();
            return true;
        }

        private Task UnzipFolder()
        {
            var evt = new FastZipEvents();
            evt.Progress += (sender, args) => _reporter.ReportProgress((int)(args.PercentComplete));
            evt.DirectoryFailure += (sender, args) => { };

            FastZip zip = new FastZip(evt);
            return Task.Run(() => zip.ExtractZip(_patchFileName, _config.ModFolder, string.Empty));

        }
    }
}
