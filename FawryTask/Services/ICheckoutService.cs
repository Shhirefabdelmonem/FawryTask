using FawryTask.Models;

namespace FawryTask.Services
{
    public interface ICheckoutService
    {
        CheckoutResult Checkout(ICustomer customer, ICart cart);
    }
}