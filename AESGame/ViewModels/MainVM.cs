using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AESGame.Models;

namespace AESGame.ViewModels
{
    public class MainVM
    {
        public DataUsageCheck usageCheck;

        public MainVM()
        {
            usageCheck = new DataUsageCheck();
            usageCheck.initData();
        }
    }
}
