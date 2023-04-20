namespace Team6.Models
{
	public class OrderHistory
	{	
		public int OrderId { get; set; }
		public int OrderItemId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public string ProductImage { get; set; }


		public DateTime PurchaseOn { get; set; }
		public int Qty { get; set; }
		public List<string> Activation_Code { get; set; }
		public int? Rating { get; set; }
    
		public OrderHistory()
		{
			this.Activation_Code = new List<string>();
		}
			
	}
}
