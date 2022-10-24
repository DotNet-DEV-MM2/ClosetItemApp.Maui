using ClosetItemApp.Maui.Models;
using SQLite;

namespace ClosetItemApp.Maui.Services
{
    public class ClosetItemDatabaseService
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;
        int result = 0;
        public ClosetItemDatabaseService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<ClosetItem>();
        }

        public List<ClosetItem> GetClosetItems()
        {
            try
            {
                Init();
                var items = conn.Table<ClosetItem>().ToList();
                //return conn.Table<ClosetItem>().ToList();
                return items;
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return new List<ClosetItem>();
        }

        public ClosetItem GetClosetItem(int id)
        {
            try
            {
                Init();
                return conn.Table<ClosetItem>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public int DeleteClosetItem(int id)
        {
            try
            {
                Init();
                return conn.Table<ClosetItem>().Delete(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data.";
            }

            return 0;
        }

        public void AddClosetItem(ClosetItem closetItem)
        {
            try
            {
                Init();

                if (closetItem == null)
                    throw new Exception("Invalid ClosetItem Record");

                result = conn.Insert(closetItem);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Insert data.";
            }
        }

        public void UpdateClosetItem(ClosetItem closetItem)
        {
            try
            {
                Init();

                if (closetItem == null)
                    throw new Exception("Invalid ClosetItem Record");

                result = conn.Update(closetItem);
                StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Update data.";
            }
        }
    }
}
