using FawryTask.Models.Products;

namespace FawryTask.Models.Cart
{

    public class Cart : ICart
    {
        private readonly List<CartItem> _items = new();

        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
        public bool IsEmpty => _items.Count == 0;
        public decimal Subtotal => _items.Sum(item => item.TotalPrice);

        public void Add(IProduct product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
            if (quantity > product.Quantity)
                throw new InvalidOperationException($"Cannot add {quantity} items. Only {product.Quantity} available.");

            var existingItem = _items.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + quantity;
                if (newQuantity > product.Quantity)
                    throw new InvalidOperationException($"Cannot add {quantity} more items. Only {product.Quantity - existingItem.Quantity} more available.");

                existingItem.Quantity = newQuantity;
            }
            else
            {
                _items.Add(new CartItem(product, quantity));
            }
        }

        public void Remove(IProduct product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _items.RemoveAll(item => item.Product == product);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(IProduct product)
        {
            return _items.Any(item => item.Product == product);
        }
    }
}