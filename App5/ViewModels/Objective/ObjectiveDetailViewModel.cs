using App5.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5.ViewModels.Objective
{
    [QueryProperty(nameof(ObjectiveId), nameof(ObjectiveId))]
    public class ObjectiveDetailViewModel : BaseViewModel
    {
        private Models.Objective objective;
        private string objectiveId;
        private string name;
        private string tag;
        private string prioirty;
        private DateTime dateToFinish;
        public string Id { get; set; }

        public ObjectiveDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Tag
        {
            get => tag;
            set => SetProperty(ref tag, value);
        }

        public string Priority
        {
            get => prioirty;
            set => SetProperty(ref prioirty, value);
        }

        public DateTime DateToFinish
        {
            get => dateToFinish;
            set => SetProperty(ref dateToFinish, value);
        }

        public Command SaveCommand { get; }

        private async void OnSave()
        {
            objective.Id = Guid.NewGuid().ToString();
            objective.Name = Name;
            objective.Tag = Tag;
            objective.Priority = Priority;
            objective.DateToFinish = (DateTime)DateToFinish;

            await ObjectiveDataStore.UpdateAsync(objective);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public string ObjectiveId
        {
            get
            {
                return objectiveId;
            }
            set
            {
                objectiveId = value;
                LoadObjectiveId(value);
            }
        }

        public async void LoadObjectiveId(string objectiveId)
        {
            try
            {
                objective = await ObjectiveDataStore.GetAsync(objectiveId);
                Id = objective.Id;
                Name = objective.Name;
                Tag = objective.Tag;
                Priority = objective.Priority;
                DateToFinish = objective.DateToFinish;
                Title = Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Objective");
            }
        }
    }
}
