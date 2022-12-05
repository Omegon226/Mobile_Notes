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
    class AdditionalInfoObjectiveViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Objective> Objectives_High { get; }
        public ObservableCollection<Models.Objective> Objectives_Mid { get; }
        public ObservableCollection<Models.Objective> Objectives_Low { get; }

        public ObservableCollection<Models.Objective> Objectives_Full { get; }

        public Command LoadObjectivesCommand { get; }

        public AdditionalInfoObjectiveViewModel()
        {
            Title = "Доп - инфо по задачам";
            Objectives_High = new ObservableCollection<Models.Objective>();
            Objectives_Mid = new ObservableCollection<Models.Objective>();
            Objectives_Low = new ObservableCollection<Models.Objective>();

            Objectives_Full = new ObservableCollection<Models.Objective>();

            LoadObjectivesCommand = new Command(async () => await ExecuteLoadObjectivesCommand());
        }

        async Task ExecuteLoadObjectivesCommand()
        {
            IsBusy = true;

            try
            {
                Objectives_High.Clear();
                Objectives_Mid.Clear();
                Objectives_Low.Clear();

                Objectives_Full.Clear();

                var objectives = await ObjectiveDataStore.GetAsync(true);

                foreach (var objective in objectives)
                {
                    if (objective.Priority == "Высокий")
                    {
                        objective.Color = "#c72e2e";
                        Objectives_High.Add(objective);
                    }
                    if (objective.Priority == "Средний")
                    {
                        objective.Color = "#cc5a5a";
                        Objectives_Mid.Add(objective);
                    }
                    if (objective.Priority == "Низкий")
                    {
                        objective.Color = "#cf9595";
                        Objectives_Low.Add(objective);
                    }
                }

                foreach (var objective in Objectives_High)
                {
                    Objectives_Full.Add(objective);
                }
                foreach (var objective in Objectives_Mid)
                {
                    Objectives_Full.Add(objective);
                }
                foreach (var objective in Objectives_Low)
                {
                    Objectives_Full.Add(objective);
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
            ExecuteLoadObjectivesCommand();
        }
    }
}
