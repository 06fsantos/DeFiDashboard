using Binance.Models;

namespace Binance
{
    public interface IBinancePriceAPI
    {
        Task<BinancePriceModel> GetPrice(HttpClient client);
    }
}