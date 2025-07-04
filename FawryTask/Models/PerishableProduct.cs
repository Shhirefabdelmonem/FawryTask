namespace FawryTask.Models
{
    public class PerishableProduct : Product, IPerishable
    {
        public DateTime ExpirationDate { get; private set; }

        public PerishableProduct(string name, decimal price, int quantity, DateTime expirationDate)
            : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public override bool IsAvailable()
        {
            return base.IsAvailable() && !IsExpired();
        }
    }
} 