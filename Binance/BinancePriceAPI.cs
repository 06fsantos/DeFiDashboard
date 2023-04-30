using System;
using System.Net.Http.Json;
using Binance.Models;

namespace Binance;

public class BinancePriceAPI : IBinancePriceAPI
{
    public async Task<BinancePriceModel> GetPrice(HttpClient client)
    {
        return await client.GetFromJsonAsync<BinancePriceModel>("") ?? new BinancePriceModel();
        //using (HttpClient client = new HttpClient())
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, "request uri");
        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<BinancePriceModel>() ?? new BinancePriceModel();
        //    }

        //    return new BinancePriceModel();
        //    //await client.GetFromJsonAsync<BinancePriceModel>("requestURL") ?? new BinancePriceModel();
        //}
    }
}

