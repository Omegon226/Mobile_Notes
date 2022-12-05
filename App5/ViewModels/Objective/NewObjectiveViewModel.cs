using App5.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App5.ViewModels.Objective
{
    public class NewObjectiveViewModel : BaseViewModel
    {
        private string name;
        private string tag;
        private string prioirty;
        private DateTime dateToFinish;

        public NewObjectiveViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
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
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Models.Objective newObjective = new Models.Objective()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Tag = Tag,
                Priority = Priority,
                DateToFinish = (DateTime)DateToFinish
            };

            await ObjectiveDataStore.AddAsync(newObjective);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
