using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LightroomSync
{
    internal class Status
    {
        public string LastUser { get; set; }
        public static bool LightroomIsOpen()
        {
            Process[] processes = Process.GetProcessesByName("Lightroom");
            return processes.Length > 0;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
