using Organiser.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organiser.Data
{
    public class NoteDataBase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<Note>().Wait();
        }

        public void ClearNoteTable()
        {
            database.DropTableAsync<Note>().Wait();
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()
        {
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return database.Table<Note>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)
            {
                return database.UpdateAsync(note);
            }
            else
            {
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return database.DeleteAsync(note);
        }
    }
}
