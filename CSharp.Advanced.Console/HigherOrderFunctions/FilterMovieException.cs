using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.HigherOrderFunctions
{   

    public class FilterMovieException : Exception
    {
        public FilterMovieException() { }
        public FilterMovieException(string message)
            : base(message)
        {
        }
        public FilterMovieException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
