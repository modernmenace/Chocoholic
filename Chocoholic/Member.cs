using System;
using System.Linq;

public class Member
{
    private uint oID;
    private Database database;

    public Member(uint id, Database db)
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
            else if(commandArray[0].ToLower() == "service")
            {   
                 if(commandArray.Length<3)
                 {
                     Console.WriteLine("Enter service keyword:");
                     commandArray.Append(Console.ReadLine().ToLower());
                 }

                 uint code = findServiceCode(commandArray[2]);
                 if(code == uint.MaxValue)
                 {
                     Console.WriteLine("Service not found, try again.");
                     continue;
                 }

                 uint cost = findServiceCost(commandArray[2]);
                 Console.WriteLine("Service cost (" + commandArray[2] + "): " + cost);
            }
            else
            {
                Console.WriteLine("Invalid Syntax\n");
                continue;
            }
        }
    }

    private uint findServiceCode(string s)
    {
        return uint.MaxValue;
    }

    private uint findServiceCost(string s)
    {
        return uint.MaxValue;
    }

    public uint ID
    {
        get { return oID; }
        set { oID = value; }
    }
}
