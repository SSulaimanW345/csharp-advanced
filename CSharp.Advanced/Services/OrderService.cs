using CSharp.Advanced.Enums;
using CSharp.Advanced.Interfaces;
using CSharp.Advanced.Models;
using OneOf;

namespace CSharp.Advanced.Services
{
    public class OrderService:IOrderService
    {
        public OneOf<Receipt, PlaceOrderError> PlaceOrder(Order order) 
        {
            if (order is null)
            {
                return PlaceOrderError.DoesntExist;
            }

            if (order.Cost > order.Payment)
            {
                return PlaceOrderError.InsufficientFunds;
            }
            var receipt = new Receipt();
            return receipt;
        }
    }
}
