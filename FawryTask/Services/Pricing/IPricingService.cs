using FawryTask.Models.Products;

namespace FawryTask.Services.Pricing
{

    public interface IPricingService
    {
        decimal CalculateShippingFees(IEnumerable<IShippable> shippableItems, IEnumerable<int> quantities);
    }
}