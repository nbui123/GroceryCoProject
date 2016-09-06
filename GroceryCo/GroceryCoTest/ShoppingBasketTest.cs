using NUnit.Framework;
using System;
using GroceryCo;

namespace GroceryCoTest
{
	[TestFixture ()]
	public class ShoppingBasketTest
	{
		private static readonly string ITEM_NAME = "apple";
		[Test ()]
		public void addItem_newItem_returnExpectedResult ()
		{
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (getGroceryItemData (ITEM_NAME));
			Assert.AreEqual (1, shoppingBasket.getShoppingBasket().Count);
			Assert.AreEqual (ITEM_NAME, shoppingBasket.getShoppingBasket () [0].Name);
		}

		[Test ()]
		public void addItem_sameItemAdded_returnExpectedResult ()
		{
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (getGroceryItemData (ITEM_NAME));
			shoppingBasket.addItem (getGroceryItemData (ITEM_NAME));
			Assert.AreEqual (1, shoppingBasket.getShoppingBasket().Count);
			Assert.AreEqual (ITEM_NAME, shoppingBasket.getShoppingBasket () [0].Name);
		}

		[Test ()]
		public void addItem_newItemAdded_returnExpectedResult ()
		{
			string newItem = "banana";
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (getGroceryItemData (ITEM_NAME));
			shoppingBasket.addItem (getGroceryItemData (newItem));
			Assert.AreEqual (2, shoppingBasket.getShoppingBasket().Count);
			Assert.AreEqual (ITEM_NAME, shoppingBasket.getShoppingBasket () [0].Name);
			Assert.AreEqual (newItem, shoppingBasket.getShoppingBasket () [1].Name);
		}

		[Test ()]
		public void calculateFinalBill_oneItem_returnExpectedResult  ()
		{
			decimal price = 1.50m;
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (new GroceryItemData (ITEM_NAME, price));
			Assert.AreEqual (price, shoppingBasket.calculateFinalBill ());
		}

		[Test ()]
		public void calculateFinalBill_multipleItem_returnExpectedResult  ()
		{
			decimal price1 = 1.50m;
			decimal price2 = 2.50m;
			string newItem = "banana";
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (new GroceryItemData (ITEM_NAME, price1));
			shoppingBasket.addItem (new GroceryItemData (newItem, price2));
			Assert.AreEqual (price1+price2, shoppingBasket.calculateFinalBill ());
		}

		[Test ()]
		public void calculateTotalSavings_oneItem_returnExpectedResult  ()
		{
			decimal price = 1.50m;
			decimal promoPrice = 1.00m;
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (getGroceryItemData(ITEM_NAME, price, promoPrice));
			Assert.AreEqual (price-promoPrice, shoppingBasket.calculateTotalSavings ());
		}

		[Test ()]
		public void calculateTotalSavings_multipleItem_returnExpectedResult  ()
		{
			decimal price = 1.50m;
			decimal promoPrice = 1.00m;
			string newItem = "banana";
			ShoppingBasket shoppingBasket = new ShoppingBasket ();
			shoppingBasket.addItem (getGroceryItemData(ITEM_NAME, price, promoPrice));
			shoppingBasket.addItem (getGroceryItemData(newItem, price, promoPrice));
			Assert.AreEqual ((price-promoPrice)*2, shoppingBasket.calculateTotalSavings ());
		}

		private GroceryItemData getGroceryItemData(string item) {
			GroceryItemData groceryItemData = new GroceryItemData (item, 1.50m);
			return groceryItemData;
		}

		private GroceryItemData getGroceryItemData(string item, decimal regularPrice, decimal promoPrice) {
			GroceryItemData groceryItemData = new GroceryItemData (item, regularPrice);
			groceryItemData.setPromotionInfo (PromoUtil.PromoReducePrice, promoPrice, 1);
			return groceryItemData;
		}
	}
}

