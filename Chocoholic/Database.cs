using System;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;

public class Database
{
    private string db_file = Directory.GetCurrentDirectory() + "/database.db";

    public enum ActorType
    {
        Member,
        Provider,
        Manager,
        Operator
    }


    //All database code here
    public void initialize()
    {
        if (!File.Exists(db_file))
            createDatabase();
        else
        {
            string cs = @"URI=" + db_file;
            using var con = new SQLiteConnection(cs);
            con.Open();
        }
    }

    //cretae new operator or manager
    public void createActor(ActorType type)
    {
        //This method can only be used with operator and manager types
        Debug.Assert(type == ActorType.Manager || type == ActorType.Operator);

        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Manager)
        {
            cmd.CommandText = @"INSERT INTO Managers default values";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT id FROM Managers ORDER BY id DESC LIMIT 1;";
            cmd.ExecuteNonQuery();

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Manager created with id '" + $"{rdr.GetInt32(0)}" + "'!");
            }
        }
        else if (type == ActorType.Operator)
        {
            cmd.CommandText = @"INSERT INTO Operators default values";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT id FROM Operators ORDER BY id DESC LIMIT 1;";
            cmd.ExecuteNonQuery();

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Operator created with id '" + $"{rdr.GetInt32(0)}" + "'!");
            }
        }

        con.Close();
    }


    private void createDatabase()
    {
        //Database doesnt exist yet; set up tables etc.
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();

        //Create Tables
        using var cmd = new SQLiteCommand(con);
        cmd.CommandText = @"CREATE TABLE Providers(id INTEGER PRIMARY KEY,
                    name TEXT, address TEXT, city TEXT, state TEXT, zipcode INTEGER)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Members(id INTEGER PRIMARY KEY,
                    name TEXT, status INTEGER, address TEXT, city TEXT, state TEXT, zipcode INTEGER)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Operators(id INTEGER PRIMARY KEY)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Managers(id INTEGER PRIMARY KEY)";
        cmd.ExecuteNonQuery();
        con.Close();
    }
}