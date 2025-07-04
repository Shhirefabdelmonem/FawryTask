namespace FawryTask.Models
{

    public class CheckoutResult
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal ShippingFees { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal CustomerBalanceAfterPayment { get; private set; }
        public List<IShippable> ShippableItems { get; private set; }

        private CheckoutResult(bool isSuccess, string errorMessage = "", decimal subtotal = 0,
            decimal shippingFees = 0, decimal totalAmount = 0, decimal customerBalanceAfterPayment = 0,
            List<IShippable>? shippableItems = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Subtotal = subtotal;
            ShippingFees = shippingFees;
            TotalAmount = totalAmount;
            CustomerBalanceAfterPayment = customerBalanceAfterPayment;
            ShippableItems = shippableItems ?? new List<IShippable>();
        }

        public static CheckoutResult Success(decimal subtotal, decimal shippingFees,
            decimal customerBalanceAfterPayment, List<IShippable> shippableItems)
        {
            return new CheckoutResult(true, "", subtotal, shippingFees,
                subtotal + shippingFees, customerBalanceAfterPayment, shippableItems);
        }

        public static CheckoutResult Failure(string errorMessage)
        {
            return new CheckoutResult(false, errorMessage);
        }
    }
}