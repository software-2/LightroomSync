using Newtonsoft.Json;
using System.Diagnostics;

namespace LightroomSync
{
    public partial class Form1 : Form
    {
        private Config config = new Config();
        private Status status = new Status();

        private void Log(string message)
        {
            eventsTextBox.Text = message + "\n" + eventsTextBox.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Status.LightroomIsOpen())
            {
                Log("ERROR: Lightroom is open, cannot save to network drive");
                return;
            }

            string[] files = Directory.GetFiles(config.LocalFolder, "*.lrcat");

            foreach (string file in files)
            {
                string catName = Path.GetFileNameWithoutExtension(file);
                
                try
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
                }
            }
        }
    }
}