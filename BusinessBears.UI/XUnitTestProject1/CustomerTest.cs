using BusinessBears.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessBears.Tests
{
    public class CustomerTests
    {
        Customer customer = new Customer();
        Customer customer2 = new Customer(2, "Amy", "Adams", new DateTime(2008, 3, 1, 7, 0, 0));
        Customer customer3 = new Customer(2, "Amy", "Adams", DateTime.Now);
        [Fact]
        public void OrderLimitReturnsTrueWhenEmpty()
        {
            Assert.True(customer.OrderLimit(DateTime.Now));
            
        }

        [Fact]
        public void OrderLimitReturnsTrueWhenInputTimeSufficientlyLate()
        {
            Assert.True(customer2.OrderLimit(DateTime.Now));

        }

        [Fact]
        public void OrderLimitReturnsFalseWhenInputTimeTooEarly()
        {
            Assert.False(customer3.OrderLimit(DateTime.Now));
        }
    }
        
    
}