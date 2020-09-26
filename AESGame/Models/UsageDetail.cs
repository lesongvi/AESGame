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
        public int total_usage { get; set; }
        public int string_usage { get; set; }
        public int destr_usage { get; set; }
        public int file_usage { get; set; }
        public int defile_usage { get; set; }
        public string ip { get; set; }
        public long update { get; set; }
    }
}
