using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App5.Models;

namespace App5.Services
{
    public class NoteDataStore : IDataStore<Note>
    {
        readonly List<Models.Note> notes = new List<Models.Note>();
        const string _notesJsonFileName = "Notes.json";

        #region Строка JSON для Notes
        string jsonNotesText = @"[
            {
                'Id': 1,
                'Name': 'Заметка 1',
                'Description': '',
                'HasDrawing': false
            },
            {
                'Id': 2,
                'Name': 'Заметка 2',
                'Description': 'Ну что то надо поделать',
                'HasDrawing': false
            },
            {
                'Id': 3,
                'Name': 'Заметка 3',
                'Description': 'Все я устал',
                'HasDrawing': false
            }
            ]";
        #endregion

        string pathPersonal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public NoteDataStore()
        {
            var path_notes = Path.Combine(pathPersonal, _notesJsonFileName);
            //File.WriteAllText(path_notes, jsonNotesText);
            var jsonNotesContent = File.ReadAllText(path_notes);

            notes.AddRange(JsonConvert.DeserializeObject<List<Note>>(jsonNotesContent));

            //notes = new List<Note>()
            //{
            //    new Note { Id = Guid.NewGuid().ToString(), Name = "Заметка 1", Description = "", HasDrawing = false },
            //    new Note { Id = Guid.NewGuid().ToString(), Name = "Заметка 2", Description = "Ну что то надо поделать", HasDrawing = false },
            //    new Note { Id = Guid.NewGuid().ToString(), Name = "Заметка 3", Description = "Все я устал", HasDrawing = false },
            //};
        }

        public async Task<bool> AddAsync(Note note)
        {
            notes.Add(note);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Note note)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == note.Id).FirstOrDefault();
            notes.Remove(oldItem);
            notes.Add(note);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == id).FirstOrDefault();
            notes.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Note> GetAsync(string id)
        {
            return await Task.FromResult(notes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Note>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(notes);
        }
    }
}
