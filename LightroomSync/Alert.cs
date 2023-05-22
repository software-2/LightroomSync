using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightroomSync
{
    public partial class Alert : Form
    {
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

        private void FlashAppIcon()
        {
            // Create a new instance of FLASHWINFO
            FLASHWINFO flashInfo = new FLASHWINFO
            {
                cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
                hwnd = Process.GetCurrentProcess().MainWindowHandle,
                dwFlags = 2 | 4, // Flash both the caption and taskbar button
                uCount = uint.MaxValue, // Flash indefinitely
                dwTimeout = 0 // Use the default flash rate
            };

            // Call FlashWindowEx to make the application icon flash
            FlashWindowEx(ref flashInfo);
        }

        public Alert(string lastUser)
        {
            InitializeComponent();
            FlashAppIcon();

            label_info.Text = "Another machine (" + lastUser + ") has indicated it is not safe to use Lightroom!" +
                Environment.NewLine + Environment.NewLine +
                "This is either because Lightroom is already open on that machine, or the status file somehow got out of sync. " +
                Environment.NewLine + Environment.NewLine +
                "It is strongly recommended you kill Lightroom here and then gracefully close Lightroom on " + lastUser + "." +
                Environment.NewLine + Environment.NewLine +
                "If you are sure this machine has the most up to date version of your catalogs and it is not open on another machine, click \"Override Catalogs\" " +
                "to erase the status file on your network share. When you close Lightroom, we'll use this machine's catalogs as the new master record.";
        }

        private void button_KillLightroom_Click(object sender, EventArgs e)
        {

        }

        private void Alert_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
