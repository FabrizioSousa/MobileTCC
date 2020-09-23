using PrimeiraVersao.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static PrimeiraVersao.Models.Usuario;

namespace PrimeiraVersao.ViewModels
{
    class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> GetItemsAsync()
        {
            return db.Table<Usuario>().ToListAsync();
        }
    }
}
