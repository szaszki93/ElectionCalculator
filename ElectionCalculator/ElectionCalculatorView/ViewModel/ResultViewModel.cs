﻿using ElectionCalculatorService.Entity;
using ElectionCalculatorView.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElectionCalculatorView.ViewModel
{
    public class ResultViewModel : LogoutViewModel
    {
        public ResultViewModel(MainWindowViewModel mainViewModel) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());

            Data = mainViewModel.ResultBusiness.GetResult();
        }

        public ResultViewModel(MainWindowViewModel mainViewModel, Result data) : base(mainViewModel)
        {
            ShowGraphCmd = new RelayCommand(x => ShowGraph());

            Data = data;
        }

        public Result Data { get; private set; }

        public ICommand ShowGraphCmd { get; set; }

        private void ShowGraph()
        {
            mainViewModel.OpenGraphView(Data);
        }
    }
}