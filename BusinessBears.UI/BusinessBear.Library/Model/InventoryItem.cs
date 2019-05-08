using BusinessBears.Library.Abstracts;
using System;
using System.Collections.Generic;

namespace BusinessBears.Library
{
    /// <summary>
    /// A holding class used to integrate Product subclasses with Location.Inventory
    /// </summary>
    public class InventoryItem
    {
        private Product product;
        private int _quantity;
        public int Quantity { get => _quantity; set => _quantity = value; }
        public Product Product { get => product; }

        public InventoryItem(Product p, int i)
        {
            this.product = p;
            this._quantity = i;
        }
    }
}
