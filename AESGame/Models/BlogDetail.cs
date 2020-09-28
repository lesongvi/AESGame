using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Models
{
    public class BlogDetail
    {
        public bool success { get; set; }
        public List<BlogData> data { get; set; }
    }

    public class BlogData
    {
        public string title { get; set; }
        public string content { get; set; }
        public string blogUri { get; set; }
    }
}
