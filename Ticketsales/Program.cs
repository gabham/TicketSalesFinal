using System; 
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        string pricepath = @"..\..\..\registeredprices.txt";
        string sumpath = @"..\..\..\sumoftickets.txt";

        

        List<Ticket> Tickets = new List<Ticket>();
     
        static void Main()
           {
            TicketBooth p = new TicketBooth();
            p.Start();        
           }

        void Start()
        {
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
            Console.WriteLine("print - prints amount of tickets either by type of ticket or total amount of tickets purchased");
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
                    // Also adds ticket price to a separate text file, this is to later calculate the sum of all tickets using Sum()

                    if (price2 == 25)
                    {
                     
                        StreamWriter child = new StreamWriter(childpath,true);
                        child.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr", ticket.Name, ticket.Age, ticket.Price));
                        child.Close();

                        StreamWriter price = new StreamWriter(pricepath, true);
                        price.WriteLine(String.Format("{0}", ticket.Price));
                        price.Close();
                    }
                    else if (price2 == 75)
                    {
                        
                        StreamWriter senior = new StreamWriter(seniorpath, true);
                        senior.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr", ticket.Name, ticket.Age, ticket.Price));
                        senior.Close();

                        StreamWriter price = new StreamWriter(pricepath, true);
                        price.WriteLine(String.Format("{0}", ticket.Price));
                        price.Close();
                    }
                    else if (price2 == 100)
                    {
                        
                        StreamWriter adult = new StreamWriter(adultpath, true);
                        adult.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr" , ticket.Name, ticket.Age, ticket.Price));
                        adult.Close();

                        StreamWriter price = new StreamWriter(pricepath, true);
                        price.WriteLine(String.Format("{0}", ticket.Price));
                        price.Close();
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

                    for(int i = 0; i < realamount; i++)
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
             Environment.Exit(0); 
          }

         void Print()
         {
            
                Console.WriteLine("\nWhich type of ticket would you like to print? Enter one of these types: (child/senior/adult)");
                
                string typeinput = Console.ReadLine().ToLower();
                                                                           
                        if (typeinput == "child")
                        {
                          try
                          {
                            string childtext = System.IO.File.ReadAllText(childpath);
                            Console.WriteLine("\nDisplaying all tickets in category 'child':");
                            Console.WriteLine("{0}", childtext);
                          }
                          catch
                          {
                           Console.WriteLine("\nNo tickets have been registered in this category");
                          }  
                    
                        }
                        else if(typeinput == "senior")
                        {
                          try
                          {                          
                            string seniortext = System.IO.File.ReadAllText(seniorpath);
                            Console.WriteLine("\nDisplaying all tickets in category 'senior':");
                            Console.WriteLine("{0}", seniortext);
                            return;
                          }
                          catch
                          {                          
                            Console.WriteLine("\nNo tickets have been registered in this category");
                          }
                           
                        }
                        else if(typeinput == "adult")
                        {
                          try
                          {
                              string adulttext = System.IO.File.ReadAllText(adultpath);
                              Console.WriteLine("\nDisplaying all tickets in category 'adult':");
                              Console.WriteLine("{0}", adulttext);
                          }
                          catch
                          {
                            Console.WriteLine("\nNo tickets have been registered in this category");
                          }
                                               
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
            else if(input == "multiple")
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
           
            StreamWriter refund = new StreamWriter(refundpath, true);                                  
                   
                        Console.WriteLine("\nEnter the name of the ticket holder to refund:");
                        string name = Console.ReadLine();

                        for (int i = 0; i < Tickets.Count; i++)
                        {
                            var ticket = Tickets[i];
                            if (ticket.Name == name)
                            {
                                refund.WriteLine(String.Format("Name: {0}, Age: {1}, Price: {2}kr" + " (REFUNDED TICKET)", ticket.Name, ticket.Age, ticket.Price));
                                refund.Close();

                                Tickets.Remove(ticket);



                                Console.WriteLine("\nRefund complete!");
                                Console.WriteLine("Check 'refundarchive.txt' for archived refunds.");
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
                
                for(int i = 0; i < realamount; i++)
                {
                    SingleRefund();
                }

                Console.WriteLine("\nAll " + realamount + " refunds have been registered into the system!");

                return;
            }
            catch
            {
                Console.WriteLine("\nYou used the wrong symbols, enter integers only :)");
            }

        }

        void Archive()
        {
            if (refundpath == null)
            {
                Console.WriteLine("\nThe archive is empty...");
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
            StreamWriter sum = new StreamWriter(sumpath);

            int totalsum = File.ReadAllLines(pricepath).Sum(x => int.Parse(x));

            sum.WriteLine(totalsum + "kr is the current sum of all registered tickets as of" + DateTime.Now.ToString(" HH:mm:ss tt"));
            sum.Close();

            Console.WriteLine("\n" + totalsum + "kr is the sum of all the tickets purchased and registered in the system");
            Console.WriteLine("The total sum of all tickets have been registered in 'sumoftickets.txt' for accounting");
        }

    }

    

    /// Class for the ticket. 
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
