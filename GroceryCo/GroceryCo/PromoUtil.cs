using System;

namespace GroceryCo
{
	public class PromoUtil
	{
		public static readonly int PromoReducePrice = 1;
		public static readonly int PromoReducePriceIfMeetQuantity = 2;
		public static readonly int PromoBuyOneGetOneFree = 3;
		public static readonly int PromoBuyOneGetOneHalfOff = 4;
		public static IPromotion getPromotionInto (int promoCode, Decimal regularPrice, Decimal? promoPrice, int? quantityToGetPromoPrice)
		{
			try{
				double percent100 = 1.0;
				double percent50 = 0.5;
				switch (promoCode) {
					case 1: //PromoReducePrice
						return new PromoReducePrice (regularPrice, (Decimal)promoPrice, 1);
					case 2: //PromoReducePriceIfMeetQuantity
						return new PromoReducePrice (regularPrice, (Decimal)promoPrice, (int)quantityToGetPromoPrice);
					case 3: //PromoBuyOneGetOneFree
						return new PromoBuyOneGetOneDiscount (regularPrice, percent100);
					case 4: //PromoBuyOneGetOneHalfOff
						return new PromoBuyOneGetOneDiscount (regularPrice, percent50);
					default:
						return null;
				}  
			} catch(Exception){
				return null;
			}
		}
	}
}

