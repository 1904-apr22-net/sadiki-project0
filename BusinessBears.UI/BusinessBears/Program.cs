using BusinessBears.DataAccess.Entities;
using BusinessBears.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Customer = BusinessBears.Library.Customer;
using Location = BusinessBears.Library.Location;
using CustomerDB = BusinessBears.DataAccess.Entities.Customer;
using LocationDB = BusinessBears.DataAccess.Entities.Location;
using System.Linq;
using Product = BusinessBears.DataAccess.Entities.Product;

namespace BusinessBears
{
    class Program
    {
        static void Main(string[] args)
        {

            BBearContext dbContext = CreateDbContext();
            bool running = true;
            Console.WriteLine("Business Bears: We Sell Bears, Train Bears, And Nothing Else. NO GIRAFFES.");
            
            while (running)
            {
                Console.WriteLine();
                Console.Write("Choose a submenu, or \"q\" to quit: ");
                Console.WriteLine();
                Console.WriteLine("r:\tCreate & process new order.");
                Console.WriteLine("a:\tView order history.");
                
                var input = Console.ReadLine();
                if (input == "r")
                {
                    bool customerhandling = true;
                    int cID=0;
                    while (customerhandling)
                    {
                        Console.WriteLine("Please input the first or last name of the customer");
                        string cname = Console.ReadLine();
                        List<Customer> lc = RetrieveCustomers(dbContext, cname);
                        if (lc.Count() <= 0)
                        {
                            Console.WriteLine("No results for that name. Please try again.");
                        }
                        else {
                            foreach (Customer customer in lc)
                            {
                                customer.CustomerDetails();
                                
                            }
                            Console.WriteLine();
                            Console.WriteLine("Please input the Customer ID from the provided list.");
                            string s = Console.ReadLine();
                            
                            while (!Int32.TryParse(s, out cID) || lc.Count(x => x.Id.Equals(cID)) == 0)
                            {
                                Console.WriteLine("Input must be a number from the provided list");

                                s = Console.ReadLine();
                            }
                            if (!lc.Single(x => x.Id == cID).OrderLimit(DateTime.Now))
                            {
                                Console.WriteLine("This customer has bought bears in the last two hours. Please wait or try another customer.");
                            }
                            else
                            {
                                customerhandling = !customerhandling;
                            }
                            
                        }
                    }


                    Console.WriteLine("How many bears will be required for this order?");
                 
                     
                    string s2 = Console.ReadLine();
                    int bearcountO;
                    
                    while (!Int32.TryParse(s2, out bearcountO) && bearcountO > 0)
                    {
                        Console.WriteLine("Not a valid number, try again.");

                        s2 = Console.ReadLine();
                    }

                    
                    int counter = 0;
                    HashSet<Training> trainingArray = new HashSet<Training>();
                    List<Bear> bearList = new List<Bear>();
                    while (counter < bearcountO)
                    {
                        Console.WriteLine($"Select the training modules needed for Bear #{counter + 1}. Insert 'f' when finished");
                        Console.WriteLine("Note: Only one type of training per bear. Extra modules will be ignored.");
                        Console.WriteLine($"r:\t Juggling - $19.99");
                        Console.WriteLine($"a:\t Fighting - $24.99");
                        Console.WriteLine($"s:\t Tax Evasion - $45.99");
                        Console.WriteLine($"l:\t Marriage Counselling - $69.99");
                        Console.WriteLine($"t:\t Divininty - $199.99");
                        Console.WriteLine($"p:\t C#/.Net  - $2.99");

                        string trainingSelect = Console.ReadLine();
                        if (trainingSelect == "r")
                        {
                            Training t = new Training("Juggling", 19.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "a")
                        {
                            Training t = new Training("Fighting", 24.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "s")
                        {
                            Training t = new Training("Tax Evasion", 45.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "l")
                        {
                            Training t = new Training("Marriage Counselling", 69.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "t")
                        {
                            Training t = new Training("Divinity", 199.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "p")
                        {
                            Training t = new Training("C#/.Net", 2.99);
                            Console.WriteLine("Training module added!");
                            trainingArray.Add(t);
                        }
                        if (trainingSelect == "f")
                        {
                            Bear bear = new Bear(trainingArray);
                            Console.WriteLine($"Training assigned to Bear #{counter+1}");
                            bearList.Add(bear);
                         
                            trainingArray = new HashSet<Training>();
                            counter++;
                     
                        }
                        else
                        {
                            Console.WriteLine("Please use one of the listed inputs");
                        }
                        
                    }

                    Order currentOrder = new Order(bearList);
                    int q = bearList[0].upgrades.Count();


                    currentOrder.CustomerID = cID;

                    bool locationhandling = false;
                    while (!locationhandling)
                    {

                        Console.WriteLine("Please input one of the following location IDs");
                        PrintLocations(dbContext);
                        int lID = Convert.ToInt32(Console.ReadLine());



                        Location orderLocation = RetrieveLocation(dbContext, lID);
                        currentOrder = orderLocation.ProcessOrder(currentOrder);
                        if (currentOrder.Price == null)
                        {
                            Console.WriteLine("Please try another location.");
                        }
                        else
                        {
                            AddOrder(dbContext, currentOrder);
                            UpdateLocation(dbContext, currentOrder);
                            UpdateCustomer(dbContext, currentOrder);
                            locationhandling = !locationhandling;
                        }
                    }
                }
                else if (input == "a")
                {
                    Console.WriteLine("r:\tView a location's order history.");
                    Console.WriteLine("a:\tView a customer's order history.");
                    var inputL = Console.ReadLine();
                    if (inputL == "a")
                    {
                        Console.WriteLine("Please input the first or last name of the customer");
                        string cname = Console.ReadLine();
                        foreach (Customer customer in RetrieveCustomers(dbContext, cname))
                        {
                        };
                        
                    }
                    Console.WriteLine("Input the target ID:");
                    int ordersearchid = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                        Console.WriteLine("Sort by:");
                        Console.WriteLine("r:\tCheapest");
                        Console.WriteLine("a:\tMost Expensive");
                        Console.WriteLine("s:\tEarliest");
                        Console.WriteLine("t:\tLatest");
                        var inputL2 = Console.ReadLine();
                    if (inputL == "r")
                    {
                        if (inputL2 == "r")
                        {
                            PrintOrdersByLocation(dbContext, ordersearchid, "price");
                        }
                        if (inputL2 == "a")
                        {
                            PrintOrdersByLocation(dbContext, ordersearchid, "price-r");
                        }
                        if (inputL2 == "s")
                        {
                            PrintOrdersByLocation(dbContext, ordersearchid, "time");
                        }
                        if (inputL2 == "t")
                        {
                            PrintOrdersByLocation(dbContext, ordersearchid, "time-r");
                        }
                    }
                    if (inputL == "a")
                    {
                        if (inputL2 == "r")
                        {
                            PrintOrdersByCustomer(dbContext, ordersearchid, "price");
                        }
                        if (inputL2 == "a")
                        {
                            PrintOrdersByCustomer(dbContext, ordersearchid, "price-r");
                        }
                        if (inputL2 == "s")
                        {
                            PrintOrdersByCustomer(dbContext, ordersearchid, "time");
                        }
                        if (inputL2 == "t")
                        {
                            PrintOrdersByCustomer(dbContext, ordersearchid, "time-r");
                        }

                    }
                    
                }
                else if (input == "q")
                {
                    Console.WriteLine("Exiting Business Bears...");
                    running = false;
                    
                }
                else
                {
                    Console.WriteLine("Please input a valid value\n");

                }
            }

            
        }

        private static List<Customer> RetrieveCustomers(BBearContext dbContext, string name)
        {
            List<Customer> lc = new List<Customer>();
            Console.WriteLine("Matching Customers:");
            foreach (CustomerDB customer in dbContext.Customer.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).Include(x => x.Orders))
            {
                Customer newcustomer = new Customer(customer.CustomerId, customer.FirstName, customer.LastName, customer.LastOrder);
   
                lc.Add(newcustomer);
            }
            return lc;
        }
        private static void PrintLocations(BBearContext dbContext)
        {
            foreach (LocationDB location in dbContext.Location)
            {
                Console.WriteLine(location.LocationId);
            }
        }


        private static void PrintOrdersByLocation(BBearContext dbContext, int location_id, string type)
        {
           
            if (type == "time")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.LocationId == location_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderBy(x => x.CreatedAt))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "time-r")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.LocationId == location_id).Include(x => x.Customer)
                    .Include(x=>x.SoldBears).ThenInclude(y=>y.SoldTraining).OrderByDescending(x => x.CreatedAt))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "price")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.LocationId == location_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderBy(x => x.PriceTag))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "price-r")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.LocationId == location_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderByDescending(x => x.PriceTag))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else
            {
                Console.WriteLine("Invalid type. Please try again");
            }
            Console.WriteLine();
        }

        private static void PrintOrdersByCustomer(BBearContext dbContext, int customer_id, string type)
        {
            
            if (type == "time")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.CustomerId == customer_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderBy(x => x.CreatedAt))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "time-r")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.CustomerId == customer_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderByDescending(x => x.CreatedAt))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "price")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.CustomerId == customer_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderBy(x => x.PriceTag))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else if (type == "price-r")
            {
                foreach (Orders order in dbContext.Orders.Where(x => x.CustomerId == customer_id).Include(x => x.Customer)
                    .Include(x => x.SoldBears).ThenInclude(y => y.SoldTraining).OrderByDescending(x => x.PriceTag))
                {
                    PrintOrders(order, dbContext);
                }
            }
            else
            {
                Console.WriteLine("Invalid type. Please try again");
            }
            Console.WriteLine();
        }

        private static void PrintOrders(Orders order, BBearContext dbContext)
        {
            Console.WriteLine($"Customer {order.CustomerId}: {order.Customer.FirstName} {order.Customer.LastName}");
            Console.WriteLine($"Location: {order.LocationId}");
            int bearcounter = 1;
            foreach (SoldBears bear in order.SoldBears)
            {
                
                Console.WriteLine($"Bear #{bearcounter}:");
                foreach (SoldTraining training in dbContext.SoldTraining.Include(x => x.Product).Where(x => x.BearId == bear.BearId))
                //foreach (SoldTraining training in bear.SoldTraining)
                {
                    Console.WriteLine(training.Product.ProductName);
                }

                bearcounter++;
            }
            Console.WriteLine($"Price Paid: ${order.PriceTag}");

        }

        private static Location RetrieveLocation(BBearContext dbContext, int location_id)
        {
            LocationDB location = dbContext.Location.Include(x => x.Inventory).ThenInclude(y => y.Product).SingleOrDefault(x => x.LocationId == location_id);
            Location newlocation = new Location();
            newlocation.ID = location_id;
            newlocation.Inventory["Bear"].Quantity = location.Inventory.Where(x => x.Product.ProductName == "Bear").First().Quantity;
            foreach (Inventory inv in location.Inventory.Where(x => x.Product.ProductName != "Bear"))
            {
                Training p = new Training(inv.Product.ProductName, Convert.ToDouble(inv.Product.DefPrice));
                newlocation.AddProduct(p, inv.Quantity);
            }

            return newlocation;
        }

        private static void UpdateLocation(BBearContext dbContext, Order order)
        { 
            dbContext.Location.Find(order.LocationID).Inventory.Where(x => x.Product.ProductName == "Bear").First().Quantity--;
            foreach (Bear bear in order.bears)
            {
                foreach (Training t in bear.upgrades)
                {
                    dbContext.Location.Find(order.LocationID).Inventory.Where(x => x.Product.ProductName == t.Name).First().Quantity--;
                }
            }

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                dbContext.Location.Find(order.LocationID).Inventory.Where(x => x.Product.ProductName == "Bear").First().Quantity++;
                foreach (Bear bear in order.bears)
                {
                    foreach (Training t in bear.upgrades)
                    {
                        dbContext.Location.Find(order.LocationID).Inventory.Where(x => x.Product.ProductName == t.Name).First().Quantity++;
                    }
                }
                
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddOrder(BBearContext dbContext, Order order)
        { 

            var newOrders = new Orders();
            newOrders.PriceTag = Convert.ToDecimal(order.Price);
            newOrders.LocationId = order.LocationID;
            newOrders.CustomerId = order.CustomerID;
            newOrders.CreatedAt = DateTime.Now;
            foreach (Bear bear in order.bears)
            {
                SoldBears b = new SoldBears();
                HashSet<SoldTraining> hst = new HashSet<SoldTraining>();
                foreach (Training training in bear.upgrades)
                {

                    SoldTraining st = new SoldTraining();
                    Product p = dbContext.Product.Where(x => x.ProductName == training.Name).First();
                    st.ProductId = p.ProductId;
                    hst.Add(st);
                }
                b.SoldTraining = hst;
                newOrders.SoldBears.Add(b);
            }



            dbContext.Add(newOrders);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                dbContext.Orders.Remove(newOrders);
                Console.WriteLine(ex.Message);
            }
        }

        private static void UpdateCustomer (BBearContext dbContext, Order order)
        {
            dbContext.Customer.Find(order.CustomerID).DefLocationId = order.LocationID;
            dbContext.Customer.Find(order.CustomerID).LastOrder = DateTime.Now;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                dbContext.Customer.Find(order.CustomerID).DefLocationId = null;
                dbContext.Customer.Find(order.CustomerID).LastOrder = null;
                Console.WriteLine(ex.Message);
            }
        }
        private static BBearContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BBearContext>();
            optionsBuilder
                .UseSqlServer(DataAccess.SecretConfiguration.ConnectionString);
                //.UseLoggerFactory(AppLoggerFactory);

            return new BBearContext(optionsBuilder.Options);
        }
    }
}
