using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using static LightroomSync.Alert;
using static System.Net.Mime.MediaTypeNames;

namespace LightroomSync
{
    public partial class Form1 : Form
    {
        private Config config = new Config();
        private Status status = new Status();

        private bool hasDealtWithLightroomOpen = false;

        private bool timerBeingHandled = false; //Used to handle async events in the timer potentially firing multiple times


        // Struct representing FLASHWINFO
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        // Import the FlashWindowEx function from user32.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        private void StopFlashing()
        {
            FLASHWINFO flashInfo = new FLASHWINFO
            {
                cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
                hwnd = Process.GetCurrentProcess().MainWindowHandle,
                dwFlags = 0, // Stop flashing
                uCount = 0,
                dwTimeout = 0
            };
            FlashWindowEx(ref flashInfo);
        }

        private void Log(string message)
        {
            if (eventsTextBox.InvokeRequired)
            {
                eventsTextBox.Invoke(new Action<string>(Log), message + Environment.NewLine + eventsTextBox.Text);
            }
            else
            {
                eventsTextBox.Text = message + Environment.NewLine + eventsTextBox.Text;
            }
        }

        private async Task UpdateStatusFileOnNetwork()
        {
            await Task.Run(() =>
            {
                try
                {
                    string filePath = config.NetworkFolder + "\\status.txt";
                    File.WriteAllText(filePath, status.ToJson());
                    Log("Updated status file");
                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR: Failed writing status file: " + ex.Message);
                }
            });
        }

        static async Task ZipFilesAndFolders(string zipFilename, string[] filesToZip, string[] foldersToZip)
        {
            await Task.Run(() =>
            {
                using (ZipArchive zipArchive = ZipFile.Open(zipFilename, ZipArchiveMode.Create))
                {
                    // Zip individual files
                    foreach (string filePath in filesToZip)
                    {
                        if (File.Exists(filePath))
                        {
                            string entryName = Path.GetFileName(filePath);
                            zipArchive.CreateEntryFromFile(filePath, entryName);
                        }
                        else
                        {
                            throw new FileNotFoundException($"File not found: {filePath}");
                        }
                    }

                    // Zip folders
                    foreach (string folderPath in foldersToZip)
                    {
                        if (Directory.Exists(folderPath))
                        {
                            string folderName = Path.GetFileName(folderPath);

                            // Zip files inside the folder
                            string[] files = Directory.GetFiles(folderPath);
                            foreach (string filePath in files)
                            {
                                string entryName = Path.Combine(folderName, Path.GetFileName(filePath));
                                zipArchive.CreateEntryFromFile(filePath, entryName);
                            }
                        }
                        else
                        {
                            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
                        }
                    }
                }
            });
        }

