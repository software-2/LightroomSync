namespace LightroomSync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "C:\\Users\\" + System.Environment.UserName + "\\Pictures\\Lightroom";
        }
    }
}