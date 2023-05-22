using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LightroomSync
{
    internal class Config
    {
        public string LocalFolder { get; set; }
        public string NetworkFolder { get; set; }

        public Config() { 
            this.LocalFolder = "C:\\Users\\" + System.Environment.UserName + "\\Pictures\\Lightroom";
            this.NetworkFolder = "P:\\Lightroom";
        }
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        
    }
}
