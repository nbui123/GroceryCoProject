using System;

namespace GroceryCo
{
	public interface IPromotion
	{
		Decimal getPromoPricePerItem();
		int getQuantityToGetPromoPrice();
		Decimal getSavings();
		Decimal getTotalPriceForQuantityMet();
		Boolean isMetPromoRequirements(int amountBrought); 
		string getPromoDisplayInfo(); 
	}
}

