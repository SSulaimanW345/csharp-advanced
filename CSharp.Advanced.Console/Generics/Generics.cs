using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Generics introduces the concept of type parameters to .NET.
 * Generics make it possible to design classes and methods that defer the specification of one or more type parameters until 
 * you use the class or method in your code. For example, by using a generic type parameter T, you can write a single class 
 * that other client code can use without incurring the cost or risk of runtime casts or boxing operations
 *  The type parameter T is used in several locations where a concrete type would ordinarily be used to indicate the type of the item stored in the list:

As the type of a method parameter in the AddHead method.
As the return type of the Data property in the nested Node class.
As the type of the private member data in the nested class.
 */


namespace CSharp.Advanced.Console.Generics
{

    // where T : IComparable
    // where T : Product
    // where T : struct
    // where T : class
    // where T : new()

    public class Utilities
    {
        public T Max<T>(T a, T b) where T : IComparable // a non-generic class can have a generic method
        {
            return a.CompareTo(b) > 0 ? a: b;
        }
    }

    public class Utilities<T> where T : IComparable // shift to class level
    {
        public T Max(T a, T b) 
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }

    public class DiscountCalculator<TProduct> where TProduct : Product
    {
        public float CalculateDiscount(TProduct product) 
        {
            return product.Price;
        }
    }

    public class Nullable<T> where T : struct // value type
    {
        private object _value;

        public Nullable()
        {
        }

        public Nullable(T value)
        {
            _value = value;
        }

        public bool HasValue
        {
            get { return _value != null; }
        }

        public T GetValueOrDefault()
        {
            if (HasValue)
                return (T)_value;

            return default(T);
        }
    }

    public class Product
    {
        public float Price;
    }
}
