using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#pragma warning disable IDE0044

namespace Ticketsales
{

    class TicketBooth
    {
        string childpath = @"..\..\..\childrentickets.txt";
        string adultpath = @"..\..\..\adulttickets.txt";
        string seniorpath = @"..\..\..\seniortickets.txt";
        string refundpath = @"..\..\..\refundarchive.txt";
        string sumpath = @"..\..\..\sumoftickets.txt";
        
        List<Ticket> Tickets = new List<Ticket>();

        static void Main()
        {
            TicketBooth p = new TicketBooth();
            p.Start();
        }

        void Start()
        {
            Wipe();

            {
                Console.WriteLine("Welcome to the ticket booth! \n");
                Console.WriteLine("You are currently running the test version of our ticketing software to be installed for future concert events.");
                Console.WriteLine("Test out the program to your heart's content and make sure that everything is in order!");
                Console.WriteLine("For help purchasing tickets and everything else, type 'help'");
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter command:");
                string input = Console.ReadLine().ToLower();

                if (input == "help")
                {
                    Help();
                }
                else if (input == "single")
                {
                    Single();
                }
                else if (input == "quit")
                {
                    Quit();
                }
                else if (input == "family")
                {
                    Family();
                }
                else if (input == "refund")
                {
                    Refund();
                }
                else if (input == "print")
                {
                    Print();
                }
                else if (input == "sum")
                {
                    Sum();
                }
                else
                {
                    Console.WriteLine("\nInvalid Command!");
                }

            }
        }

        // METHODS LOCATED BELOW THIS COMMENT

        void Help()
        {
            Console.WriteLine();
            Console.WriteLine("single - purchasing a ticket for one person");
            Console.WriteLine("family - purchasing tickets for a family");
            Console.WriteLine("print - prints amount of tickets by ticket type, you can also count the amount of tickets sold");
            Console.WriteLine("refund - refund one or more tickets, you also have the ability to check the archive of refunded tickets");
            Console.WriteLine("sum - calculates the sum of all purchased tickets and registers it in a separate file");
            Console.WriteLine("help - displays commands");
            Console.WriteLine("quit - exits the ticket booth");
        }

