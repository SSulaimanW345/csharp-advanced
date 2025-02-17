using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.HigherOrderFunctions
{
    public static class OrderValidator
    {
        public static bool ValidateOrder(Order order)
        {
            if (order.Items.Count == 0) return false;
            if (order.TotalAmount <= 0) return false;
            if (order.ShippingAddress == null) return false;
            return true;
        }
        public static Func<Order, bool> CreateValidator(string countryCode, decimal minimumOrderValue) 
        {
            var baseValidations = new List<Func<Order, bool>> {
                 o => o.Items.Count > 0,
                o => o.TotalAmount >= minimumOrderValue,
                o => o.ShippingAddress != null
                };
            // Add country-specific validations
            switch (countryCode)
            {
                case "US":
                    baseValidations.Add(order => IsValidUSAddress(order.ShippingAddress));
                    break;
                case "EU":
                    baseValidations.Add(order => IsValidVATNumber(order.VatNumber));
                    break;
            }

            // Combine all validations into a single function
            return CombineValidations(baseValidations);
        }

        private static bool IsValidVATNumber(int vatNumber)
        {
            throw new NotImplementedException();
        }

        private static bool IsValidUSAddress(string shippingAddress)
        {
            return true;
        }

        private static Func<Order, bool> CombineValidations(List<Func<Order, bool>> validations) => (order) => validations.All(v => v(order));
        public class Order
        {
            public List<Item> Items;
            public int TotalAmount;
            public string ShippingAddress;
            public int VatNumber;
        }

        public class Item
        {
        }

    }
    // What if we need:
    // - different validation rules for different countries?
    // - to reuse some validations but not others?
    // - to combine validations differently?

}
