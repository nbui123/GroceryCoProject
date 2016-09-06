using System;

namespace GroceryCo
{
	public class MoneyDisplayUtil
	{
		public static string formatMoneyDisplay(decimal money){
			return "$"+ String.Format ("{0:0.00}", money);
		}
	}
}