        private Status? getNetworkStatus()
        {
            string networkStatusFile = config.NetworkFolder + "\\status.txt";
            if (File.Exists(networkStatusFile))
            {

                string jsonContent = File.ReadAllText(networkStatusFile);
                try
                {
                    return JsonConvert.DeserializeObject<Status>(jsonContent);
                }
                catch (JsonException ex)
                {
                    Log("JSON parsing error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Log("Unexpected error: " + ex.Message);
                }
            }
            return null;
        }

        private async Task UploadCatalogs()
        {
            if (Status.LightroomIsOpen())
            {
                Log("ERROR: Lightroom is open, cannot save to network drive");
                return;
            }

            //Verify status file says it's safe to work (if it exists, otherwise, we can assume this is the first time)
            Status? loadedStatus = getNetworkStatus();
            if (loadedStatus != null && loadedStatus.isSafeToOverride)
            {
                //Safe to proceed
            }
            else if (loadedStatus != null && loadedStatus.LastUser == Environment.MachineName)
            {
                //Safe to proceed since we're the ones who said it wasn't safe.
            }
            else
            {
                Log("Network status file says it's not safe to proceed! Something has gone wrong, or another catalog sync is happening!");
                return;
            }

            status.isSafeToOverride = false;
            try
            {
                await UpdateStatusFileOnNetwork();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return;
            }


            string[] files = Directory.GetFiles(config.LocalFolder, "*.lrcat");

            status.MostRecentVersions = new List<string>();

            foreach (string file in files)
            {
                string catName = Path.GetFileNameWithoutExtension(file);


                string[] filesToZip = { config.LocalFolder + "\\" + catName + ".lrcat" };
                string[] foldersToZip = { config.LocalFolder + "\\" + catName + ".lrcat-data", config.LocalFolder + "\\" + catName + " Helper.lrdata" };

                DateTime lastModified = File.GetLastWriteTime(file);
                string customFormat = catName + " - " + lastModified.ToString("yyyy-MM-dd HH-mm-ss") + ".zip";

                Log("Zipping " + catName);
                try
                {
                    await ZipFilesAndFolders(customFormat, filesToZip, foldersToZip);
                    status.MostRecentVersions.Add(customFormat);
                    Log("Files and folders have been zipped successfully.");
                }
                catch (Exception ex)
                {
                    Log("ERROR: zipping files and folders: " + ex.Message);
                    return;
                }

                Log("Moving " + customFormat + " to " + config.NetworkFolder);
                try
                {
                    await Task.Run(() => { File.Move(customFormat, config.NetworkFolder + "\\" + customFormat, true); });
                    Log(customFormat + " moved successfully.");
                }
                catch (IOException ex)
                {
                    Log("Error moving: " + ex.Message);
                }
            }

            status.isSafeToOverride = true;
            try
            {
                await UpdateStatusFileOnNetwork();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return;
            }
            Log("Sucessfully updated all " + files.Length.ToString() + " catalog(s) to network share.");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void label1_Click(object sender, EventArgs e)
        {
            await UploadCatalogs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.txt"))
            {

                string jsonContent = File.ReadAllText("config.txt");
                try
                {
                    Config? loadedConfig = JsonConvert.DeserializeObject<Config>(jsonContent);

                    if (loadedConfig != null)
                    {
                        config = loadedConfig;
                    }
                    else
                    {
                        Log("JSON deserialization failed");
                    }
                }
                catch (JsonException ex)
                {
                    Log("JSON parsing error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Log("Unexpected error: " + ex.Message);
                }
            }

            localFolderTextBox.Text = config.LocalFolder;
            networkFolderTextBox.Text = config.NetworkFolder;

            status.LastUser = System.Environment.MachineName;
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = "config.txt";
            File.WriteAllText(filePath, config.ToJson());
        }

        private void localFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            config.LocalFolder = localFolderTextBox.Text;
        }

        private void networkFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            config.NetworkFolder = networkFolderTextBox.Text;
        }

        private void buttonSelectLocalFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Show the folder browser dialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Check if the user selected a folder
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Update the text box with the selected folder path
                    localFolderTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonSelectNetworkFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // Show the folder browser dialog
                DialogResult result = folderBrowserDialog.ShowDialog();

                // Check if the user selected a folder
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Update the text box with the selected folder path
                    networkFolderTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timerBeingHandled == true)
            {
                //return;
            }

            HandleTimerEvent();
        }

