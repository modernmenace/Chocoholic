using System;
public class Terminal
{
    public Terminal()
    {
        //All Command Line Code here
    }


    public void run()
    {
        while (true)
        {
            //feel free to change this, just placeholder
            Console.WriteLine("Please Enter a Command");
            string command = Console.ReadLine();

            if (command.ToLower() == "exit") break;
        }
    }
}
