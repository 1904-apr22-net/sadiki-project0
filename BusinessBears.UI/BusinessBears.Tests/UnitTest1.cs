using BusinessBear.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessBears.Tests
{
    public class LocationStocking
    {
        [Fact]
        public void StandardInput()
        {
            //set-up
            Location testlocation = new Location();

            //function
            Bear bear2 = new Bear();
            Console.WriteLine(bear2.Name);
            bear2.Quantity = 9;
            testlocation.AddProduct(bear2);

            //assert
            Assert.Equal(9, testlocation.Inventory["Bear"].Quantity);

        }
    }

    public class OrderCreation
    {
        [Fact]
        public void StandardOrder()
        {

            //set-up
            Training t1 = new Training("Juggling", 14.99);
            Training t2 = new Training("War Tactics", 49.99);
            Training t3 = new Training("Couples' Therapy", 39.99);
            HashSet<Training> tarray = new HashSet<Training> { t1, t2, t3 };
            Bear bear = new Bear(tarray);
            List<Bear> bears = new List<Bear> { bear };
            Order order = new Order(bears);

            //function
            
        }
    }
}