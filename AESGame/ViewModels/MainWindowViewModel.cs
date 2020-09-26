using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AESGame.Models;

namespace AESGame.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public int _tabNumber;
        public int SwitchView
        {
            get => _tabNumber;
            set
            {
                _tabNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SwitchView"));
                }
            }
        }

        public string _AESEncryptText;
        public string AESEncryptText
        {
            get => _AESEncryptText;
            set
            {
                _AESEncryptText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AESEncryptText"));
                }
            }
        }

        DataUsageCheck usageCheck;

        public MainWindowViewModel()
        {
            SwitchView = 0;
            AESEncryptText = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
