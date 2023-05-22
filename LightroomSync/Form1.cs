using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace LightroomSync
{
    public partial class Form1 : Form
    {
        private Config config = new Config();
        private Status status = new Status();

        private bool hasDealtWithLightroomOpen = false;

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

        private async Task UploadCatalogs()
        {
            if (Status.LightroomIsOpen())
            {
                Log("ERROR: Lightroom is open, cannot save to network drive");
                return;
            }

            //Verify status file says it's safe to work (if it exists, otherwise, we can assume this is the first time)
            string networkStatusFile = config.NetworkFolder + "\\status.txt";
            if (File.Exists(networkStatusFile))
            {

                string jsonContent = File.ReadAllText(networkStatusFile);
                try
                {
                    Status? loadedStatus = JsonConvert.DeserializeObject<Status>(jsonContent);

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

        private async void button1_Click(object sender, EventArgs e)
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

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (Status.LightroomIsOpen() && hasDealtWithLightroomOpen == false)
            {
                timer1.Enabled = false;
                Log("Detected Lightroom is open. Updating the status file to alert other machines.");
                hasDealtWithLightroomOpen = true;
                status.isSafeToOverride = false;
                status.LastUser = Environment.MachineName;
                await UpdateStatusFileOnNetwork();
                timer1.Enabled = true;
            }
            else if (Status.LightroomIsOpen() == false && hasDealtWithLightroomOpen == true)
            {
                //This means Lightroom WAS open, but isn't anymore. This is where we upload the catalogs.
                timer1.Enabled = false;
                await UploadCatalogs();
                hasDealtWithLightroomOpen = false;
                timer1.Enabled = true;
            }
        }
    }
}