using App5.Models;
using App5.Views;
using App5.Views.Objective;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5.ViewModels.Objective
{
    public class ObjectivesViewModel : BaseViewModel
    {
        private Models.Objective _selectedObjective;

        public ObservableCollection<Models.Objective> Objectives { get; }
        public Command LoadObjectivesCommand { get; }
        public Command AddObjectivesCommand { get; }
        public Command FindObjectiveCommand { get; }
        public Command GetAdditionalInfoObjectiveCommand { get; }
        public Command GetBoardOfObjectivesCommand { get; }
        public Command<Models.Objective> ObjectiveTapped { get; }

        public ObjectivesViewModel()
        {
            Title = "Список задач";
            Objectives = new ObservableCollection<Models.Objective>();
            LoadObjectivesCommand = new Command(async () => await ExecuteLoadObjectivesCommand());

            ObjectiveTapped = new Command<Models.Objective>(OnObjectiveSelected);

            AddObjectivesCommand = new Command(OnAddObjective);
            FindObjectiveCommand = new Command(OnFindObjective);
            GetAdditionalInfoObjectiveCommand = new Command(OnAdditionalInfoObjectiveCommand);
            GetBoardOfObjectivesCommand = new Command(OnBoardOfObjectivesCommand);
        }

        async Task ExecuteLoadObjectivesCommand()
        {
            IsBusy = true;

            try
            {
                Objectives.Clear();
                var objectives = await ObjectiveDataStore.GetAsync(true);
                foreach (var objective in objectives)
                {
                    Objectives.Add(objective);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedObjective = null;
        }

        public Models.Objective SelectedObjective
        {
            get => _selectedObjective;
            set
            {
                SetProperty(ref _selectedObjective, value);
                OnObjectiveSelected(value);
            }
        }

        private async void OnAddObjective(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewObjectivePage));
        }

        private async void OnFindObjective(object obj)
        {
            await Shell.Current.GoToAsync(nameof(FindeObjectivePage));
        }
        private async void OnAdditionalInfoObjectiveCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AdditionalInfoObjectivePage));
        }
        private async void OnBoardOfObjectivesCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(BoardObjectivesPage));
        }

        async void OnObjectiveSelected(Models.Objective objective)
        {
            if (objective == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ObjectiveDetailPage)}?{nameof(ObjectiveDetailViewModel.ObjectiveId)}={objective.Id}");
        }
    }
}
