using App5.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5.ViewModels.Note
{
    [QueryProperty(nameof(NoteId), nameof(NoteId))]
    public class NoteDetailViewModel : BaseViewModel
    {
        private Models.Note note;
        private string noteId;
        private string name;
        private string description;
        private bool hasDrawing;
        public string Id { get; set; }

        public NoteDetailViewModel()
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

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public bool HasDrawing
        {
            get => hasDrawing;
            set => SetProperty(ref hasDrawing, value);
        }

        public string NoteId
        {
            get
            {
                return noteId;
            }
            set
            {
                noteId = value;
                LoadNoteId(value);
            }
        }

        public Command SaveCommand { get; }

        private async void OnSave()
        {
            note.Id = Guid.NewGuid().ToString();
            note.Name = Name;
            note.Description = Description;
            note.HasDrawing = HasDrawing;

            await NoteDataStore.UpdateAsync(note);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public async void LoadNoteId(string noteId)
        {
            try
            {
                note = await NoteDataStore.GetAsync(noteId);
                Id = note.Id;
                Name = note.Name;
                Description = note.Description;
                HasDrawing = note.HasDrawing;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Note");
            }
        }

    }
}