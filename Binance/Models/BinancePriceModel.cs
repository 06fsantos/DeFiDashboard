using System;
namespace Binance.Models;

public class BinancePriceModel
{
	public BinancePriceModel()
	{
		Prices = new List<Prices>();
	}

	public List<Prices> Prices { get; set; }
}

public class Prices
{
	public DateTime Date { get; set; }
	public decimal Price { get; set; }
}

