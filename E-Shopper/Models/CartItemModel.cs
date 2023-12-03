namespace E_Shopper.Models
{
	public class CartItemModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string image {  get; set; }
		public decimal Total
		{
			get
			{
				return Price * Quantity;
			}
		}
        public CartItemModel()
        {
            
        }
        public CartItemModel(ProductModel product, int quantity)
        {
            ProductId = product.Id;
			ProductName = product.Name;
			Price = product.Price;
			Quantity = quantity;
			image = product.Image;
        }
    }
}
