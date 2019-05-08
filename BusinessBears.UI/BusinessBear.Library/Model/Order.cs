using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessBears.Library
{
    public class Order
    {
        public int Id { get; set; }
        string customer_name;
        public string Customer_Name { get => customer_name; set => customer_name = value; }
        int customer_id;
        int location_id;
        double? _price;
        public double? Price { get => _price; set => _price = value; }
        public int LocationID { get => location_id; set => location_id = value; }
        public int CustomerID { get => customer_id; set => customer_id = value; }
        public List<Bear> bears;
        private DateTime _ordertime;
        public DateTime Ordertime { get => _ordertime; set => _ordertime = value; }
        void OrderDetails()
        {
            int beartracker = 1;
            Console.WriteLine($"Customer: {0} {1} \n Location: {2}", customer_name, location_id);
            foreach (Bear bear in this.bears)
            {
                

                Console.WriteLine($"Bear #{0} had following upgrades:", beartracker);
                foreach (Training training in bear.upgrades)
                {
                    Console.WriteLine(training.Name);
                }
                beartracker++;
            }
            Console.WriteLine($"Total Cost: {1} \n Order Time: {0}", _ordertime, _price);
        }
        public Order(List<Bear> bears)
        {
            this.bears = bears;
        }

    }
}
