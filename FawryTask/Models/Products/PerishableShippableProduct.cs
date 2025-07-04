namespace FawryTask.Models.Products
{

    public class PerishableShippableProduct : Product, IPerishable, IShippable
    {
        public DateTime ExpirationDate { get; private set; }
        public double Weight { get; private set; }

        public PerishableShippableProduct(string name, decimal price, int quantity, DateTime expirationDate, double weight)
            : base(name, price, quantity)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be positive", nameof(weight));

            ExpirationDate = expirationDate;
            Weight = weight;
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public override bool IsAvailable()
        {
            return base.IsAvailable() && !IsExpired();
        }

        public string GetName()
        {
            return Name;
        }

        public double GetWeight()
        {
            return Weight;
        }
    }
}