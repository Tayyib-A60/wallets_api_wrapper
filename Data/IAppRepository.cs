using wallets_api_wrapper.Models;

namespace wallets_api_wrapper.Data
{
    public interface IAppRepository
    {
        CardDetails GetCardDetails(string cardBin);
    }
}