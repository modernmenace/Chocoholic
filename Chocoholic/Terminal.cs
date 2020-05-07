using System;

public class Terminal
{
    Database database;

    public void run(Database db)
    {
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
        {
            if (!database.actorExists(id, Database.ActorType.Operator))
            {
                Console.WriteLine("Operator not found with id '" + id + "'!");
                run(db);
            }
            Operator op = new Operator(id, database);
            op.terminal();
        }
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
    //createServiceReport, getMemberBalance, getProvider, providerCount, actorExists functions available in database
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

    //TODO: Implement Manager Functionality
    //getServiceReport, serviceReportCount functions available in database
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
