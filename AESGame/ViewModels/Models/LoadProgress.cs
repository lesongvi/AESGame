﻿using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AESGame.ViewModels.Models
{
    public class LoadProgress : NotifyChangedBase, IProgress<(string mess, int perc)>
    {
        private bool _isInstalling;

        public Visibility Visibility
        {
            get
            {
                if (_isInstalling) return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public bool IsInstalling
        {
            get => _isInstalling;
            set
            {
                _isInstalling = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Visibility));
            }
        }

        private int _installProgress;

        public int Progress
        {
            get => _installProgress;
            set
            {
                _installProgress = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ProgressString));
            }
        }

        public string ProgressString
        {
            get => $"{_installProgress}%";
        }

        private string _installStatus;

        public string Status
        {
            get => _installStatus;
            set
            {
                _installStatus = value;
                OnPropertyChanged();
            }
        }

        public void Report((string mess, int perc) value)
        {
            Progress = value.perc;
            Status = value.mess;
        }
    }
}
