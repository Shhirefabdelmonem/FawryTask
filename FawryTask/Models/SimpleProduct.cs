namespace FawryTask.Models
{
    public class SimpleProduct : Product
    {
        public SimpleProduct(string name, decimal price, int quantity)
            : base(name, price, quantity)
        {
        }
    }
}