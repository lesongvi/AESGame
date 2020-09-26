using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AESGame.Models
{
    public class Data
    {
        public string ip { get; set; }
        public string action { get; set; }

        public override string ToString()
        {
            return $"{ip}: {action}";
        }
    }
}
