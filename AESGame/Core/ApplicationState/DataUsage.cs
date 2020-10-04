using AESGame.Common;
using AESGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace AESGame.Core.ApplicationState
{
    public class DataUsage : NotifyChangedBase
    {
        public UsageDetail usage;
        public DataUsageCheck usageCheck = new DataUsageCheck();

        public static DataUsage Instance { get; } = new DataUsage();

        public DataUsage() { }

        public void GeneralConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            setDataUsage();
        }

        public void setDataUsage()
        {
            AESUsage hashme;
            usage = usageCheck.initData();

            AESStringUsage = usage.string_usage;
            AESFileUsage = usage.file_usage;
            hashme = new AESUsage(usage);
            encryptUsage = new encryptUsageViewModel(hashme).dataUsage;
            DescText = "Bạn đã mã hóa " + usage.string_usage + " chuỗi và " + usage.file_usage + " file";
            AESStringDesc = "Bạn đã mã hóa " + usage.string_usage  + " chuỗi trong " + usage.total_usage + " lần";
            AESFileDesc = "Bạn đã mã hóa " + usage.file_usage + " file trong " + usage.total_usage + " lần";
            AESStringPerc = Math.Round(((double)usage.string_usage / usage.total_usage * 100), 2) + "%";
            AESFilePerc = Math.Round(((double)usage.file_usage / usage.total_usage * 100), 2) + "%";
            OnPropertyChanged(nameof(AESStringUsage));
            OnPropertyChanged(nameof(AESFileUsage));
            OnPropertyChanged(nameof(encryptUsage));
            OnPropertyChanged(nameof(DescText));
        }

        public void safeReloadPlugins()
        {
            this.setDataUsage();
        }

        public List<AESUsage> encryptUsage { get; private set; }
        public int AESStringUsage { get; private set; }
        public int AESFileUsage { get; private set; }
        public string DescText { get; private set; }
        public string AESStringDesc { get; private set; }
        public string AESFileDesc { get; private set; }
        public string AESStringPerc { get; private set; }
        public string AESFilePerc { get; private set; }
    }
}
