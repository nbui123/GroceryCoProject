using System;
/*
 * ie. Buy one get one free
 * ie. Buy one get one 50% off
 * % off depends on the variable percentageDiscountOf2ndItem
 * 1 = 100% off
 * 0.5 = 50% off
 */
namespace GroceryCo
{
	public class PromoBuyOneGetOneDiscount : IPromotion
	{
		private readonly int _quantityToGetPromoPrice = 2;
		private decimal _regularPrice;
		private double _percentageDiscountOf2ndItem;

		public PromoBuyOneGetOneDiscount (decimal regularPrice, double percentageDiscountOf2ndItem)
		{
			_regularPrice = regularPrice;
			_percentageDiscountOf2ndItem = percentageDiscountOf2ndItem;
		}

		public Decimal getTotalPriceForQuantityMet() {
			return getPromoPricePerItem() * _quantityToGetPromoPrice;
		}

		public int getQuantityToGetPromoPrice()
		{
			return _quantityToGetPromoPrice; 
		}

		public Decimal getPromoPricePerItem()
		{
			return (_regularPrice * _quantityToGetPromoPrice - (_regularPrice*(decimal)_percentageDiscountOf2ndItem)) /2;
		}

		public Decimal getSavings()
		{
			return (_regularPrice - getPromoPricePerItem()) * _quantityToGetPromoPrice;
		}

		public Boolean isMetPromoRequirements(int amountBrought) {
			if (amountBrought >= _quantityToGetPromoPrice) {
				return true;
			}
			return false;
		}

		public string getPromoDisplayInfo() {
			if (_percentageDiscountOf2ndItem == 1) {
				return string.Format("Buy One Get One free"); 
			}
			return string.Format("Buy One Get One {0} off", _percentageDiscountOf2ndItem.ToString("00%")); 
		}
	}
}

