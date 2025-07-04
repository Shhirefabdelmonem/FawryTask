using FawryTask.Models;

namespace FawryTask.Services
{

    public interface IPricingService
    {
        decimal CalculateShippingFees(IEnumerable<IShippable> shippableItems, IEnumerable<int> quantities);
    }
} 