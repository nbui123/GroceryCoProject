using System;

namespace GroceryCo
{
	public class GroceryItemData
	{
		private String _name;
		private Decimal _regularPrice;
		private int _count;		
		private Boolean _isOnPromotion;
		private IPromotion _promotionInfo;

		public GroceryItemData (String name, Decimal regularPrice)
		{
			_name = name;
			_regularPrice = regularPrice;
			_isOnPromotion = false;
			_count = 1;
		}

		public IPromotion PromotionInfo
		{
			get { return this._promotionInfo;} 
		}

		public String Name
		{
			get { return this._name; }
		}

		public Decimal RegularPrice
		{
			get { return this._regularPrice; }
			set { this._regularPrice = value; }
		}

		public Boolean isOnPromotion
		{
			get { return this._isOnPromotion; }
		}

		public int Count
		{
			get { return this._count; }
			set {this. _count = value; }
		}

		public void addToQuantity(int quantity)
		{
			this._count += quantity;
		}

		public void setPromotionInfo (int promoCode, Decimal? promoPrice, int? quantityToGetPromoPrice)
		{
			_promotionInfo = PromoUtil.getPromotionInto (promoCode, _regularPrice, promoPrice, quantityToGetPromoPrice); 
			if (_promotionInfo != null) {
				_isOnPromotion = true;
			}
		}

		public decimal getTotalCost() {
			decimal totalCost = 0;
			int amount = _count;
			while (amount > 0 ){
				if (_isOnPromotion && _promotionInfo.isMetPromoRequirements (amount)) {
					totalCost += _promotionInfo.getTotalPriceForQuantityMet();
					amount -= _promotionInfo.getQuantityToGetPromoPrice ();
				} else {
					totalCost += _regularPrice;
					amount -= 1;
				}
			}
			return totalCost;
		}

		public decimal getTotalSavings() {
			decimal totalSavings = 0;
			int amount = _count;
			while (amount > 0 ){
				if (_isOnPromotion && _promotionInfo.isMetPromoRequirements (amount)) {
					totalSavings += _promotionInfo.getSavings();
					amount -= _promotionInfo.getQuantityToGetPromoPrice ();
				} else {
					break;
				}
			}
			return totalSavings;
		}
	}
}

