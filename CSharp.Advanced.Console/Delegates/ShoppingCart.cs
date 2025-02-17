using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Delegates
{   

    public class ShoppingCart
    {
        public List<Product> Items { get; set; } = new List<Product>();

        public delegate void MentionDiscount(decimal subTotal);

        public decimal GenerateTotal(MentionDiscount mentionDiscount, Func<List<Product>,decimal,decimal> calculatediscountedTotal,
            Action<string> mentionDiscounting)
        {   
            decimal subTotal = Items.Sum(X=> X.Price);
            mentionDiscount(subTotal);
            mentionDiscounting("We are applying your discount");
            return calculatediscountedTotal(Items,subTotal);
        }
    }

    
}
