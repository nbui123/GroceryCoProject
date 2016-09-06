using System;
using System.Collections.Generic;

namespace GroceryCo
{
	class MainClass
	{
		private static Dictionary<String, GroceryItemData> nameToGroceryItemDataMap = new Dictionary<String, GroceryItemData> ();

		public static void Main (string[] args)
		{
			string[] file = System.IO.File.ReadAllLines("../../../../PricingInfo");
			nameToGroceryItemDataMap = ReadPricingFileUtil.getNameToGroceryItemDataMap (file);

			string[] lines =  readValidFileName ("Shopping list file name:");
			ShoppingBasket shoppingBasket = createShoppingBasket (lines);
			printToConsole (shoppingBasket);
		}

		private static ShoppingBasket createShoppingBasket(string[] items) {
			ShoppingBasket shoppingBasket = new ShoppingBasket(); 
			foreach (string item in items)
			{
				if (!string.IsNullOrWhiteSpace (item)) {
					string caseInsentiveItem = item.ToLower ();
					if (nameToGroceryItemDataMap.ContainsKey (caseInsentiveItem)) {
						GroceryItemData groceryItem = nameToGroceryItemDataMap [caseInsentiveItem];
						if (groceryItem != null) {
							shoppingBasket.addItem (groceryItem);
						}
					} else {
						Console.WriteLine ("error - no data for {0}", caseInsentiveItem);
					}
				}
			}
			return shoppingBasket;
		}

		private static string[] readValidFileName(String output) {
			string[] file;
			while (true) {
				try{
					Console.WriteLine (output);
					file = System.IO.File.ReadAllLines("../../../../"+ Console.ReadLine());
					break;
				} catch(System.IO.FileNotFoundException e){
					Console.WriteLine (e.Message);
				}
			}
			return file;
		}

		private static void printToConsole(ShoppingBasket shoppingBasket) {
			Console.WriteLine("\nReceipt ");
			foreach (GroceryItemData item in shoppingBasket.getShoppingBasket())
			{
				int amount = item.Count;
				while (amount > 0 ){
					if (item.isOnPromotion && item.PromotionInfo.isMetPromoRequirements (amount)) {
						IPromotion promoInfo = item.PromotionInfo;
						displayPromoInfoToConsole (item, promoInfo);
						amount -= promoInfo.getQuantityToGetPromoPrice ();
					} else if (amount > 1) {
						displayMultipleRegularPricedItem (item);
						amount -= item.Count;
					} else {
						Console.WriteLine (item.Name + "\t" + MoneyDisplayUtil.formatMoneyDisplay(item.RegularPrice));
						amount -= 1;
					}
				}
			}
			Console.WriteLine ("--------------------------");
			Console.WriteLine ("Total Savings = " + MoneyDisplayUtil.formatMoneyDisplay (shoppingBasket.calculateTotalSavings()));
			Console.WriteLine ("Total Due = " + MoneyDisplayUtil.formatMoneyDisplay (shoppingBasket.calculateFinalBill()));
		}

		private static void displayPromoInfoToConsole(GroceryItemData item, IPromotion promoInfo){
			if (promoInfo.getQuantityToGetPromoPrice () == 1) {
				Console.WriteLine (item.Name + " \t" + MoneyDisplayUtil.formatMoneyDisplay(promoInfo.getTotalPriceForQuantityMet ()));
			} else {
				Console.WriteLine (item.Name + " (X" + promoInfo.getQuantityToGetPromoPrice () + ") " + MoneyDisplayUtil.formatMoneyDisplay(promoInfo.getTotalPriceForQuantityMet ()));
			}
			Console.WriteLine ("\t"+ item.PromotionInfo.getPromoDisplayInfo ());
			Console.WriteLine ("\tsavings " + MoneyDisplayUtil.formatMoneyDisplay (item.PromotionInfo.getSavings()));
		}

		private static void displayMultipleRegularPricedItem(GroceryItemData item){
			Console.WriteLine (item.Name + " (X" + item.Count + ") " + MoneyDisplayUtil.formatMoneyDisplay (item.RegularPrice * item.Count));
			Console.WriteLine ("\t @ " + MoneyDisplayUtil.formatMoneyDisplay (item.RegularPrice) + " each");

		}
	}
}
