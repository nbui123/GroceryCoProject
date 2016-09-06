using NUnit.Framework;
using System;
using GroceryCo;

namespace GroceryCoTest
{
	[TestFixture ()]
	public class GroceryItemDataTest
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

		[Test ()]
		public void getTotalCost_multipleItem_noPromo_returnCorrectValue ()
		{
			int quantity = 2;
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			groceryItem.Count = quantity;
			Assert.AreEqual (REGULAR_PRICE * groceryItem.Count, groceryItem.getTotalCost ());
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoReducePrice_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoReducePriceIfMeetQuantity);
			Assert.AreEqual (PROMO_PRICE, groceryItem.getTotalCost ());
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoReducePrice_doesNotMeetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_doesNotMeetPromoQuantity (PromoUtil.PromoReducePriceIfMeetQuantity);
			Assert.AreEqual (REGULAR_PRICE * groceryItem.Count, Math.Round(groceryItem.getTotalCost (), 2));
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoReducePrice_meetPromoQuantityAndMore_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantityAndMore (PromoUtil.PromoReducePriceIfMeetQuantity);
			decimal expectedValue = REGULAR_PRICE * (groceryItem.Count - QUANTITY) + PROMO_PRICE;
			Assert.AreEqual (expectedValue, Math.Round(groceryItem.getTotalCost (), 2));
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoBuyOneGetOneFree_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoBuyOneGetOneFree);
			Assert.AreEqual (REGULAR_PRICE, Math.Round(groceryItem.getTotalCost (), 2));
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoBuyOneGetOneFree_doesNotMeetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_doesNotMeetPromoQuantity (PromoUtil.PromoBuyOneGetOneFree);
			Assert.AreEqual (REGULAR_PRICE * groceryItem.Count, Math.Round(groceryItem.getTotalCost (), 2));
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoBuyOneGetOneHalfOff_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoBuyOneGetOneHalfOff);
			Assert.AreEqual (REGULAR_PRICE + REGULAR_PRICE/2, groceryItem.getTotalCost ());
		}

		[Test ()]
		public void getTotalCost_multipleItem_withPromoBuyOneGetOneHalfOff_meetPromoQuantityAndMore_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantityAndMore (PromoUtil.PromoBuyOneGetOneHalfOff);
			decimal expectedValue = REGULAR_PRICE + REGULAR_PRICE + REGULAR_PRICE/2;
			Assert.AreEqual (expectedValue, groceryItem.getTotalCost ());
		}

		[Test ()]
		public void getTotalSavings_oneItem_noPromo_returnCorrectValue ()
		{
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			Assert.AreEqual (0, groceryItem.getTotalSavings ());
		}

		[Test ()]
		public void getTotalSavings_multipleItem_noPromo_returnCorrectValue ()
		{
			int quantity = 2;
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			groceryItem.Count = quantity;
			Assert.AreEqual (0 * groceryItem.Count, groceryItem.getTotalSavings ());
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoReducePrice_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoReducePriceIfMeetQuantity);
			Assert.AreEqual ((REGULAR_PRICE* QUANTITY-PROMO_PRICE), groceryItem.getTotalSavings ());
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoReducePrice_doesNotMeetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_doesNotMeetPromoQuantity (PromoUtil.PromoReducePriceIfMeetQuantity);
			Assert.AreEqual (0, Math.Round(groceryItem.getTotalSavings (), 2));
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoReducePrice_meetPromoQuantityAndMore_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantityAndMore (PromoUtil.PromoReducePriceIfMeetQuantity);
			decimal expectedValue = (REGULAR_PRICE * QUANTITY - PROMO_PRICE);
			Assert.AreEqual (expectedValue, Math.Round(groceryItem.getTotalSavings (), 2));
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoBuyOneGetOneFree_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoBuyOneGetOneFree);
			Assert.AreEqual (REGULAR_PRICE, Math.Round(groceryItem.getTotalSavings (), 2));
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoBuyOneGetOneFree_doesNotMeetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_doesNotMeetPromoQuantity (PromoUtil.PromoBuyOneGetOneFree);
			Assert.AreEqual (0, Math.Round(groceryItem.getTotalSavings (), 2));
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoBuyOneGetOneHalfOff_meetPromoQuantity_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantity (PromoUtil.PromoBuyOneGetOneHalfOff);
			Assert.AreEqual (REGULAR_PRICE/2, groceryItem.getTotalSavings ());
		}

		[Test ()]
		public void getTotalSavings_multipleItem_withPromoBuyOneGetOneHalfOff_meetPromoQuantityAndMore_returnCorrectValue ()
		{
			GroceryItemData groceryItem = getGroceryItemData_meetPromoQuantityAndMore (PromoUtil.PromoBuyOneGetOneHalfOff);
			Assert.AreEqual (REGULAR_PRICE/2, groceryItem.getTotalSavings ());
		}

		private GroceryItemData getGroceryItemData_doesNotMeetPromoQuantity(int promo){
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			groceryItem.setPromotionInfo (promo, PROMO_PRICE, QUANTITY);
			groceryItem.Count = QUANTITY - 1;
			return groceryItem;
		}

		private GroceryItemData getGroceryItemData_meetPromoQuantity(int promo){
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			groceryItem.setPromotionInfo (promo, PROMO_PRICE, QUANTITY);
			groceryItem.Count = QUANTITY;
			return groceryItem;
		}

		private GroceryItemData getGroceryItemData_meetPromoQuantityAndMore(int promo){
			GroceryItemData groceryItem = new GroceryItemData (PRODUCT, REGULAR_PRICE);
			groceryItem.setPromotionInfo (promo, PROMO_PRICE, QUANTITY);
			groceryItem.Count = QUANTITY + 1;
			return groceryItem;
		}
	}
}

