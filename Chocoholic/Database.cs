using System;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;
using System.Collections.Generic;

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


    //intialize database
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

    //check if actor exists with given ID
    public bool actorExists(uint id, ActorType type = ActorType.Member)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string table = "Members";

        if      (type == ActorType.Member)   table = "Members";
        else if (type == ActorType.Provider) table = "Providers";
        else if (type == ActorType.Manager)  table = "Managers";
        else if (type == ActorType.Operator) table = "Operators";

        cmd.CommandText = @"SELECT * FROM " + table + " WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            con.Close();
            return true;
        }
        con.Close();
        return false;
    }

    //create service
    public void createService(uint serviceCode, string name, uint providerID, int fee)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = @"INSERT INTO Services(code, name, providerID, fee) VALUES (" + serviceCode + ", '" + name + "', "
            + providerID + ", " + fee + ")";
        cmd.ExecuteNonQuery();

        con.Close();
    }

    public int getMemberStatus(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = "SELECT status FROM Members WHERE id = " + id;
        cmd.ExecuteNonQuery();

        int status = -1;
        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            status = rdr.GetInt32(0);
        }

        con.Close();
        return status;
    }

    //get member info by id
    public string getMember(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string member = "";

        cmd.CommandText = @"Select * FROM Members WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            for (int i = 1; i < rdr.FieldCount; i++)
            {
                member += rdr.GetValue(i);
                member += ",";
            }
        }

        con.Close();
        return member;
    }

    //get number of members
    public int memberCount()
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        int count = 0;

        cmd.CommandText = @"SELECT COUNT(*) FROM Members";
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            count += rdr.GetInt32(0);
        }

        con.Close();
        return count;
    }


    //create service report
    public void createServiceReport(uint memberID, uint providerID, string serviceDate, int fee)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string reportDate = DateTime.Now.ToString("M/d/yyyy");

        cmd.CommandText = @"INSERT INTO Reports(provider, member, fee, serviceDate, reportDate) VALUES (" + providerID + ", " + memberID
            + ", " + fee + ", '" + serviceDate + "', '" + reportDate + "')";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"SELECT id FROM Reports ORDER BY id DESC LIMIT 1;";
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            Console.WriteLine("Report created with id '" + $"{rdr.GetInt32(0)}" + "'!");
        }

        con.Close();
    }

    //get service report, separated by field
    public string getServiceReport(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string report = "";

        cmd.CommandText = @"Select * FROM Reports WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        for (int i = 1; i < rdr.FieldCount; i++)
        {
            report += rdr.GetValue(i);
            report += ",";
        }

        con.Close();
        return report;
    }

    //lookup service code
    //order of return: Service Code, Name, providerID, fee
    public string lookupService(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string service = "";
        cmd.CommandText = @"SELECT * FROM Services WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            for (int i = 1; i < rdr.FieldCount; i++)
            {
                service += rdr.GetValue(i);
                service += ",";
            }
        }

        con.Close();
        return service;
    }

    //get provider info by id
    public string getProvider(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string provider = "";

        cmd.CommandText = @"Select * FROM Providers WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            for (int i = 1; i < rdr.FieldCount; i++)
            {
                provider += rdr.GetValue(i);
                provider += ",";
            }
        }

        con.Close();
        return provider;
    }

    //get number of providers
    public int providerCount()
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        int count = 0;

        cmd.CommandText = @"SELECT COUNT(*) FROM Providers";
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            count += rdr.GetInt32(0);
        }

        con.Close();
        return count;
    }

    //get number of service reports
    public int serviceReportCount()
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        int count = 0;

        cmd.CommandText = @"SELECT COUNT(*) FROM Reports";
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            count += rdr.GetInt32(0);
        }

        con.Close();
        return count;
    }

    //get Member Balance
    public int getMemberBalance(uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = @"SELECT balance FROM Members WHERE id = " + id;
        cmd.ExecuteNonQuery();

        using SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            con.Close();
            return rdr.GetInt32(0);
        }
        con.Close();
        return -1;
    }

    //delete actor
    public void deleteActor(ActorType type, uint id)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        string table = "Members";

        if (type == ActorType.Member) table = "Members";
        else if (type == ActorType.Provider) table = "Providers";
        else if (type == ActorType.Manager) table = "Managers";
        else if (type == ActorType.Operator) table = "Operators";

        cmd.CommandText = @"DELETE FROM " + table + " WHERE id = " + id;
        cmd.ExecuteNonQuery();

        con.Close();
    }

    //delete service
    public void deleteService(uint serviceCode)
    {
        string cs = @"URI=" + db_file;
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);

        cmd.CommandText = @"DELETE FROM Services WHERE code = " + serviceCode;
        cmd.ExecuteNonQuery();

        con.Close();
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
        cmd.CommandText = @"CREATE TABLE Reports(id INTEGER PRIMARY KEY, provider INTEGER, member INTEGER, fee INTEGER, serviceDate TEXT, reportDate TEXT)";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Services(id INTEGER PRIMARY KEY, code INTEGER, name TEXT, providerID INTEGER, fee INTEGER)";
        cmd.ExecuteNonQuery();

        con.Close();

        Console.WriteLine("Database Creation Complete");
        createActor(ActorType.Operator);
    }
}