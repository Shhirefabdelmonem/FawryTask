using FawryTask.Models.Customers;
using FawryTask.Models.Cart;
using FawryTask.Models.Checkout;

namespace FawryTask.Services.Checkout
{
    public interface ICheckoutService
    {
        CheckoutResult Checkout(ICustomer customer, ICart cart);
    }
}