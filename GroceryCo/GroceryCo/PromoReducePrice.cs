using System;

/*
 * ie. Reduce price for item from $2 to $1 when quantityToGetPromotionalPrice=1
 * ie. buy 3 for $2 when quantityToGetPromotionalPrice=3
 * 
 */

namespace GroceryCo
{
	public class PromoReducePrice : IPromotion
	{
		private decimal _regularPrice;
		private decimal _totalSalePriceForQuantityMet;
		private int _quantityToGetPromoPrice;

		public PromoReducePrice (decimal regularPrice, decimal totalSalePriceForQuantityMet, int quantityToGetPromotionalPrice)
		{
			_regularPrice = regularPrice;	
			_totalSalePriceForQuantityMet = totalSalePriceForQuantityMet;
			_quantityToGetPromoPrice = quantityToGetPromotionalPrice;
		}

		public Decimal getTotalPriceForQuantityMet() {
			return getPromoPricePerItem() *_quantityToGetPromoPrice;
		}

		public int getQuantityToGetPromoPrice()
		{
			return _quantityToGetPromoPrice; 
		}

		public Decimal getPromoPricePerItem()
		{
			return _totalSalePriceForQuantityMet / (decimal)_quantityToGetPromoPrice;
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
			if (_quantityToGetPromoPrice > 1) {
				return string.Format("Reduce price \n\tbuy {0} for {1} \n\tregular {2} each", 
					_quantityToGetPromoPrice, 
					MoneyDisplayUtil.formatMoneyDisplay(_totalSalePriceForQuantityMet),
					MoneyDisplayUtil.formatMoneyDisplay(_regularPrice)); 
			}
			return string.Format("Reduce price \n\twas @ {0}", MoneyDisplayUtil.formatMoneyDisplay(_regularPrice)); 
		}
	}
}

