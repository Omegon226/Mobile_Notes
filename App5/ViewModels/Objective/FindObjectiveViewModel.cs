using App5.Models;
using App5.Views.Objective;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace App5.ViewModels.Objective
{
    class FindObjectiveViewModel : BaseViewModel
    {
        private Models.Objective _selectedObjective;

        public ObservableCollection<Models.Objective> Objectives { get; }
        public Command LoadObjectivesCommand { get; }
        public Command<Models.Objective> ObjectiveTapped { get; }

        private string name;
        public FindObjectiveViewModel()
        {
            Objectives = new ObservableCollection<Models.Objective>();
            LoadObjectivesCommand = new Command(async () => await ExecuteLoadObjectivesCommand());

            ObjectiveTapped = new Command<Models.Objective>(OnObjectiveSelected);

            FindCommand = new Command(OnFind);
            CancelCommand = new Command(OnCancel);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public Command FindCommand { get; }
        public Command CancelCommand { get; }

        private async void OnFind()
        {
            await ExecuteLoadObjectivesCommand();
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
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
                    if (name == null)
                    {
                        Objectives.Add(objective);
                    }
                    else
                    {
                        if (objective.Name.Contains(name))
                        {
                            Objectives.Add(objective);
                        }
                    }
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

        async void OnObjectiveSelected(Models.Objective objective)
        {
            if (objective == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ObjectiveDetailPage)}?{nameof(ObjectiveDetailViewModel.ObjectiveId)}={objective.Id}");
        }
    }
}
