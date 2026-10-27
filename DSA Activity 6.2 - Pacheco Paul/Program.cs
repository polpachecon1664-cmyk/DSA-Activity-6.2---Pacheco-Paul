using System;
using System.Collections.Generic;

class Program
{
    static Queue<string> deposit = new Queue<string>();
    static Queue<string> withdraw = new Queue<string>();
    static Queue<string> inquire = new Queue<string>();
    static Queue<string> others = new Queue<string>();

    static int served = 0;

    static void Main()
    {
        Console.WriteLine("Welcome to ECO BANK, INC.");

        while (true)
        {
            Console.WriteLine("\nSelect your transaction");
            Console.WriteLine("1 - DEPOSIT");
            Console.WriteLine("2 - WITHDRAW");
            Console.WriteLine("3 - INQUIRE");
            Console.WriteLine("4 - OTHERS");
            Console.WriteLine("10 - COMPLETE DEPOSIT");
            Console.WriteLine("20 - COMPLETE WITHDRAW");
            Console.WriteLine("30 - COMPLETE INQUIRE");
            Console.WriteLine("40 - COMPLETE OTHERS");
            Console.WriteLine("0 - EXIT");
            Console.Write("Enter code: ");

            string code = Console.ReadLine();

            if (code == "0")
                break;

            if (code == "10" || code == "20" ||
                code == "30" || code == "40")
            {
                CompleteTransaction(code);
            }
            else if (code == "1" || code == "2" ||
                     code == "3" || code == "4")
            {
                AddClient(code);
            }
            else
            {
                Console.WriteLine("Invalid code!");
            }

            DisplayQueues();
        }
    }

    static void AddClient(string code)
    {
        string transaction = "";
        Queue<string> selectedQueue = deposit;

        switch (code)
        {
            case "1":
                transaction = "DEPOSIT";
                selectedQueue = deposit;
                break;

            case "2":
                transaction = "WITHDRAW";
                selectedQueue = withdraw;
                break;

            case "3":
                transaction = "INQUIRE";
                selectedQueue = inquire;
                break;

            case "4":
                transaction = "OTHERS";
                selectedQueue = others;
                break;
        }

        Console.WriteLine("\nYou have chosen " + transaction);
        Console.Write("Please enter your account name: ");

        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Account name cannot be empty!");
            return;
        }

        selectedQueue.Enqueue(name);

        Console.WriteLine("You are added to the waiting list!");
    }

    static void CompleteTransaction(string code)
    {
        Queue<string> selectedQueue = deposit;
        string transaction = "";

        switch (code)
        {
            case "10":
                selectedQueue = deposit;
                transaction = "DEPOSIT";
                break;

            case "20":
                selectedQueue = withdraw;
                transaction = "WITHDRAW";
                break;

            case "30":
                selectedQueue = inquire;
                transaction = "INQUIRE";
                break;

            case "40":
                selectedQueue = others;
                transaction = "OTHERS";
                break;
        }

        Console.Write("Enter secret code: ");
        string secret = Console.ReadLine();

        if (secret != "0")
        {
            Console.WriteLine("Incorrect secret code!");
            return;
        }

        if (selectedQueue.Count == 0)
        {
            Console.WriteLine("No clients in " + transaction + " queue.");
            return;
        }

        string name = selectedQueue.Dequeue();
        served++;

        Console.WriteLine("\nTransaction Completed!");
        Console.WriteLine("Client served: " + name);
    }

    static void DisplayQueues()
    {
        Console.WriteLine("\nNOW SERVING!!! NOW SERVING!!!");
        Console.WriteLine("DEP : " + GetServing(deposit));
        Console.WriteLine("WID : " + GetServing(withdraw));
        Console.WriteLine("INQ : " + GetServing(inquire));
        Console.WriteLine("OTH : " + GetServing(others));

        Console.WriteLine("\nWAITING LIST!!! WAITING LIST!!!");
        Console.WriteLine("_______________________________________________________");

        DisplayList("DEPOSIT", deposit);
        DisplayList("WITHDRAWAL", withdraw);
        DisplayList("INQUIRE", inquire);
        DisplayList("OTHERS", others);

        Console.WriteLine("\nNumber of clients served: " + served);
        Console.WriteLine("Number of clients waiting: " +
            (deposit.Count + withdraw.Count +
             inquire.Count + others.Count));
    }

    static string GetServing(Queue<string> queue)
    {
        if (queue.Count == 0)
            return "";

        return queue.Peek();
    }

    static void DisplayList(string title, Queue<string> queue)
    {
        Console.WriteLine("\n" + title +
            " (" + queue.Count + " clients)");

        int number = 1;

        foreach (string name in queue)
        {
            Console.WriteLine(number + ". " + name);
            number++;
        }
    }
}
