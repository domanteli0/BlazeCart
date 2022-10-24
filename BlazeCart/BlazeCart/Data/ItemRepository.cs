using System.Collections.ObjectModel;
using BlazeCart.Models;
using CommunityToolkit.Maui.Core.Extensions;
using SQLite;

namespace BlazeCart.Data
{
    public class ItemRepository
    {
        private readonly SQLiteConnection _database;

        //TODO: Change database name in method DbPath 
        const string database_name = "example.db";

        public static string DbPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), database_name);

        public ItemRepository()
        {
            _database = new SQLiteConnection(DbPath);
            _database.CreateTable<Item>();
        }

        public List<Item> List()
        {
            return _database.Table<Item>().ToList();
        }
    }
}
