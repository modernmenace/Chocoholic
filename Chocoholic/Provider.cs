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
            Console.WriteLine("--Provider Terminal--");
            string command = Console.ReadLine();
            var commandArray = command.Split(' ');
            commandArray[0] = commandArray[0].ToLower();

            if (commandArray[0].Contains("exit"))
            {
                System.Environment.Exit(0);
            }
            else if(commandArray[0] == "find")
            {
                //finds either member or service
                //prints if member is valid or service code and cost
                
                if(commandArray.Length < 2)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: find (member/service)");
                    continue;
                }

                commandArray[1] = commandArray[1].ToLower();

                if (commandArray[1] == "member")
                {
                    if(commandArray.Length<3)
                        commandArray.Append("");
                    
                    string[] memberInfo = findMemberInfo(commandArray[2].ToLower());
                    if(member[0] == "")
                        Console.WriteLine("Member not found");
                    else
                        Console.WriteLine("Member Validated");
                }
                else if (commandArray[1] == "service")
                {
                    //look up service code
                    if(commandArray.Length<3)
                        commandArray.Append("");

                    string[] serviceInfo = findServiceInfo(commandArray[2].ToLower());
                    //order of return: Service Code, Name, providerID, fee

                    if(serviceInfo[0] == "" || serviceInfo.Length < 4)
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
                    Console.WriteLine("Invalid Syntax\nProper Usage: find (member/service)");
                    continue;
                }
            }
            else if(commandArray[0] == "bill") 
            {
                //Bill Service
                //provider enters member id (record and validate)
                if(commandArray.Length<3)
                    commandArray.Append("");

                //validate member handles all IO
                string memberInfo = findMemberInfo(commandArray[1].ToLower());
                //do more here

                //provider enters date of service (no validation)
                Console.WriteLine("Enter date of service: ");
                string serviceDate = Console.ReadLine();

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

    //order of return: name, balance, status, address, city, state, zipcode
    public string[] findMember(string input = "") 
    {
        //returns empty string array on fail
        

        string[] temp = new string[1];
        temp[0] = "";

        //check to see if member id is valid
        if(input == "")
        {
            //prompt user for member id
            Console.WriteLine("Enter member ID:");
            input = Console.ReadLine();
        }

        uint memberID = 0;
        try { memberID = Int32.Parse(input); }
        catch (Exception e)
        {
            Console.WriteLine("ID entered is not a number");
            return temp;
        }

        if(database.actorExists(memberID, Database.ActorType.Member))
        {
            //check if member is suspended
            int memberStatus = database.getMemberStatus(memberID);
            if(memberStatus == 0) //suspended
            {
                Console.WriteLine("Member Suspended");
                return temp;
            }
            else
            {
                return database.getMember(memberID).Split(',');
            }
        }
    }

    //order of return: Service Code, Name, providerID, fee
    public string[] findServiceInfo(string input = "") 
    {
        //return empty string array if not found
        

        //look up service code
        if(input == "")
        {
            //prompt user for service keyword
            Console.WriteLine("Enter service keyword:");
            input = Console.ReadLine();
        }

        return database.lookupService(input).Split(',');
    }
}
