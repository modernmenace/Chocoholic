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
            T_LOOP:
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
                
                if(commandArray.Length < 3)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: find (member/service) ('id')");
                    continue;
                }

                commandArray[1] = commandArray[1].ToLower();

                if (commandArray[1] == "member")
                {
                    if(commandArray.Length<3)
                        commandArray.Append("");
                    
                    string[] memberInfo = findMemberInfo(commandArray[2].ToLower());
                    Console.WriteLine(memberInfo[0]);
                    if (memberInfo[0] == "")
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
                if(commandArray.Length<2)
                {
                    Console.WriteLine("Invalid Syntax\nbill ('member ID')");
                    continue;
                }

                //find member handles all IO
                string[] memberInfo = findMemberInfo(commandArray[1].ToLower());
                Console.WriteLine(memberInfo[0]);
                //order of return: name, balance, status, address, city, state, zipcode, id

                if(memberInfo[0] != "")
                    Console.WriteLine("Member " + memberInfo[0] + " Validated");
                else
                {
                    Console.WriteLine("Member not validated");
                    continue;
                }
                uint memberID = UInt32.Parse(commandArray[1].ToLower());

                //provider enters date of service (no validation)
                Console.WriteLine("Enter date of service: ");
                string serviceDate = Console.ReadLine();

                //provider looks up service for coorisponding service code
                string[] serviceInfo;
                while(true)
                {
                    Console.WriteLine("Enter service keyword: ");
                    string inputKey = Console.ReadLine();
                    if(inputKey == "quit" || inputKey == "exit")
                        goto T_LOOP; //continue back to main terminal loop
                    serviceInfo = findServiceInfo(inputKey);
                    if(serviceInfo[0] != "")
                    {
                        //provider confirms service
                        if(serviceInfo.Length < 4)
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
                            Console.WriteLine("Confirm this is the correct service? (Y/N)");
                            if(Console.ReadLine().ToLower() == "y")
                            {
                                Console.WriteLine("Service confirmed");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Service not confirmed");
                                continue;
                            }
                        }
                    }
                }
                
                //provider adds comments to service
                Console.WriteLine("Enter any comments about the service(optional):");
                string serviceComments = Console.ReadLine();
                
                //provider sends request to database to be logged
                //first display info and ask provider for confimation to send report and log it
                Console.WriteLine("Service Summary:");
                Console.WriteLine("Service code: " + serviceInfo[0]);
                Console.WriteLine("Service name: " + serviceInfo[1]);
                Console.WriteLine("Provider ID: " + serviceInfo[2]);
                Console.WriteLine("Service fee: " + serviceInfo[3]);
                Console.WriteLine("Member ID: " + memberID);
                Console.WriteLine("Member name: " + memberInfo[0]);
                Console.WriteLine("Service date: " + serviceDate);
                Console.WriteLine("Comments: " + serviceComments);
                Console.WriteLine("Confirm this is the correct service report? (Y/N)");
                if(Console.ReadLine().ToLower() != "y")
                {
                    Console.WriteLine("Service report not comfirmed");
                    continue;
                }
                else
                {
                    //push to database
                    Console.WriteLine("Service report confirmed");
                    int fee = 0;
                    try { fee = Int32.Parse(serviceInfo[3]); }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fee is not an integer value!");
                        continue;
                    }
                    database.createServiceReport(memberID, ID,serviceDate, fee);

                    //write record to text file for verification
                    string filename = ID + "_" + serviceDate + "_" + DateTime.Now.ToString("hh:mm tt") + ".txt";
                    try
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\" + filename, true))
                        {
                            //Date and time of record
                            file.WriteLine(DateTime.Now.ToString("F"));
                            //service code
                            file.WriteLine("Service code: " + serviceInfo[0]);
                            //provider id
                            file.WriteLine("Provider iD: " + ID);
                            //service fee
                            file.WriteLine("Service Fee: " + serviceInfo[3]);
                            //member id
                            file.WriteLine("Member ID: " + memberID);
                            //service date
                            file.WriteLine("Service Date: " + serviceDate);
                            //service comments
                            file.WriteLine("Comments:" + serviceComments);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Verification file unable to be recorded");
                        continue;
                    }
                    Console.WriteLine("Service record recorded");
                }
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

    //order of return: name, balance, status, address, city, state, zipcode, id
    public string[] findMemberInfo(string input = "") 
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
        try { memberID = UInt32.Parse(input); }
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
                var a = database.getMember(memberID).Split(',');
                a.Append(input);
                return a;
            }
        }
        string[] defaultS = new string[1];
        defaultS[0] = "";
        return defaultS;
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
