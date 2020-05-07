using System;
using System.Linq;

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
            else if(commandArray[0].ToLower() == "find")
            {
                //finds either member or service
                //prints if member is valid or service code and cost
                
                if(commandArray.Length < 2)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: find (member/service)");
                    continue;
                }
                if (commandArray[1].ToLower() == "member")
                {
                    //check to see if member id is valid
                    if(commandArray.Length<3)
                    {
                        //prompt user for member id
                        Console.WriteLine("Enter member ID:");
                        commandArray.Append(Console.ReadLine());
                    }

                    uint inputID = 0;
                    try { inputID = Int32.Parse(commandArray[2]); }
                    catch (Exception e)
                    {
                        Console.WriteLine("ID entered is not a number, please start over");
                        continue;
                    }

                    if(database.actorExists(inputID, Database.ActorType.Member))
                    {
                        //check if member is suspended
                    }
                    
                }
                else if (commandArray[0].ToLower() == "service")
                {
                    //look up service code
                    if(commandArray.Length<3)
                    {
                        //prompt user for service keyword
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
                    Console.WriteLine("Invalid Syntax\nProper Usage: find (member/service)");
                    continue;
                }
            }
            else if(commandArray[0].ToLower() == "bill") 
            {
                //Bill Service
                //provider enters member id (record and validate)
                //provider enters date of service
                //provider looks up service for coorisponding service code
                //provider confirms service and adds comments
                //provider sends request to database to be logged
                //provider is shown a copy of the data to be saved locally for verification purposes
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

    public uint findServiceCost(string key)
    {
        //
    }

    public uint findServiceCode(string key)
    {
        //return MAX if not found
    }
}