        void Single()
        {

            Console.WriteLine("\nEnter name of ticket holder:");
            string name = Console.ReadLine().ToLower();

            if (name.Contains(" "))
            {
                Console.WriteLine("\nA name can't contain spaces.");
                return;
            }
            else if (name.Any(Char.IsDigit))
            {
                Console.WriteLine("\nA name can't contain numbers.");
                return;
            }

            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("Enter age of ticket holder:");
                string age = Console.ReadLine();

                try
                {

                    int realage = int.Parse(age);

                    int price2 = 0;

                    if (realage < 18)
                    {
                        price2 = 25;
                    }
                    else if (realage > 65)
                    {
                        price2 = 75;
                    }
                    else if (realage >= 18 && realage <= 65)
                    {
                        price2 = 100;
                    }

                    Ticket ticket = new Ticket(name, realage, price2);

                    Tickets.Add(ticket);

                    // Adding a ticket to a .txt file, depending on if it's a child ticket, adult ticket or senior ticket
        
                    if (price2 == 25)
                    {
                        StreamWriter child = new StreamWriter(childpath, true);
                        child.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr", ticket.Name, ticket.Age, ticket.Price));
                        child.Close();                      
                    }
                    else if (price2 == 75)
                    {
                        StreamWriter senior = new StreamWriter(seniorpath, true);
                        senior.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr", ticket.Name, ticket.Age, ticket.Price));
                        senior.Close();                    
                    }
                    else if (price2 == 100)
                    {
                        StreamWriter adult = new StreamWriter(adultpath, true);
                        adult.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr", ticket.Name, ticket.Age, ticket.Price));
                        adult.Close();
                    }

                    Console.WriteLine("\nPurchase complete!");
                    return;
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter numbers, not letters.\nTry again :)");
                }
            }
        }

        void Family()
        {
            while (true)
            {
                Console.WriteLine("\nHow many family members are you purchasing tickets for?");
                string amount = Console.ReadLine();

                try
                {
                    int realamount = int.Parse(amount);

                    for (int i = 0; i < realamount; i++)
                    {
                        Single();
                    }

                    Console.WriteLine("\nAll " + realamount + " purchases have been completed, have a nice day!");
                    return;
                }
                catch
                {
                    Console.WriteLine("\nYou're supposed to type in numbers, not other symbols.\nTry again");
                }
            }

        }

        void Quit()
        {
            Sum();
            Environment.Exit(0);
        }

        void Print()
        {

            Console.WriteLine("\nWhich type of ticket would you like to print? Enter one of these types: (child/senior/adult)");
            Console.WriteLine("If you want to count all the tickets you've purchased, type 'count'");

            string input = Console.ReadLine().ToLower();

            if (input == "child")
            {
                try
                {
                    if (new FileInfo(childpath).Length == 0)
                    {
                        Console.WriteLine("\nNo tickets have been sold in this category");
                    }
                    else
                    {
                        string childtext = System.IO.File.ReadAllText(childpath);
                        Console.WriteLine("\nDisplaying all tickets in category 'child':");
                        Console.WriteLine("{0}", childtext);
                    }
                    return;
                }
                catch
                {
                    Console.WriteLine("\nNo tickets have been registered in this category");
                }

            }
            else if (input == "senior")
            {
                try
                {
                    if (new FileInfo(seniorpath).Length == 0)
                    {
                        Console.WriteLine("\nNo tickets have been sold in this category");
                    }
                    else
                    {
                        string seniortext = System.IO.File.ReadAllText(seniorpath);
                        Console.WriteLine("\nDisplaying all tickets in category 'senior':");
                        Console.WriteLine("{0}", seniortext);
                    }
                    return;
                }
                catch
                {
                    Console.WriteLine("\nNo tickets have been registered in this category");
                }

            }
            else if (input == "adult")
            {
                try
                {
                    if (new FileInfo(adultpath).Length == 0)
                    {
                        Console.WriteLine("\nNo tickets have been sold in this category");
                    }
                    else
                    {
                        string adulttext = System.IO.File.ReadAllText(adultpath);
                        Console.WriteLine("\nDisplaying all tickets in category 'adult':");
                        Console.WriteLine("{0}", adulttext);
                    }
                    return;
                }
                catch
                {
                    Console.WriteLine("\nNo tickets have been registered in this category");
                }

            }
            else if (input == "count")
            {
                Console.WriteLine("\nTotal numbers of tickets purchased: " + Tickets.Count + " tickets");
            }

        }


        void Refund()
        {
            Console.WriteLine("\nDo you want to refund a single purchase or multiple purchases? (single/multiple)");
            Console.WriteLine("If you want to check all refunded tickets, type 'archive'");
            string input = Console.ReadLine();

            if (input == "single")
            {
                SingleRefund();
            }
            else if (input == "multiple")
            {
                FamilyRefund();
            }
            else if (input == "archive")
            {
                Archive();
            }
        }


        void SingleRefund()
        {

            using (StreamWriter refund = new StreamWriter(refundpath, true))
            {
                Console.WriteLine("\nEnter the name of the ticket holder to refund:");
                string name = Console.ReadLine().ToLower();

                for (int i = 0; i < Tickets.Count; i++)
                {
                    var ticket = Tickets[i];

                    if (ticket.Name == name)
                    {
                        refund.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr" + " (REFUNDED TICKET)", ticket.Name, ticket.Age, ticket.Price));
                        refund.Close();

                        Tickets.Remove(ticket);

                        string tempPath = "";

                        if (ticket.Age >= 18 && ticket.Age <= 65)
                        {
                            tempPath = adultpath;      
                        }
                        else if (ticket.Age < 18)
                        {
                            tempPath = childpath;
                        }
                        else
                        {
                            tempPath = seniorpath;
                        }

                        //This code below removes the refunded ticket from the category it belonged to by replacing it with a blank line in the .txt file

                        string old;
                        string n = "";

                        StreamReader sr = File.OpenText(tempPath);
                        while ((old = sr.ReadLine()) != null)
                        {
                            try
                            {
                                if (!old.Contains(name))
                                {
                                    n += old + Environment.NewLine;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("\nSomething went wrong.");
                            }
                        }
                        sr.Close();

                        File.WriteAllText(tempPath, n);

                        Console.WriteLine("\nRefund has been completed.");
                    }
                }
            }
        }

       
        void FamilyRefund()
        {
            Console.WriteLine("\nEnter the amount of tickets you want to refund:");
            string amount = Console.ReadLine();

            try
            {
                int realamount = int.Parse(amount);

                for (int i = 0; i < realamount; i++)
                {
                    SingleRefund();
                }

                Console.WriteLine("\nAll " + realamount + " refunds have been registered into the system!");
                return;
            }
            catch 
            {
                Console.WriteLine("\nYou didn't enter any numbers");
            }                        
        }

        void Archive()
        {
            if (!File.Exists(refundpath))
            {
                Console.WriteLine("Archive does not exist because no refunds have been registered into the system.");
                return;
            }

            if (new FileInfo(refundpath).Length == 0)
            {
                Console.WriteLine("\nThe archive seems to be empty at the moment.");         
            }
            else
            {
                string text = System.IO.File.ReadAllText(refundpath);
                Console.WriteLine("\nDisplaying all refunded tickets:");
                Console.WriteLine("{0}", text);
            }
        }

        void Sum()
        {
            using (StreamWriter totalsum = new StreamWriter(sumpath))
            {
                int sum = 0;

                if (Tickets.Count == 0)
                {
                    Console.WriteLine("\nNo tickets have been purchased.");
                }
                else
                {
                    foreach (Ticket ticket in Tickets)
                    {
                        sum += ticket.Price;
                    }

                    totalsum.WriteLine("The total sum of all registered tickets is: " + sum + "kr");
                    Console.WriteLine("\nThe sum of all tickets sold is: " + sum + "kr");
                    Console.WriteLine("The total sum has been registered in a separate file for accounting");
                }                
            }
        }

        // Wipe() används för att radera all text på alla filer vid uppstart, för att kunna börja på ny kula
        // Detta är för att det inte gick att genomföra återköp på gamla biljetter som man köpt när man stängde programmet och öppnade upp programmet igen
        // Skulle velat ha kommit på något så att man kunde spara de gamla köpen och refunda dem men det verkar som att det inte var ett krav
        void Wipe()
        {
            System.IO.File.WriteAllText(childpath, string.Empty) ;
                                        
            System.IO.File.WriteAllText(adultpath, string.Empty);
                                      
            System.IO.File.WriteAllText(seniorpath, string.Empty);
                             
            System.IO.File.WriteAllText(refundpath, string.Empty);

            System.IO.File.WriteAllText(sumpath, string.Empty);                        
        }

    }
 
    class Ticket
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int Price { get; private set; }

        public Ticket(string name, int age, int price)
        {
            this.Name = name;
            this.Age = age;
            this.Price = price;
        }

    }

}
