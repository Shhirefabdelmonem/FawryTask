using FawryTask.Models.Products;

namespace FawryTask.Services.Pricing
{

    public class PricingService : IPricingService
    {
        private const decimal ShippingRatePerKg = 30m; // $30 per kg

        public decimal CalculateShippingFees(IEnumerable<IShippable> shippableItems, IEnumerable<int> quantities)
        {
            if (shippableItems == null || !shippableItems.Any())
                return 0;

            var itemsList = shippableItems.ToList();
            var quantitiesList = quantities.ToList();

            if (itemsList.Count != quantitiesList.Count)
                throw new ArgumentException("Number of items and quantities must match");

            double totalWeight = 0;
            for (int i = 0; i < itemsList.Count; i++)
            {
                totalWeight += itemsList[i].GetWeight() * quantitiesList[i];
            }

            var weightInKg = (decimal)(totalWeight / 1000);
            return weightInKg * ShippingRatePerKg;
        }
    }
}