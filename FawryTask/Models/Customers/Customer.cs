namespace FawryTask.Models.Customers
{

    public class Customer : ICustomer
    {
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public Customer(string name, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name cannot be null or empty", nameof(name));
            if (balance < 0)
                throw new ArgumentException("Customer balance cannot be negative", nameof(balance));

            Name = name;
            Balance = balance;
        }

        public bool HasSufficientBalance(decimal amount)
        {
            return Balance >= amount;
        }

        public void DeductBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance");

            Balance -= amount;
        }
    }
} 