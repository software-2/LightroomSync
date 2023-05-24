using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace LightroomSync
{
    internal class Utils
    {
        public static void CreateShortcutInStartupFolder(string appPath)
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "LightroomSync.lnk");

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = appPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(appPath);
            shortcut.Description = "LightroomSync";
            shortcut.Arguments = "tray";
            shortcut.Save();
        }

        public static bool ShortcutExistsInStartupFolder()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "LightroomSync.lnk");

            return System.IO.File.Exists(shortcutPath);
        }

        public static void DeleteShortcutFromStartupFolder()
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string shortcutPath = Path.Combine(startupFolderPath, "LightroomSync.lnk");

            System.IO.File.Delete(shortcutPath);
        }

        public static string GetWorkingDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\LightroomSync";
        }

        public static void OpenURL(string url)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }
    }
}
