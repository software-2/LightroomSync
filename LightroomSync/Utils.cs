using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
