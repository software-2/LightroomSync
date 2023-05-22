using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO.Compression;
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
                    Log("An error occurred while zipping files and folders: " + ex.Message);
                }


                /*try
                {
                    File.Copy(config.LocalFolder + "\\" + catName + ".lrcat", config.NetworkFolder + "\\" + catName + ".lrcat", true);
                    Log(catName + ".lrcat copied successfully.");
                    File.Copy(config.LocalFolder + "\\" + catName + ".lrcat-data", config.NetworkFolder + "\\" + catName + ".lrcat-data", true);
                    Log(catName + ".lrcat-data copied successfully.");
                    File.Copy(config.LocalFolder + "\\" + catName + " Helper.lrdata", config.NetworkFolder + "\\" + catName + " Helper.lrdata", true);
                    Log(catName + " Helper.lrdata copied successfully.");
                }
                catch (IOException ex)
                {
                    Log("Error copying: " + ex.Message);
                }*/
            }
        }
    }
}