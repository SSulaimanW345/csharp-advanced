using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.HigherOrderFunctions
{
    public static class EnumerableExtensions
    {
        public static T SingleElseException<T>(this IEnumerable<T> sequence,Func<T,bool> predicate, Func<IEnumerable<T>, Exception> exceptionFactory) 
        {
            var matchedItems = new List<T>();
            foreach (var item in sequence) 
            {
                if (predicate(item)) matchedItems.Add(item);    
            }

            if(matchedItems.Count == 0) { return matchedItems[0]; }
            
            throw exceptionFactory(matchedItems);
        }
    }
}
