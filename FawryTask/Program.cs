using FawryTask.Models.Products;
using FawryTask.Models.Customers;
using FawryTask.Models.Cart;
using FawryTask.Models.Checkout;
using FawryTask.Services.Checkout;
using FawryTask.Services.Pricing;
using FawryTask.Services.Shipping;

namespace FawryTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Fawry E-Commerce Checkout System ===\n");

            // Initialize services following Dependency Injection (DIP principle)
            var pricingService = new PricingService();
            var shippingService = new ShippingService();
            var checkoutService = new CheckoutService(pricingService, shippingService);

            // Create sample products
            var cheese = new PerishableShippableProduct("Cheese", 100m, 10, DateTime.Now.AddDays(7), 200); // 200g per unit
            var tv = new ShippableProduct("TV", 500m, 5, 2000); // 2000g per unit  
            var biscuits = new PerishableShippableProduct("Biscuits", 150m, 8, DateTime.Now.AddDays(5), 700); // 700g per unit
            var scratchCard = new SimpleProduct("Mobile Scratch Card", 25m, 20);

            // Create customer with sufficient balance
            var customer = new Customer("Shefo", 2000m);

            Console.WriteLine("=== Test Case 1: Successful Checkout ===");
            // Create cart and add items
            var cart = new Cart();
            cart.Add(cheese, 2);
            cart.Add(biscuits, 1);
            cart.Add(scratchCard, 1);

            Console.WriteLine($"Customer initial balance: ${customer.Balance}");
            Console.WriteLine("Cart contents:");
            foreach (var item in cart.Items)
            {
                Console.WriteLine($"  {item.Quantity}x {item.Product.Name} @ ${item.Product.Price} each");
            }
            Console.WriteLine();

            // Perform checkout
            var result = checkoutService.Checkout(customer, cart);

            if (result.IsSuccess)
            {
                Console.WriteLine($"Customer balance after payment: ${customer.Balance}");
            }
            else
            {
                Console.WriteLine($"Checkout failed: {result.ErrorMessage}");
            }

            Console.WriteLine("\n" + new string('=', 50));

            // Test Case 2: Cart with expired product
            Console.WriteLine("\n=== Test Case 2: Expired Product ===");
            var expiredCheese = new PerishableShippableProduct("Expired Cheese", 100m, 5, DateTime.Now.AddDays(-1), 200);
            var cart2 = new Cart();
            cart2.Add(expiredCheese, 1);

            var customer2 = new Customer("Jane Smith", 500m);
            var result2 = checkoutService.Checkout(customer2, cart2);
            Console.WriteLine($"Result: {(result2.IsSuccess ? "Success" : result2.ErrorMessage)}");

            // Test Case 3: Insufficient balance
            Console.WriteLine("\n=== Test Case 3: Insufficient Balance ===");
            var cart3 = new Cart();
            cart3.Add(tv, 3); // 3 TVs at $500 each = $1500 + shipping

            var poorCustomer = new Customer("Poor Customer", 100m);
            var result3 = checkoutService.Checkout(poorCustomer, cart3);
            Console.WriteLine($"Result: {(result3.IsSuccess ? "Success" : result3.ErrorMessage)}");

            // Test Case 4: Empty cart
            Console.WriteLine("\n=== Test Case 4: Empty Cart ===");
            var emptyCart = new Cart();
            var customer4 = new Customer("Customer 4", 1000m);
            var result4 = checkoutService.Checkout(customer4, emptyCart);
            Console.WriteLine($"Result: {(result4.IsSuccess ? "Success" : result4.ErrorMessage)}");

            // Test Case 5: Out of stock
            Console.WriteLine("\n=== Test Case 5: Out of Stock ===");
            var limitedTv = new ShippableProduct("Limited TV", 500m, 2, 2000);
            var cart5 = new Cart();
            cart5.Add(limitedTv, 5); // Try to add more than available

            var customer5 = new Customer("Customer 5", 5000m);
            try
            {
                var result5 = checkoutService.Checkout(customer5, cart5);
                Console.WriteLine($"Result: {(result5.IsSuccess ? "Success" : result5.ErrorMessage)}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error adding to cart: {ex.Message}");
            }


        }
    }
}
