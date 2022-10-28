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
        private string noteId;
        private string name;
        private string description;
        private bool hasDrawing;
        public string Id { get; set; }

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

        public async void LoadNoteId(string noteId)
        {
            try
            {
                var item = await NoteDataStore.GetAsync(noteId);
                Id = item.Id;
                Name = item.Name;
                Description = item.Description;
                HasDrawing = item.HasDrawing;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Note");
            }
        }
    }
}
