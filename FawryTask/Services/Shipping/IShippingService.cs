using FawryTask.Models.Products;

namespace FawryTask.Services.Shipping
{

    public interface IShippingService
    {
        void ProcessShipment(List<IShippable> shippableItems, List<int> quantities);
    }
}