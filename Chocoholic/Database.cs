using System;
using System.IO;
using System.Data.SQLite;

class Database
{
    //All database code here
    public void initialize()
    {
        string dbFile = Directory.GetCurrentDirectory() + "/database.db";
        if (!File.Exists(dbFile))
            createDatabase(dbFile);
        else
        {
            string cs = @"URI=" + dbFile;
            using var con = new SQLiteConnection(cs);
            con.Open();
        }
    }

    private void createDatabase(string file)
    {
        //Database doesnt exist yet; set up tables etc.
        string cs = @"URI=" + file;
        using var con = new SQLiteConnection(cs);
        con.Open();

        //Tables: Providers, Members, Records
        //TODO: Write This
        using var cmd = new SQLiteCommand(con);
        cmd.CommandText = @"CREATE TABLE Providers(id INTEGER PRIMARY KEY,
                    name TEXT)";
        cmd.ExecuteNonQuery();
    }
}