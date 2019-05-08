using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessBears.Library.Abstracts
{
    /// <summary>
    /// Abstract class used as a model for any object representing a product. Contains functionality for
    /// getting/setting names and provides an abstract method for price getting
    /// </summary>
    public abstract class Product
    {
        protected string _name;
        public string Name { get => _name; set => _name = value; }
        public abstract double getPrice();

    }
}
