using App5.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App5.ViewModels.Note
{
    public class NewNoteViewModel : BaseViewModel
    {
        private string name;
        private string description;
        private bool _hasDrawing;

        public NewNoteViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public bool HasDrawing
        {
            get => _hasDrawing;
            set => SetProperty(ref _hasDrawing, value);
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
            Models.Note newNote = new Models.Note
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                HasDrawing = HasDrawing
            };

            await NoteDataStore.AddAsync(newNote);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
