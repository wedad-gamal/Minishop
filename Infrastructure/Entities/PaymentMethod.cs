namespace Minishop.Infrastructure.Entities
{
    public class PaymentMethod : BaseType
    {
        public List<Cart> Carts { get; set; }
    }
}