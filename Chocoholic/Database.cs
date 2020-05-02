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
        Operator,
        None
    }


    //All database code here
    public void initialize()
    {
        if (!File.Exists(db_file))
            createDatabase();
        else
        {
            string cs = @"Data Source=" + db_file;
            using var con = new SQLiteConnection(cs);
            con.Open();
        }
    }

    //create new member or provider
    public void createActor(ActorType type, string name, string address, string city, string state, int zipcode, int balance = 100, int status = 1)
    {
        //This method can only be used with member and provider types
        Debug.Assert(type == ActorType.Member || type == ActorType.Provider);

        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"INSERT INTO Members(name, balance, status, address, city, state, zipcode) VALUES ('" + name + "', " + balance + ", " + status + ", '"
                + address + "', '" + city + "', '" + state + "', " + zipcode + ");";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT id FROM Members ORDER BY id DESC LIMIT 1;";
            cmd.ExecuteNonQuery();

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Member created with id '" + $"{rdr.GetInt32(0)}" + "'!");
            }
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"INSERT INTO Providers(name, address, city, state, zipcode) VALUES ('" + name + "', '"
                + address + "', '" + city + "', '" + state + "', " + zipcode + ");";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"SELECT id FROM Providers ORDER BY id DESC LIMIT 1;";
            cmd.ExecuteNonQuery();

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine("Provider created with id '" + $"{rdr.GetInt32(0)}" + "'!");
            }
        }

        con.Close();
    }

    //create new operator or manager
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

    public void updateName(ActorType type, uint id, string name)
    {
        if (type != ActorType.Member && type != ActorType.Provider) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"UPDATE Members SET name = '" + name + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"UPDATE Providers SET name = '" + name + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    public void updateAddress(ActorType type, uint id, string address)
    {
        if (type != ActorType.Member && type != ActorType.Provider) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"UPDATE Members SET address = '" + address + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"UPDATE Providers SET address = '" + address + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    public void updateCity(ActorType type, uint id, string city)
    {
        if (type != ActorType.Member && type != ActorType.Provider) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"UPDATE Members SET city = '" + city + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"UPDATE Providers SET city = '" + city + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    public void updateState(ActorType type, uint id, string state)
    {
        if (type != ActorType.Member && type != ActorType.Provider) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"UPDATE Members SET state = '" + state + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"UPDATE Providers SET state = '" + state + "' WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    public void updateZipcode(ActorType type, uint id, int zipcode)
    {
        if (type != ActorType.Member && type != ActorType.Provider) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        if (type == ActorType.Member)
        {
            cmd.CommandText = @"UPDATE Members SET zipcode = " + zipcode + " WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }
        else if (type == ActorType.Provider)
        {
            cmd.CommandText = @"UPDATE Providers SET address = " + zipcode + " WHERE id = " + id;
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    public void updateStatus(ActorType type, uint id, int status)
    {
        if (type != ActorType.Member) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = @"UPDATE Members SET status = " + status + " WHERE id = " + id;
        cmd.ExecuteNonQuery();

        con.Close();
    }

    public void updateBalance(ActorType type, uint id, int balance)
    {
        if (type != ActorType.Member) return; //nothing to update
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = @"UPDATE Members SET balance = " + balance + " WHERE id = " + id;
        cmd.ExecuteNonQuery();

        con.Close();
    }


    private void createDatabase()
    {
        //Database doesnt exist yet; set up tables etc.
        string cs = @"Data Source=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();

        //Create Tables
        using var cmd = new SQLiteCommand(con);
        cmd.CommandText = @"CREATE TABLE Providers(id INTEGER PRIMARY KEY,
                    name TEXT, address TEXT, city TEXT, state TEXT, zipcode INTEGER)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Members(id INTEGER PRIMARY KEY,
                    name TEXT, balance INTEGER, status INTEGER, address TEXT, city TEXT, state TEXT, zipcode INTEGER)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Operators(id INTEGER PRIMARY KEY)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Managers(id INTEGER PRIMARY KEY)";
        cmd.ExecuteNonQuery();
        con.Close();

        Console.WriteLine("Database Creation Complete");
        createActor(ActorType.Operator);
    }
}