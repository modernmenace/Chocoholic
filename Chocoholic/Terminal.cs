using System;

public class Terminal
{
    Database database;

    public void run(Database db)
    {
        //feel free to change this, just placeholder

        //get terminal mode
        this.database = db;
        Database.ActorType userMode = Database.ActorType.None;
        while (true)
        {
            Console.WriteLine("Please select a terminal mode:\nMember - 1\nProvider - 2\nManager - 3\nOperator - 4\n");
            string command = Console.ReadLine();
            if      (command.Contains("1")) userMode = Database.ActorType.Member;
            else if (command.Contains("2")) userMode = Database.ActorType.Provider;
            else if (command.Contains("3")) userMode = Database.ActorType.Manager;
            else if (command.Contains("4")) userMode = Database.ActorType.Operator;

            if (userMode != Database.ActorType.None) break;
        }

        //get user id (trusting user that ID exists for now)
        Console.WriteLine("Please Enter ID Number:");
        uint id = UInt32.MaxValue;
        while (id == UInt32.MaxValue)
            try { id = UInt32.Parse(Console.ReadLine()); } catch (FormatException e) { Console.WriteLine("Invalid ID, Please Try Again"); }
        Console.WriteLine("Success! Ready for Input!\n"); 

        if (userMode == Database.ActorType.Member)
            memberTerminal(id);
        else if (userMode == Database.ActorType.Provider)
            providerTerminal(id);
        else if (userMode == Database.ActorType.Operator)
            operatorTerminal(id);
        else if (userMode == Database.ActorType.Manager)
            managerTerminal(id);

        Console.WriteLine(id);
    }

    //TODO: Implement Member Functionality
    private void memberTerminal(uint id)
    {
        while (true)
        {
            string command = Console.ReadLine();


            if (command.ToLower().Contains("exit"))
            {
                System.Environment.Exit(0);
            }
        }
    }

    //TODO: Implement Provider Functionality
    //createServiceReport function available in database
    private void providerTerminal(uint id)
    {
        while (true)
        {
            string command = Console.ReadLine();


            if (command.ToLower().Contains("exit"))
            {
                System.Environment.Exit(0);
            }
        }
    }

    //TODO: Implement Operator Functionality
    private void operatorTerminal(uint id)
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
                    Console.WriteLine("Invalid Syntax\nProper Usage: create (member/provider)");
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
                    try { zip = Int32.Parse(Console.ReadLine()); } catch (Exception e)
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
                else
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: create (member/provider)");
                    continue;
                }
            }

            //remove command
            if (commandArray[0].ToLower() == "remove" || commandArray[0].ToLower() == "delete")
            {
                if (commandArray.Length < 2)
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: " + commandArray[0] + " (member/provider)");
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
                    } catch (Exception e)
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
                else
                {
                    Console.WriteLine("Invalid Syntax\nProper Usage: " + commandArray[0] + " (member/provider)");
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
                    for(int i = 4; i < commandArray.Length; i++)
                    {
                        newFieldValue.Insert(newFieldValue.Length, commandArray[i]);
                        newFieldValue.Insert(newFieldValue.Length, " ");
                    }

                    //update string fields
                    if (commandArray[3].ToLower() == "name")    database.updateName(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "address") database.updateAddress(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "city")    database.updateCity(Database.ActorType.Member, mId, newFieldValue);
                    else if (commandArray[3].ToLower() == "state")   database.updateState(Database.ActorType.Member, mId, newFieldValue);
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

    //TODO: Implement Manager Functionality
    private void managerTerminal(uint id)
    {
        while (true)
        {
            string command = Console.ReadLine();


            if (command.ToLower().Contains("exit"))
            {
                System.Environment.Exit(0);
            }
        }
    }
}
