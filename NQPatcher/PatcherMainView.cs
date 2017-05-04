using Microsoft.WindowsAPICodePack.Dialogs;
using NQPatcher.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NQPatcher
{
    public partial class PatcherMainView : Form
    {
        public PatcherMainView()
        {
            InitializeComponent();


        }

        IAsyncResult BeginInvoke(Action action)
        {
            return base.BeginInvoke(action);
        }
        private void PatcherMainView_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(GetCiv5ModFolder));

        }

        private string GetCiv5ModTarget()
        {
            var mod = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None),
                                   PatcherSettings.ModFolder,
                                   PatcherSettings.ModSubFolder);
            return mod;
        }
        private void GetCiv5ModFolder()
        {
            if (UserSettings.Default.Civ5ModFolder.HasValue() == false)
            {
                UserSettings.Default.Civ5ModFolder = GetCiv5ModTarget();

                UserSettings.Default.Save();
            }
            this.Civ5InstallFolderTxt.Text = UserSettings.Default.Civ5ModFolder;
        }

        #region Civ5InstallFolder
        //private void GetCiv5InstallFolder()
        //{
        //    if (UserSettings.Default.Civ5InstallFolder.HasValue() == false)
        //    {

        //        SteamExplorer se = new SteamExplorer();

        //        var installFolder = se.GetCiv5InstallFolder();

        //        UserSettings.Default.Civ5InstallFolder = installFolder;

        //        UserSettings.Default.Save();
        //    }

        //    this.Civ5InstallFolderTxt.Text = UserSettings.Default.Civ5InstallFolder;
        //}


        //private void ManualFolderSetup_Click(object sender, EventArgs e)
        //{
        //    CommonOpenFileDialog cofd = new CommonOpenFileDialog();
        //    cofd.IsFolderPicker = true;
        //    cofd.Multiselect = false;

        //    var result = cofd.ShowDialog();
        //    if (result == CommonFileDialogResult.Ok)
        //    {
        //        UserSettings.Default.Civ5InstallFolder = cofd.FileName;

        //        UserSettings.Default.Save();
        //        this.Civ5InstallFolderTxt.Text = UserSettings.Default.Civ5InstallFolder;
        //    }
        //}

        #endregion


        /// <summary>
        /// Allows the user to override the mod folder by picking the mod folder himself
        /// </summary>
        private void ManualFolderSetup_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.IsFolderPicker = true;
            cofd.Multiselect = false;

            var result = cofd.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                UserSettings.Default.Civ5ModFolder = cofd.FileName;

                UserSettings.Default.Save();
                this.Civ5InstallFolderTxt.Text = UserSettings.Default.Civ5ModFolder;
            }
            /// TODO : Check if folder has "modinfo file, maybe read it ?"
        }
        private void ResetCiv5Folder_Click(object sender, EventArgs e)
        {
            var installFolder = GetCiv5ModTarget();

            UserSettings.Default.Civ5ModFolder = installFolder;

            UserSettings.Default.Save();

            this.Civ5InstallFolderTxt.Text = UserSettings.Default.Civ5ModFolder;
        }

        private void ChangeStatus(string str)
        {
            this.BeginInvoke(() => StatusLabel.Text = str);
        }

        private async void PatchButton_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            await Patch();
        }

        private async Task Patch()
        {
            try
            {
                // reporter is used to pass progress to the view (by just updating the progressBar1.ProgressPercentage)
                var reporter = new ProgressReporter(p => this.ReportProgress(p));
                var patcher = new Patcher();

                // build config with service call of patch infos
                var config = new PatcherConfig { ModFolder = Path.Combine(UserSettings.Default.Civ5ModFolder) };
                patcher.Init(config, reporter);


                ChangeStatus("Downloading patch...");
                // Download patch
                await patcher.DownloadPatch();

                // Backup existing patch
                ChangeStatus("Backuping existing patch...");
                await patcher.BackupMod();


                // Apply patch
                ChangeStatus("Applying patch...");
                await patcher.ApplyPatch();

                ChangeStatus("Done...");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
        }




        private void ReportProgress(int progress)
        {

            this.BeginInvoke(() =>
            {
                this.progressBar1.Value = progress;

            });
        }
    }
}
