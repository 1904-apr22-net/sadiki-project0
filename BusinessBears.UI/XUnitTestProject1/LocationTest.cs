using BusinessBears.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessBears.Tests
{
    public class LocationTest
    {
        [Fact]
        public void StandardInput()
        {
            //set-up
            Location testlocation = new Location();

            //function
            Training t2 = new Training("Walnut Crushing", 27.99);
            testlocation.AddProduct(t2, 9);

            //assert
            Assert.Equal(9, testlocation.Inventory["Walnut Crushing"].Quantity);

        }
    

 
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



        [Fact]
        public void StandardOrderProcessing()
        {

            //set-up
            Training t1 = new Training("Juggling", 14.99);
            Training t2 = new Training("War Tactics", 49.99);
            Training t3 = new Training("Couples' Therapy", 39.99);
            HashSet<Training> tarray = new HashSet<Training> { t1, t2, t3 };
            Bear bear = new Bear(tarray);
            List<Bear> bears = new List<Bear> { bear };
            Order order = new Order(bears);
            Location testlocation = new Location();

            //function
            Order neworder = testlocation.ProcessOrder(order);

        }
    }
}