using AESGame.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AESGame
{
    internal static class CustomDialogManager
    {
        internal static MainWin MainWindow { get; set; }
        internal static void ShowDialog(UserControl userControl)
        {
            MainWindow?.ShowContentAsDialog(userControl);
        }

        internal static void ShowModalDialog(UserControl userControl)
        {
            MainWindow?.ShowContentAsModalDialog(userControl);
        }


        internal static void HideCurrentModal()
        {
            MainWindow?.HideModal();
        }
    }
}
