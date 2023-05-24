﻿namespace Minishop.Infrastructure.Entities
{
    public class CartItem : BaseType
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }

        public int? CartId { get; set; }
        public Cart Cart { get; set; }

    }
}
