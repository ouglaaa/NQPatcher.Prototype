using Gameloop.Vdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NQPatcher
{
    public interface ISteamExplorer
    {
        string GetGameInstallFolder(string gameName);
    }

    class SteamExplorer : ISteamExplorer
    {
        /// <summary>
        /// Centralized settings static class, not to hardcode too much values inline in every method
        /// </summary>
        private static class Settings
        {
            public const string RegKeyPath = @"Software\Valve\Steam";
            public const string SteamPathKeyName = "SteamPath";
            public const string SteamAppsFolder = "SteamApps";

            public const string LibraryFolderFileName = "libraryfolders.vdf";
            public const string LibraryFolderKeyName = "LibraryFolders";

            public const string GameNameKeyName = "name";
            public const string InstallDirKeyName = "installdir";
            public const string CommonDir = "Common";
        }

        /// <summary>
        /// Get the steam main (install) folder by looking up the windows registry
        /// </summary>
        private string GetSteamAppsFolder()
        {
            string path = null;
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(Settings.RegKeyPath);

            if (regKey != null)
            {
                string installPath = regKey.GetValue(Settings.SteamPathKeyName).ToString().Replace('/', '\\');

                path = Path.Combine(installPath, Settings.SteamAppsFolder);
            }

            if (Directory.Exists(path) == false)
            {
                throw new Exception($"Directory not found: [{path}]");
            }
            return path;
        }


        /// <summary>
        /// Reads the LibraryFolders.vdf file to get all steam lib folders (if user has multiple folder (like one on SSD, multiple on HDD))
        /// </summary>
        private IEnumerable<string> DiscoverLibFolders(string steamAppsFolder)
        {
            List<string> folders = new List<string>();
            folders.Add(steamAppsFolder);
            var libFolders = Path.Combine(steamAppsFolder, Settings.LibraryFolderFileName);
            using (StreamReader reader = new StreamReader(libFolders))
            {
                var serial = new VdfSerializer();

                dynamic file = serial.Deserialize(reader);
                var token = file.Value as VObject;
                for (int i = 1; i < 100; ++i)
                {
                    VProperty vprop;
                    if (token.TryGetValue(i.ToString(), out vprop) == false)
                        break;
                    var folder = vprop.Value.ToString().Replace("\\\\", "\\");
                    if (Directory.Exists(folder))
                        folders.Add(Path.Combine(folder, Settings.SteamAppsFolder)); // concats /steamapps/ to the library folders
                    else
                    {
                       // Debug.WriteLine($"Folder [{folder}] does not exist");
                    }
                }
            }

            return folders;
        }

        private IEnumerable<string> GetSteamLibraryFolders()
        {
            string mainFolder = GetSteamAppsFolder();

            var libFolders = Enumerable.Empty<string>();

            return new[] { mainFolder }.Concat(libFolders);
        }


        /// <summary>
        /// Look for the game ACF file by parsing every acf file in every lib folders to look for the game name
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns></returns>
        public string GetGameInstallFolder(string gameName)
        {
            var steamApps = GetSteamAppsFolder();

            var libFolders = DiscoverLibFolders(steamApps);

            foreach (var lib in libFolders)
            {
                (var found, var folder) = LookFor(gameName, lib);
                if (found)
                    return folder;
            }
            return null;
        }

        /// <summary>
        /// Parse every ACF file in the library folder to look for the game ACF file
        /// </summary>
        /// <param name="gameName">game name to look for</param>
        /// <param name="lib">library folder path</param>
        /// <returns>
        /// found : have we found the libFolder
        /// folder: the game install folder
        /// </returns>
        private (bool found, string folder) LookFor(string gameName, string lib)
        {
            foreach (var acf in Directory.GetFiles(lib, "*.acf"))
            {
                (var found, var folder) = LookInFile(gameName, acf);
                if (found)
                {
                    var installFolder = Path.Combine(lib, Settings.CommonDir, folder);

                    return (found, installFolder);
                }
            }
            return (false, string.Empty);
        }

        /// <summary>
        /// Parse the ACF file to check if its the right one
        /// </summary>
        /// <param name="gameName">game name</param>
        /// <param name="acf">acf file path</param>
        /// <returns>
        /// found: was it the right ACF file
        /// folder: game install folder
        /// </returns>
        private (bool found, string folder) LookInFile(string gameName, string acf)
        {
            using (var reader = new StreamReader(acf))
            {
                (var name, var installDir) = ReadFile(reader);
                //Debug.WriteLine($"Parsing File: {acf}");
                //Debug.WriteLine($"name: {name} installDir: {installDir}");

                if (name == gameName) 
                    return (true, installDir);
                return (false, string.Empty); 
            }
        }

        /// <summary>
        /// ACF file parsing method, deserializes the file into an object, return the right properties 
        /// </summary>
        /// <param name="reader">the acf file reader</param>
        /// <returns>
        /// name: the ACF AppState.name property
        /// installDir: the ACF AppState.installdir property
        /// </returns>
        private (string name, string installDir) ReadFile(StreamReader reader)
        {

            var serial = new VdfSerializer();
            var file = serial.Deserialize(reader);
            
            var f = file.Value as VObject;

            (var found, var vprop) = f.GetValue(Settings.GameNameKeyName);
            if (!found)
                throw new InvalidOperationException($"Could not find property {Settings.GameNameKeyName}");

            var name = vprop.Value.ToString();
            (found, vprop) = f.GetValue(Settings.InstallDirKeyName);
            if (!found)
                throw new InvalidOperationException($"Could not find property {Settings.InstallDirKeyName}");

            var installDir = vprop.Value.ToString();

            return (name, installDir);
        }
    }

    public static class SteamExplorerExtensions
    {
        /// <summary>
        /// Gets the Civ5 install folder
        /// </summary>
        /// <param name="svc"></param>
        /// <returns></returns>
        public static string GetCiv5InstallFolder(this ISteamExplorer svc)
        {
            return svc.GetGameInstallFolder("Sid Meier's Civilization V");
        }
    }
}
