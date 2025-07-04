namespace FawryTask.Models
{

    public class CartItem
    {
        public IProduct Product { get; private set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;

        public CartItem(IProduct product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));

            Product = product;
            Quantity = quantity;
        }
    }
} 