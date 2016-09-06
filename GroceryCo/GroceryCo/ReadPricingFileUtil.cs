using System;
using System.Collections.Generic;

namespace GroceryCo
{
	public class ReadPricingFileUtil
	{
		public static readonly Char seperator1 = ';';
		private static readonly Char seperator2 = '=';

		public static Dictionary<String, GroceryItemData> getNameToGroceryItemDataMap(string[] file)
		{
			Dictionary<String, GroceryItemData> nameToGroceryItemDataMap = new Dictionary<String, GroceryItemData> ();
			foreach (string line in file) {
				//line ie. item=banana; regularPrice=2,000.7; promoCode=2; quantityRequired=1; promoPrice=2.1
				if (isValidLine(line)) {
					cleanLine (line);
					List<String> subParts = new List<String>(line.Split(seperator1));
					String name = getItemName (subParts);
					decimal? regularPrice = getPrice (subParts, "regularPrice");
					if (name != null && regularPrice != null) {
						GroceryItemData groceryItem;
						if (!nameToGroceryItemDataMap.ContainsKey (name)) {
							groceryItem = new GroceryItemData (name, (decimal)regularPrice);
							nameToGroceryItemDataMap.Add (groceryItem.Name, groceryItem);
						} else {
							groceryItem = nameToGroceryItemDataMap [name];
							groceryItem.RegularPrice = (decimal)regularPrice;
						}
						int? promoCode = getInt (subParts, "promoCode"); 
						if (promoCode != null) {
							groceryItem.setPromotionInfo ((int)promoCode, getPrice (subParts, "promoPrice"), getInt (subParts, "quantityRequired"));
						}
					}
				}
			}
			return nameToGroceryItemDataMap;
		}

		private static Boolean isValidLine(string line){
			if (string.IsNullOrWhiteSpace (line)) {
				return false;
			}
			if (!line.StartsWith ("item")) {
				return false;
			}
			return true;
		}

		private static string cleanLine(string line){
			line = line.Replace (" ", "");
			line = line.ToLower ();
			return line;
		}

		private static decimal? getPrice(List<String> subParts, string name){
			foreach (String entry in subParts) {
				if (entry.Contains (name)) {
					try{
						return Decimal.Parse (entry.Split (seperator2) [1]);
					} catch (FormatException){
						return (decimal?)null;
					}
				} 
			}
			return (decimal?)null;
		}

		private static string getItemName(List<String> subParts){
			foreach (String entry in subParts) {
				if (entry.Contains ("item")) {
					try{
						return entry.Split (seperator2) [1];
					} catch (FormatException){
						return null;
					}
				} 
			}
			return null;
		}

		private static int? getInt(List<String> subParts, string name){
			foreach (String entry in subParts) {
				if (entry.Contains (name)) {
					try {
						return int.Parse (entry.Split (seperator2) [1]);
					}catch (FormatException){
						return (int?)null;
					}
				}
			}
			return (int?)null;
		}

	}
}

