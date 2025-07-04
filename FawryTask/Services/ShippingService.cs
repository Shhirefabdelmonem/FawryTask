using FawryTask.Models;

namespace FawryTask.Services
{

    public class ShippingService : IShippingService
    {
        public void ProcessShipment(List<IShippable> shippableItems, List<int> quantities)
        {
            if (shippableItems == null || !shippableItems.Any())
                return;

            if (shippableItems.Count != quantities.Count)
                throw new ArgumentException("Number of items and quantities must match");

            Console.WriteLine("** Shipment notice **");

            double totalWeight = 0;
            for (int i = 0; i < shippableItems.Count; i++)
            {
                var item = shippableItems[i];
                var quantity = quantities[i];
                var itemWeight = item.GetWeight() * quantity;
                totalWeight += itemWeight;

                Console.WriteLine($"{quantity}x {item.GetName()}        {itemWeight}g");
            }

            Console.WriteLine($"Total package weight {totalWeight / 1000:F1}kg");
            Console.WriteLine();
        }
    }
}