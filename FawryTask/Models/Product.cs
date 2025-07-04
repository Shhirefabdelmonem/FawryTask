namespace FawryTask.Models
{
    public abstract class Product : IProduct
    {
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public int Quantity { get; set; }

        protected Product(string name, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or empty", nameof(name));
            if (price < 0)
                throw new ArgumentException("Product price cannot be negative", nameof(price));
            if (quantity < 0)
                throw new ArgumentException("Product quantity cannot be negative", nameof(quantity));

            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public virtual bool IsAvailable()
        {
            return Quantity > 0;
        }
    }
}