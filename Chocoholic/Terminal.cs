using System;

public class Terminal
{

    public void run(Database db)
    {
        //feel free to change this, just placeholder

        //get terminal mode
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
        uint id = UInt32.MaxValue;
        while (id == UInt32.MaxValue)
            try { id = UInt32.Parse(Console.ReadLine()); } catch (FormatException e) { Console.WriteLine("Invalid ID, Please Try Again"); }

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
