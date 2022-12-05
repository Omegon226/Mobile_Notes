using App5.Models;
using App5.Views.Note;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App5.ViewModels.Note
{
    class FindNoteViewModel : BaseViewModel
    {
        private Models.Note _selectedNote;

        public ObservableCollection<Models.Note> Notes { get; }
        public Command LoadNotesCommand { get; }
        public Command<Models.Note> NoteTapped { get; }

        private string name = null;
        public FindNoteViewModel()
        {
            Notes = new ObservableCollection<Models.Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            NoteTapped = new Command<Models.Note>(OnNoteSelected);

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
            await ExecuteLoadNotesCommand();
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
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
                    if (name == null)
                    {
                        Notes.Add(note);
                    }
                    else 
                    {
                        if (note.Name.Contains(name))
                        {
                            Notes.Add(note);
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

        async void OnNoteSelected(Models.Note note)
        {
            if (note == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.NoteId)}={note.Id}");
        }
    }
}
