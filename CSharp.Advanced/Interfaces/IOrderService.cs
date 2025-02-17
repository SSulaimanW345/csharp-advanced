using CSharp.Advanced.Enums;
using CSharp.Advanced.Models;
using OneOf;

namespace CSharp.Advanced.Interfaces
{
    public interface IOrderService
    {
        public OneOf<Receipt, PlaceOrderError> PlaceOrder(Order order);

    }
}
