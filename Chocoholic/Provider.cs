using System;


public class Provider
{
    private uint oID;
    private Database database;

    public Provider(uint id, Database db)
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
                if (commandArray[1].ToLower() == "member")
                {
                    //check to see if member id is valid
                }
                else if (commandArray[0].ToLower() == "service")
                {
                    //look up service code
                }
            }
            else if(commandArray[0].ToLower() == "bill") 
            {
                //Bill Service
                //provider enters member id
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

    public seachServiceDirectory()
    {
        //
    }
}
