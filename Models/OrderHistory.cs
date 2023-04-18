namespace Team6.Models
{
	public class OrderHistory
	{
		public int ProductId { get; set; }
		public DateTime PurchaseOn { get; set; }
		public int Qty { get; set; }
		public string Activation_Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ProductImage { get; set; }
		public OrderHistory()
		{
		}
			
	}
}
