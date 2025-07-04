namespace FawryTask.Models
{

    public class ShippableProduct : Product, IShippable
    {
        public double Weight { get; private set; }

        public ShippableProduct(string name, decimal price, int quantity, double weight)
            : base(name, price, quantity)
        {
            if (weight <= 0)
                throw new ArgumentException("Weight must be positive", nameof(weight));
            
            Weight = weight;
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