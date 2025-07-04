namespace FawryTask.Models
{

    public interface ICart
    {
        IReadOnlyList<CartItem> Items { get; }
        bool IsEmpty { get; }
        decimal Subtotal { get; }
        
        void Add(IProduct product, int quantity);
        void Remove(IProduct product);
        void Clear();
        bool Contains(IProduct product);
    }
} 