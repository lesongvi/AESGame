using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Common
{
    public interface IStartupLoader
    {
        IProgress<(string, int)> PrimaryProgress { get; }
        IProgress<(string, int)> SecondaryProgress { get; }

        string PrimaryTitle { get; set; }
        string SecondaryTitle { get; set; }

        bool SecondaryVisible { get; set; }
    }
}
