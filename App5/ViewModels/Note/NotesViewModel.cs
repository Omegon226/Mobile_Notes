using App5.Models;
using App5.Views;
using App5.Views.Note;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5.ViewModels.Note
{
    class NotesViewModel : BaseViewModel
    {
        private Models.Note _selectedNote;

        public ObservableCollection<Models.Note> Notes { get; }
        public Command LoadNotesCommand { get; }
        public Command AddNotesCommand { get; }
        public Command FindNoteCommand { get; }
        public Command<Models.Note> NoteTapped { get; }

        public NotesViewModel()
        {
            Title = "Список заметок";
            Notes = new ObservableCollection<Models.Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            NoteTapped = new Command<Models.Note>(OnNoteSelected);

            AddNotesCommand = new Command(OnAddNote);

            FindNoteCommand = new Command(OnFindNote);
        }

        async Task ExecuteLoadNotesCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();
                var notes = await NoteDataStore.GetAsync(true);
                foreach (var note in notes)
                {
                    Notes.Add(note);
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
            SelectedNote = null;
        }

        public Models.Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnNoteSelected(value);
            }
        }

        private async void OnAddNote(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewNotePage));
        }

        private async void OnFindNote(object obj)
        {
            await Shell.Current.GoToAsync(nameof(FindNotePage));
        }

        async void OnNoteSelected(Models.Note note)
        {
            if (note == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.NoteId)}={note.Id}");
        }
    }
}
