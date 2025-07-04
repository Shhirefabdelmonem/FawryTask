using FawryTask.Models.Products;
using FawryTask.Models.Customers;
using FawryTask.Models.Cart;
using FawryTask.Models.Checkout;
using FawryTask.Services.Pricing;
using FawryTask.Services.Shipping;

namespace FawryTask.Services.Checkout
{

    public class CheckoutService : ICheckoutService
    {
        private readonly IPricingService _pricingService;
        private readonly IShippingService _shippingService;

        public CheckoutService(IPricingService pricingService, IShippingService shippingService)
        {
            _pricingService = pricingService ?? throw new ArgumentNullException(nameof(pricingService));
            _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
        }

        public CheckoutResult Checkout(ICustomer customer, ICart cart)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            // Validate cart is not empty
            if (cart.IsEmpty)
                return CheckoutResult.Failure("Cart is empty");

            // Validate all products are available and not expired
            foreach (var item in cart.Items)
            {
                if (!item.Product.IsAvailable())
                {
                    if (item.Product is IPerishable perishable && perishable.IsExpired())
                        return CheckoutResult.Failure($"Product '{item.Product.Name}' has expired");
                    else
                        return CheckoutResult.Failure($"Product '{item.Product.Name}' is out of stock");
                }

                if (item.Quantity > item.Product.Quantity)
                    return CheckoutResult.Failure($"Insufficient stock for '{item.Product.Name}'. Available: {item.Product.Quantity}, Requested: {item.Quantity}");
            }

            var subtotal = cart.Subtotal;

            var shippableItems = new List<IShippable>();
            var shippableQuantities = new List<int>();

            foreach (var item in cart.Items)
            {
                if (item.Product is IShippable shippable)
                {
                    shippableItems.Add(shippable);
                    shippableQuantities.Add(item.Quantity);
                }
            }

            var shippingFees = _pricingService.CalculateShippingFees(shippableItems, shippableQuantities);
            var totalAmount = subtotal + shippingFees;

            if (!customer.HasSufficientBalance(totalAmount))
                return CheckoutResult.Failure("Insufficient customer balance");

            customer.DeductBalance(totalAmount);

            foreach (var item in cart.Items)
            {
                item.Product.Quantity -= item.Quantity;
            }

            if (shippableItems.Any())
                _shippingService.ProcessShipment(shippableItems, shippableQuantities);

            DisplayCheckoutReceipt(cart.Items, subtotal, shippingFees, totalAmount);

            return CheckoutResult.Success(subtotal, shippingFees, customer.Balance, shippableItems);
        }

        private void DisplayCheckoutReceipt(IReadOnlyList<CartItem> items, decimal subtotal, decimal shippingFees, decimal totalAmount)
        {
            Console.WriteLine("** Checkout receipt **");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Quantity}x {item.Product.Name}        {item.TotalPrice}");
            }

            Console.WriteLine("----------------------");
            Console.WriteLine($"Subtotal         {subtotal}");
            Console.WriteLine($"Shipping         {shippingFees}");
            Console.WriteLine($"Amount           {totalAmount}");
        }
    }
}