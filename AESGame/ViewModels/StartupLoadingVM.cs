﻿using AESGame.Common;
using AESGame.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.ViewModels
{
    public class StartupLoadingVM : BaseVM, IStartupLoader
    {
        public LoadProgress PrimaryProgress { get; } = new LoadProgress();
        public LoadProgress SecondaryProgress { get; } = new LoadProgress();

        IProgress<(string, int)> IStartupLoader.PrimaryProgress => PrimaryProgress;
        IProgress<(string, int)> IStartupLoader.SecondaryProgress => SecondaryProgress;

        private static readonly string PlaceholderTitle = "Đang tải, vui lòng chờ...";

        private string _primaryTitle = PlaceholderTitle;
        public string PrimaryTitle
        {
            get => _primaryTitle;
            set
            {
                _primaryTitle = value;
                OnPropertyChanged();
            }
        }

        private string _secondaryTitle = PlaceholderTitle;
        public string SecondaryTitle
        {
            get => _secondaryTitle;
            set
            {
                _secondaryTitle = value;
                OnPropertyChanged();
            }
        }

        private bool _secondaryVisible;
        public bool SecondaryVisible
        {
            get => _secondaryVisible;
            set
            {
                _secondaryVisible = value;
                OnPropertyChanged();
            }
        }

        public StartupLoadingVM()
            : base("Loading")
        { }
    }
}
