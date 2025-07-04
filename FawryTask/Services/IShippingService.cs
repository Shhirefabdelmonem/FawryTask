using FawryTask.Models;

namespace FawryTask.Services { 

    public interface IShippingService
    {
        void ProcessShipment(List<IShippable> shippableItems, List<int> quantities);
    }
} 