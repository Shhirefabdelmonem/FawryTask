namespace FawryTask.Models.Customers
{
    public interface ICustomer
    {
        string Name { get; }
        decimal Balance { get; }
        bool HasSufficientBalance(decimal amount);
        void DeductBalance(decimal amount);
    }
}