namespace OrderSample.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommand
    {
        public decimal Total { get; }

        public CreateOrderCommand(decimal total)
        {
            Total = total;
        }
    }
}
