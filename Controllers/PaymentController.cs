using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Secure_Payment_Integration.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessPayment(string stripeToken, decimal amount)
        {
            try
            {
                StripeConfiguration.ApiKey = "your_stripe_secret_key";

                var options = new ChargeCreateOptions
                {
                    Amount = (long)(amount * 100), // Convert amount to cents
                    Currency = "usd",
                    Description = "Sample Charge",
                    Source = stripeToken,
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                // Check if the charge was successful
                if (charge.Status == "succeeded")
                {
                    ViewBag.Message = "Payment successful!";
                }
                else
                {
                    ViewBag.Message = "Payment failed.";
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine(ex.Message);
                ViewBag.Message = "An error occurred. Please try again.";
            }

            return View("Index");
        }
    }
}