        private async void HandleTimerEvent()
        {

            timerBeingHandled = true;

            if (Status.LightroomIsOpen() && hasDealtWithLightroomOpen == false)
            {
                timer1.Enabled = false;

                Status? loadedStatus = getNetworkStatus();
                if (loadedStatus != null && loadedStatus.isSafeToOverride == false && loadedStatus.LastUser != Environment.MachineName)
                {
                    Alert alert = new(loadedStatus.LastUser);
                    DialogResult result = alert.ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        Process[] processes = Process.GetProcessesByName("Lightroom");
                        if (processes.Length > 0)
                        {
                            processes[0].Kill();
                            Log("Lightroom process killed. Please close Lightroom on " + loadedStatus.LastUser + " before trying again.");
                            StopFlashing();
                            timer1.Enabled = true;
                            timerBeingHandled = false;
                            return;
                        }
                        else
                        {
                            Log("ERROR: Couldn't find Lightroom process to kill! Either this is a bug or you closed it manually. Please restart this application (and consider filing a bug report on GitHub).");
                            return;
                        }
                    }

                    // Continue if user selected DialogResult.No, since that means they want to wipe the existing status
                    Log("The status file on your network will be overwritten by this machine.");
                    StopFlashing();
                }

                Log("Detected Lightroom is open. Updating the status file to alert other machines.");
                hasDealtWithLightroomOpen = true;
                status.isSafeToOverride = false;
                status.LastUser = Environment.MachineName;
                await UpdateStatusFileOnNetwork();
                timer1.Enabled = true;
                timerBeingHandled = false;
            }
            else if (Status.LightroomIsOpen() == false && hasDealtWithLightroomOpen == true)
            {
                //This means Lightroom WAS open, but isn't anymore. This is where we upload the catalogs.
                timer1.Enabled = false;
                await UploadCatalogs();
                hasDealtWithLightroomOpen = false;
                timer1.Enabled = true;
                timerBeingHandled = false;
            }
            else if (Status.LightroomIsOpen() == false && hasDealtWithLightroomOpen == false)
            {
                // Lightroom has not been open, so there's a possibility the network has newer catalogs.
                timer1.Enabled = false;
                Status? loadedStatus = getNetworkStatus();
                if (loadedStatus == null)
                {
                    timer1.Enabled = true;
                    timerBeingHandled = false;
                    return;
                }

                string[] files = Directory.GetFiles(config.LocalFolder, "*.lrcat");
                string[] timestamped = new string[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    string catName = Path.GetFileNameWithoutExtension(files[i]);
                    DateTime lastModified = File.GetLastWriteTime(files[i]);
                    timestamped[i] = catName + " - " + lastModified.ToString("yyyy-MM-dd HH-mm-ss") + ".zip";
                }

                foreach (string catalog in loadedStatus.MostRecentVersions)
                {
                    if (!timestamped.Contains(catalog))
                    {
                        //We do not have the most recent version.
                        Log("Newer catalog version detected! Copying " + catalog + " to local storage.");
                        try
                        {
                            await Task.Run(() => { File.Copy(config.NetworkFolder + "\\" + catalog, catalog); });
                            Log(catalog + " copied successfully. Erasing existing catalog.");

                            string catName = catalog.Substring(0, catalog.Length - 26); //26 is len(" - yyyy-MM-dd HH-mm-ss.zip")
                            string file1 = config.LocalFolder + "\\" + catName + ".lrcat";
                            string dir1 = config.LocalFolder + "\\" + catName + ".lrcat-data";
                            string dir2 = config.LocalFolder + "\\" + catName + " Helper.lrdata";
                            try
                            {
                                if (File.Exists(file1))
                                {
                                    File.Delete(file1);
                                    Log("Deleted " + file1);
                                }
                                if (Directory.Exists(dir1))
                                {
                                    Directory.Delete(dir1, recursive: true);
                                    Log("Deleted " + dir1);
                                }
                                if (Directory.Exists(dir2))
                                {
                                    Directory.Delete(dir2, recursive: true);
                                    Log("Deleted " + dir2);
                                }
                            }
                            catch (IOException ex)
                            {
                                Log("An I/O error occurred: " + ex.Message);
                                Log("YOU SHOULD MANUALLY EXTRACT THE ZIP TO RECOVER YOUR CATALOG");
                                return;
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                Log("Unauthorized access error occurred: " + ex.Message);
                                Log("YOU SHOULD MANUALLY EXTRACT THE ZIP TO RECOVER YOUR CATALOG");
                                return;
                            }
                            catch (Exception ex)
                            {
                                Log("An error occurred: " + ex.Message);
                                Log("YOU SHOULD MANUALLY EXTRACT THE ZIP TO RECOVER YOUR CATALOG");
                                return;
                            }

                            Log("Extracting zip");
                            try
                            {
                                await Task.Run(() =>
                                {
                                    ZipFile.ExtractToDirectory(catalog, config.LocalFolder);
                                });
                                Log("Unzipped " + catalog + " - Now deleting the zip file locally.");
                            }
                            catch (Exception ex)
                            {
                                Log("An error occurred while unzipping the file: " + ex.Message);
                                Log("YOU SHOULD MANUALLY EXTRACT THE ZIP TO RECOVER YOUR CATALOG");
                            }

                            //You know what? If this file I just created has errors in deleting, I want someone to go file a bug report.
                            //That's absurd, and I'm not adding another wall of error handling around it.
                            File.Delete(catalog);
                            Log("Zip file deleted. This catalog is up to date!");
                        }
                        catch (IOException ex)
                        {
                            Log("Error copying: " + ex.Message);
                        }
                    }
                }

                timer1.Enabled = true;
                timerBeingHandled = false;
            }
        }
    }
}