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
                //look up service code
                if (commandArray.Length < 2)
                    commandArray.Append("Invalid syntax");

                string[] serviceInfo = findServiceInfo(commandArray[1].ToLower());
                //order of return: Service Code, Name, providerID, fee

                if (serviceInfo[0] == "" || serviceInfo.Length < 4)
                {
                    Console.WriteLine("Service Not Found");
                    continue;
                }
                else
                {
                    Console.WriteLine("Service code: " + serviceInfo[0]);
                    Console.WriteLine("Service name: " + serviceInfo[1]);
                    Console.WriteLine("Provider ID: " + serviceInfo[2]);
                    Console.WriteLine("Service fee: " + serviceInfo[3]);
                }

            }
            else
            {
                Console.WriteLine("Invalid Syntax\n");
                continue;
            }
        }
    }

    //order of return: Service Code, Name, providerID, fee
    public string[] findServiceInfo(string input = "")
    {
        //return empty string array if not found


        //look up service code
        if (input == "")
        {
            //prompt user for service keyword
            Console.WriteLine("Enter service keyword:");
            input = Console.ReadLine();
        }

        return database.lookupService(input).Split(',');
    }

    public uint ID
    {
        get { return oID; }
        set { oID = value; }
    }
}
