using System;

class Program
{
    static void Main(string[] args)
    {
        Database db = new Database();
        db.initialize();
        Terminal t = new Terminal();
        t.run(db);
    }
}
