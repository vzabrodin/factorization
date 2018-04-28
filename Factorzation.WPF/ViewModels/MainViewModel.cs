using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Factorization.Core;
using Factorization.Core.Enums;
using Factorization.Core.Interfaces;
using Factorization.WPF.Extensions;
using Prism.Commands;
using Prism.Mvvm;

namespace Factorization.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IFactorizationController factorizationController;
        private Stopwatch stopwatch;

        private DelegateCommand processCommand;
        private DelegateCommand cancelCommand;

        private ObservableCollection<string> cachedNumbers;
        private string numberString;
        private FactorizationAlgorithmType? algorithmType;
        private int? selectesProcessorCount;
        private FactorizationResult result;
        private bool isRunning;
        private TimeSpan timeElapsed;

        public MainViewModel()
        {
            CachedNumbers = new ObservableCollection<string>();
            ProcessCommand = new DelegateCommand(OnProcessCommand, OnCanProcessCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand);

            SelectedAlgorithmType = FactorizationAlgorithmType.QuadraticInequality;
            SelectedProcessorCount = 1;
        }

        public DelegateCommand ProcessCommand
        {
            get => processCommand;
            set => SetProperty(ref processCommand, value);
        }

        public DelegateCommand CancelCommand
        {
            get => cancelCommand;
            set => SetProperty(ref cancelCommand, value);
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
                    case FactorizationAlgorithmType.QuadraticInequality:
                        factorizationController = new QuadraticInequalityFactorization();
                        break;
                    case FactorizationAlgorithmType.GreatestCommonDivisor:
                        factorizationController = new GreatestCommonDivisorFactorization();
                        break;
                    case FactorizationAlgorithmType.QuadraticSieve:
                        factorizationController = new QuadraticSieveFactorization();
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

        public int? SelectedProcessorCount
        {
            get => selectesProcessorCount;
            set
            {
                SetProperty(ref selectesProcessorCount, value);
                ProcessCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsRunning
        {
            get => isRunning;
            private set => SetProperty(ref isRunning, value);
        }

        public FactorizationResult Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        public TimeSpan TimeElapsed
        {
            get => timeElapsed;
            set => SetProperty(ref timeElapsed, value);
        }

        private bool OnCanProcessCommand() => !String.IsNullOrWhiteSpace(NumberString)
                                              && NumberString.All(char.IsDigit)
                                              && SelectedAlgorithmType != null
                                              && SelectedProcessorCount != null
                                              && !IsRunning;

        private async void OnProcessCommand()
        {
            CachedNumbers.AddIfNotExists(NumberString);

            Result = null;
            IsRunning = true;

            stopwatch = Stopwatch.StartNew();

            Result = await factorizationController.ProcessAsync(BigInteger.Parse(NumberString),
                SelectedProcessorCount.GetValueOrDefault());

            stopwatch.Stop();
            TimeElapsed = stopwatch.Elapsed;

            IsRunning = false;
        }

        private void OnCancelCommand()
        {
            factorizationController?.Cancel();
            IsRunning = false;
        }
    }
}