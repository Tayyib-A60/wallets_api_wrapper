using Swashbuckle.AspNetCore.Filters;
using wallets_api_wrapper.Models;

namespace wallets_api_wrapper.SwaggerExamples
{
        public class GetCardDetailsResponse : IExamplesProvider<CardDetails>
    {
        CardDetails IExamplesProvider<CardDetails>.GetExamples()
        {
            return new CardDetails
            {
                Brand = "Visa",
                Scheme = "Visa/Dankhort",
                Type = "credit",
                Prepaid = false,
                Country = new Country{
                    Name = "Nigeria",
                    Currency = "NGN"
                },
                Bank = new Bank {
                    Name = "Access bank",
                    Url = "accessbank.com",
                    Phone = "+2348092829282",
                    City = "Lagos"
                }
            };
        }
    }
}