using System;
using System.Collections.ObjectModel;
using System.Numerics;
using Factorization.Core;
using Factorization.Core.Enums;
using Factorization.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace Factorization.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private DelegateCommand processCommand;
        private string numberString;
        private FactorizationAlgorithmType? algorithmType;
        private FactorizationResult result;

        private IFactorizationController factorizationController;
        private ObservableCollection<string> cachedNumbers;

        public MainViewModel()
        {
            CachedNumbers = new ObservableCollection<string>();
            ProcessCommand = new DelegateCommand(OnProcessCommand, OnCanProcessCommand);
            //Result = new FactorizationResult(BigInteger.Parse("10"), BigInteger.Parse("15"));
        }

        public DelegateCommand ProcessCommand
        {
            get => processCommand;
            set => SetProperty(ref processCommand, value);
        }

        public FactorizationAlgorithmType? SelectedAlgorithmType
        {
            get => algorithmType;
            set
            {
                SetProperty(ref algorithmType, value);
                ProcessCommand.RaiseCanExecuteChanged();
                switch (value)
                {
                    case FactorizationAlgorithmType.Pevnev:
                        factorizationController = new PevnevFactorization();
                        break;
                    case FactorizationAlgorithmType.GreatestCommonDivisor:
                        factorizationController = new PevnevFactorization();
                        break;
                    case FactorizationAlgorithmType.QuadraticSieve:
                        factorizationController = new PevnevFactorization();
                        break;
                    case null:
                        factorizationController = null;
                        break;
                }
            }
        }

        public ObservableCollection<string> CachedNumbers
        {
            get => cachedNumbers;
            set => SetProperty(ref cachedNumbers, value);
        }

        public string NumberString
        {
            get => numberString;
            set
            {
                SetProperty(ref numberString, value);
                ProcessCommand.RaiseCanExecuteChanged();
            }
        }

        public FactorizationResult Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        private bool OnCanProcessCommand() => !String.IsNullOrWhiteSpace(NumberString)
                                              && SelectedAlgorithmType != null;

        private async void OnProcessCommand()
        {
            CachedNumbers.Add(NumberString);
            Result = await factorizationController.ProcessAsync(BigInteger.Parse(NumberString));
        }
    }
}