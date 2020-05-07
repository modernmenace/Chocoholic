using System;


public class Operator
{
    private uint oID;
    private Database database;

    public Operator(uint id, Database db)
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
            //create command
            if (commandArray[0].ToLower() == "create")
            {
                if (commandArray.Length < 2)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: create (member/provider/service)");
                    continue;
                }
                if (commandArray[1].ToLower() == "member")
                {
                    //create member
                    Console.WriteLine("Please Enter Member Name:");
                    var name = Console.ReadLine();
                    Console.WriteLine("Please Enter Member Address:");
                    var address = Console.ReadLine();
                    Console.WriteLine("Please Enter Member City:");
                    var city = Console.ReadLine();
                    Console.WriteLine("Please Enter Member State:");
                    var state = Console.ReadLine();
                    Console.WriteLine("Please Enter Member Zipcode:");
                    int zip = 0;
                    try { zip = Int32.Parse(Console.ReadLine()); }
                    catch (Exception e)
                    {
                        Console.WriteLine("Zipcode is not a number, please start over");
                        continue;
                    }
                    database.createActor(Database.ActorType.Member, name, address, city, state, zip);
                }
                else if (commandArray[1].ToLower() == "provider")
                {
                    //create provider
                    Console.WriteLine("Please Enter Provider Name:");
                    var name = Console.ReadLine();
                    Console.WriteLine("Please Enter Provider Address:");
                    var address = Console.ReadLine();
                    Console.WriteLine("Please Enter Provider City:");
                    var city = Console.ReadLine();
                    Console.WriteLine("Please Enter Provider State:");
                    var state = Console.ReadLine();
                    Console.WriteLine("Please Enter Provider Zipcode:");
                    int zip = 0;
                    try { zip = Int32.Parse(Console.ReadLine()); }
                    catch (Exception e)
                    {
                        Console.WriteLine("Zipcode is not a number, please start over");
                        continue;
                    }
                    database.createActor(Database.ActorType.Provider, name, address, city, state, zip);
                }
                else if (commandArray[1].ToLower() == "service")
                {
                    //create service
                    Console.WriteLine("Please Enter a Name for the Service:");
                    var name = Console.ReadLine();
                    Console.WriteLine("Please Enter a Service Code:");
                    var serviceCode = Console.ReadLine();
                    Console.WriteLine("Please Enter the ID of the Corresponding Provider:");
                    var pID = Console.ReadLine();
                    Console.WriteLine("Please Enter the Fee for the Service:");
                    var fee = Console.ReadLine();

                    try
                    {
                        database.createService(UInt32.Parse(serviceCode), name, UInt32.Parse(pID), Int32.Parse(fee));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: service code, provider ID, or fee is not an integer");
                        continue;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: create (member/provider/service)");
                    continue;
                }
            }

            //remove command
            if (commandArray[0].ToLower() == "remove" || commandArray[0].ToLower() == "delete")
            {
                if (commandArray.Length < 2)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: " + commandArray[0] + " (member/provider/service)");
                    continue;
                }

