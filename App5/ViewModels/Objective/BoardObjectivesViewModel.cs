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
    class BoardObjectivesViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Objective> Objectives_NoTag { get; }
        public ObservableCollection<Models.Objective> Objectives_InWork { get; }
        public ObservableCollection<Models.Objective> Objectives_Awaiting { get; }
        public ObservableCollection<Models.Objective> Objectives_Done { get; }

        public Command LoadObjectivesCommand_NoTag { get; }
        public Command LoadObjectivesCommand_InWork { get; }
        public Command LoadObjectivesCommand_Awaiting { get; }
        public Command LoadObjectivesCommand_Done { get; }

        public BoardObjectivesViewModel()
        {
            Title = "Доска задач";
            Objectives_NoTag = new ObservableCollection<Models.Objective>();
            Objectives_InWork = new ObservableCollection<Models.Objective>();
            Objectives_Awaiting = new ObservableCollection<Models.Objective>();
            Objectives_Done = new ObservableCollection<Models.Objective>();

            LoadObjectivesCommand_NoTag = new Command(async () => await ExecuteLoadObjectivesCommand_NoTag());
            LoadObjectivesCommand_InWork = new Command(async () => await ExecuteLoadObjectivesCommand_InWork());
            LoadObjectivesCommand_Awaiting = new Command(async () => await ExecuteLoadObjectivesCommand_Awaiting());
            LoadObjectivesCommand_Done = new Command(async () => await ExecuteLoadObjectivesCommand_Done());
        }
        async Task ExecuteLoadObjectivesCommand_NoTag()
        {
            IsBusy = true;

            try
            {
                Objectives_NoTag.Clear();

                var objectives = await ObjectiveDataStore.GetAsync(true);

                foreach (var objective in objectives)
                {
                    if (objective.Tag == "Без метки")
                    {
                        Objectives_NoTag.Add(objective);
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

        async Task ExecuteLoadObjectivesCommand_InWork()
        {
            IsBusy = true;

            try
            {
                Objectives_InWork.Clear();

                var objectives = await ObjectiveDataStore.GetAsync(true);

                foreach (var objective in objectives)
                {
                    if (objective.Tag == "В работе")
                    {
                        Objectives_InWork.Add(objective);
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

        async Task ExecuteLoadObjectivesCommand_Awaiting()
        {
            IsBusy = true;

            try
            {
                Objectives_Awaiting.Clear();

                var objectives = await ObjectiveDataStore.GetAsync(true);

                foreach (var objective in objectives)
                {
                    if (objective.Tag == "В ожидании")
                    {
                        Objectives_Awaiting.Add(objective);
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

        async Task ExecuteLoadObjectivesCommand_Done()
        {
            IsBusy = true;

            try
            {
                Objectives_Done.Clear();

                var objectives = await ObjectiveDataStore.GetAsync(true);

                foreach (var objective in objectives)
                {
                    if (objective.Tag == "Закончено")
                    {
                        Objectives_Done.Add(objective);
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
            ExecuteLoadObjectivesCommand_NoTag();
            ExecuteLoadObjectivesCommand_InWork();
            ExecuteLoadObjectivesCommand_Awaiting();
            ExecuteLoadObjectivesCommand_Done();
        }
    }
}
