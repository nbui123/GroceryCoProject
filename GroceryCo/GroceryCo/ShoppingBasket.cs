using System;
using System.Collections.Generic;

/*
 * Shopping items is listed in groups.
 * ie. ['apple', 'banana', 'apple']
 * the shopping basket will contain 2 items, apple and banana, with apple containing 2 count
 * 
 */
namespace GroceryCo
{
	public class ShoppingBasket
	{
		List<GroceryItemData> items;

		public ShoppingBasket(){
			this.items=new List<GroceryItemData>();
		}
			
		public void addItem(GroceryItemData groceryItem){
			Boolean found = false;
			foreach (GroceryItemData item in items) {
				if (item.Name.Equals(groceryItem.Name)) {
					item.addToQuantity (1);
					found = true;
					break;
				}
			}
			if (!found) {
				this.items.Add(groceryItem);
			}
		}

		public Decimal calculateFinalBill(){
			Decimal sum = 0;
			foreach(GroceryItemData item in items){
				sum += item.getTotalCost ();
			}
			return sum;
		}

		public Decimal calculateTotalSavings(){
			Decimal sum = 0;
			foreach(GroceryItemData item in items){
				sum += item.getTotalSavings ();
			}
			return sum;
		}

		public List<GroceryItemData> getShoppingBasket(){
			return this.items;
		}
	}
}

