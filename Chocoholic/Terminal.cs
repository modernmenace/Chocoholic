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
        while (true)
        {
            Console.WriteLine("Please Enter ID Number:");
            uint id = UInt32.MaxValue;
            try { id = UInt32.Parse(Console.ReadLine()); } catch (FormatException e) { Console.WriteLine("Invalid ID, Please Try Again"); continue; }

            if (userMode == Database.ActorType.Member)
            {
                if (!database.actorExists(id, Database.ActorType.Member))
                {
                    Console.WriteLine("Member not found with id '" + id + "'!");
                    run(db);
                }
                Member op = new Member(id, database);
                op.terminal();
            }
            else if (userMode == Database.ActorType.Provider)
            {
                if (!database.actorExists(id, Database.ActorType.Provider))
                {
                    Console.WriteLine("Provider not found with id '" + id + "'!");
                    run(db);
                }
                Provider op = new Provider(id, database);
                op.terminal();
            }
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
            {
                if (!database.actorExists(id, Database.ActorType.Manager))
                {
                    Console.WriteLine("Manager not found with id '" + id + "'!");
                    run(db);
                }
                Manager op = new Manager(id, database);
                op.terminal();
            }
        }
    }
}
