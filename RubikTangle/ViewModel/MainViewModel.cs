using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RubikTangle.Interfaces;
using RubikTangle.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace RubikTangle.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly ITileCollection tileCollection;
        private readonly ISolvePuzzleService solvePuzzleService;
        private CancellationTokenSource cts;

        #region Properties

        public int MatrixSize { get; private set; }

        public ObservableCollection<Tile> Tiles
        {
            get
            {
                return solvePuzzleService.Solution;
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning == value)
                    return;

                isRunning = value;
                RaisePropertyChanged();
            }
        }

        private readonly ObservableCollection<SolveSpeed> solveSpeedSource;

        public ObservableCollection<SolveSpeed> SolveSpeedSource
        {
            get
            {
                return solveSpeedSource;
            }
        }

        private SolveSpeed selectedSolveSpeed;

        public SolveSpeed SelectedSolveSpeed
        {
            get
            {
                return selectedSolveSpeed;
            }
            set
            {
                if (selectedSolveSpeed == value)
                    return;

                selectedSolveSpeed = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand shuffleCommand;

        public RelayCommand ShuffleCommand
        {
            get
            {
                return shuffleCommand ?? (shuffleCommand = new RelayCommand(() => ShuffleTiles()));
            }
        }

        private RelayCommand solvePuzzleCommand;

        public RelayCommand SolvePuzzleCommand
        {
            get
            {
                return solvePuzzleCommand ?? (solvePuzzleCommand = new RelayCommand(async () => await ProcessStartStop()));
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService, ITileCollection tileCollection, ISolvePuzzleService solvePuzzleService)
        {
            MatrixSize = ViewModelLocator.MATRIXSIZE;

            // static values are sufficient here
            solveSpeedSource = new ObservableCollection<SolveSpeed>()
            {
                new SolveSpeed() { Name = "Slow", Speed = 500 },
                new SolveSpeed() { Name = "Fast", Speed = 50 },
                new SolveSpeed() { Name = "Without delay", Speed = 0 }
            };

            this.dialogService = dialogService;
            this.tileCollection = tileCollection;
            this.solvePuzzleService = solvePuzzleService;

            ShuffleTiles();
        }

        private void ShuffleTiles()
        {
            tileCollection.Shuffle();
            solvePuzzleService.DisplayTileSet(tileCollection.Tiles);
        }

        private async Task ProcessStartStop()
        {
            if (IsRunning)
            {
                cts.Cancel();
            }
            else
            {
                cts = new CancellationTokenSource();
                IsRunning = true;

                try
                {
                    bool result = await solvePuzzleService.SolveAsync(tileCollection.Tiles, MatrixSize, SelectedSolveSpeed.Speed, cts.Token);

                    if (result)
                        dialogService.ShowMessage("Success", "The task was solved successfully");
                    else
                        dialogService.ShowMessage("Failure", "The task could not be solved");
                }
                catch (TaskCanceledException)
                {
                    // cancelled, nothing to do here
                }
                catch (Exception ex)
                {
                    dialogService.ShowError("Error!", ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }
        }
    }
}
