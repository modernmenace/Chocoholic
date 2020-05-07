using System;


public class Manager
{
    private uint oID;
    private Database database;

    public Manager(uint id, Database db)
    {
        ID = id;
        database = db;
    }

    public void terminal()
    {
        while (true)
        {
            string command = Console.ReadLine();
            var commandArray = command.Split(' ');

            if (commandArray[0].ToLower().Contains("exit"))
            {
                System.Environment.Exit(0);
            }
            else if(commandArray[0].ToLower() == "search")
            {
                Console.WriteLine("Please enter the ID of the provider:");
                uint mId = 0;
                try
                {
                    mId = UInt32.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("ID is not of valid format");
                    continue;
                }
                string report = database.getServiceReport(mId);
                Console.WriteLine(report);
            }
            else
            {
                Console.WriteLine("Invalid Syntax\n");
                continue;
            }
        }
    }

    public uint ID
    {
        get { return oID; }
        set { oID = value; }
    }
}