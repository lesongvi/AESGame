using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Models
{
    public class UsageDetail
    {
        public bool success { get; set; }
        public int total_usage { get; set; } = -1;
        public int string_usage { get; set; } = -1;
        public int destr_usage { get; set; } = -1;
        public int file_usage { get; set; } = -1;
        public int defile_usage { get; set; } = -1;
        public string ip { get; set; } = "0.0.0.0";
        public long update { get; set; } = 1601829001;
        public List<MessageErr> message { get; set; }
    }
}
