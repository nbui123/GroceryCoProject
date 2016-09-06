using NUnit.Framework;
using System;
using GroceryCo;

namespace GroceryCoTest
{
	[TestFixture ()]
	public class PromoReducePrice
	{
		private static readonly decimal REGULAR_PRICE = 3.01m;
		private static readonly decimal PROMO_PRICE = 2.50m;
		private static readonly string PRODUCT = "apple";
		private static readonly int QUANTITY = 2;

		[Test ()]
		public void getTotalCost_oneItem_noPromo_returnCorrectValue ()
		{
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			Assert.AreEqual (REGULAR_PRICE, groceryItem.getTotalCost ());
		}
	}
}

