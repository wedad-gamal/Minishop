namespace Minishop.Infrastructure.Entities
{
    public class Cart : BaseType
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalPrice { get; set; }
        public double DeliveryFees { get; set; }
        public double Discount { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
