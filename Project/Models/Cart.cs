using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class CartLine
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {

        private List<CartLine> linecollection = new List<CartLine>();
        public void AddItem(Product pro, int quantity = 1)
        {
            CartLine line = linecollection.Where(p => p.product.Id == pro.Id).FirstOrDefault();

            if (line == null)
            {
                linecollection.Add(new CartLine { product = pro, Quantity = quantity });
            }
            else
                line.Quantity += quantity;
        }

        public void RemoveLine(Product pro)
        {
            linecollection.RemoveAll(p => p.product.Id == pro.Id);
        }

        public double ComputeTotalValue()
        {
            return linecollection.Sum(p => p.product.price * p.Quantity);
        }

        public void Clear()
        {
            linecollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return linecollection; }
        }
    }
}