                if (commandArray[1].ToLower() == "member")
                {
                    //remove member
                    Console.WriteLine("Please enter the ID of the member:");
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

                    if (!database.actorExists(mId, Database.ActorType.Member))
                    {
                        Console.WriteLine("Member does not exist with ID '" + mId + "'!");
                        continue;
                    }
                    database.deleteActor(Database.ActorType.Member, mId);
                }
                else if (commandArray[1].ToLower() == "provider")
                {
                    //remove provider
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

                    if (!database.actorExists(mId, Database.ActorType.Provider))
                    {
                        Console.WriteLine("Provider does not exist with ID '" + mId + "'!");
                        continue;
                    }
                    database.deleteActor(Database.ActorType.Provider, mId);
                }
                else if (commandArray[1].ToLower() == "service")
                {
                    //remove service
                    Console.WriteLine("Please enter the service code:");
                    uint mId = 0;
                    try
                    {
                        mId = UInt32.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("service code is not of valid format");
                        continue;
                    }

                    database.deleteService(mId);
                }
                else
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: " + commandArray[0] + " (member/provider/service)");
                    continue;
                }
            }

            //update command
            if (commandArray[0].ToLower() == "update")
            {
                if (commandArray.Length < 3)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: update (member/provider) ('id') (name/address/city/state/zipcode/status/balance) ('new field value')");
                    continue;
                }

                if (commandArray[1].ToLower() == "member")
                {
                    //verify ID
                    uint mId = 0;
                    try { mId = UInt32.Parse(commandArray[2]); }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid Member ID '" + commandArray[2] + "'!");
                        continue;
                    }
                    if (!database.actorExists(mId, Database.ActorType.Member))
                    {
                        Console.WriteLine("Invalid Member ID '" + commandArray[2] + "'!");
                        continue;
                    }

                    //build new field value string
                    string newFieldValue = "";
                    for (int i = 4; i < commandArray.Length; i++)
                    {
                        Console.WriteLine(i);
                        newFieldValue += commandArray[i];
                        newFieldValue += ' ';
                    }
                    Console.WriteLine(newFieldValue);

                    //update string fields
                    if (commandArray[3].ToLower() == "name") database.updateName(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "address") database.updateAddress(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "city") database.updateCity(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "state") database.updateState(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "zipcode")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer value for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateZipcode(Database.ActorType.Member, mId, newInt);
                    }
                    else if (commandArray[3].ToLower() == "status")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer value for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateStatus(Database.ActorType.Member, mId, newInt);
                    }
                    else if (commandArray[3].ToLower() == "balance")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer vaule for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateBalance(Database.ActorType.Member, mId, newInt);
                    }
                    else
                    {
                        Console.WriteLine("Field '" + commandArray[3] + "' not found!\nAcceptable Fields: name/address/city/state/zipcode/status/balance");
                        continue;
                    }
                }
                else if (commandArray[1].ToLower() == "provider")
                {
                    //verify ID
                    uint mId = 0;
                    try { mId = UInt32.Parse(commandArray[2]); }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid Provider ID '" + commandArray[2] + "'!");
                        continue;
                    }
                    if (!database.actorExists(mId, Database.ActorType.Provider))
                    {
                        Console.WriteLine("Invalid Provider ID '" + commandArray[2] + "'!");
                        continue;
                    }

                    //build new field value string
                    string newFieldValue = "";
                    for (int i = 4; i < commandArray.Length; i++)
                    {
                        newFieldValue.Insert(newFieldValue.Length, commandArray[i]);
                        newFieldValue.Insert(newFieldValue.Length, " ");
                    }

                    //update string fields
                    if (commandArray[3].ToLower() == "name") database.updateName(Database.ActorType.Provider, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "address") database.updateAddress(Database.ActorType.Provider, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "city") database.updateCity(Database.ActorType.Provider, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "state") database.updateState(Database.ActorType.Provider, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "zipcode")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer value for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateZipcode(Database.ActorType.Provider, mId, newInt);
                    }
                    else if (commandArray[3].ToLower() == "status")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer value for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateStatus(Database.ActorType.Provider, mId, newInt);
                    }
                    else if (commandArray[3].ToLower() == "balance")
                    {
                        int newInt = 0;
                        try { Int32.Parse(commandArray[4]); }
                        catch (Exception e)
                        {
                            Console.WriteLine("New integer value for field '" + commandArray[3].ToLower() + "' is not an integer");
                            continue;
                        }
                        database.updateBalance(Database.ActorType.Provider, mId, newInt);
                    }
                    else
                    {
                        Console.WriteLine("Field '" + commandArray[3] + "' not found!\nAcceptable Fields: name/address/city/state/zipcode/status/balance");
                        continue;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: update (member/provider) (name/address/city/state/zipcode/status/balance)");
                    continue;
                }
            }

            //exit command
            if (command.ToLower().Contains("exit"))
            {
                System.Environment.Exit(0);
            }
        }
    }

    public uint ID
    {
        get { return oID; }
        set { oID = value; }
    }
}
