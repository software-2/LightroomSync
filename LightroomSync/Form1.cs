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
                    string filePath = config.NetworkFolder +  "\\status.txt";
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

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            if (Status.LightroomIsOpen())
            {
                Log("ERROR: Lightroom is open, cannot save to network drive");
                return;
            }

            //TODO: Add network check before proceeding.

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

            status.MostRecentVersions = Array.Empty<string>();

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
                    status.MostRecentVersions.Append(customFormat);
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
    }
}