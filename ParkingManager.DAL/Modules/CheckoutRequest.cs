namespace ParkingManager.DAL.Modules
{
	public partial class CheckoutRequest
	{
		public int Id { get; set; }
		public string MerchantRequestID { get; set; }
		public string CheckoutRequestID { get; set; }
		public string ResponseCode { get; set; }
		public string ResponseDescription { get; set; }
		public string CustomerMessage { get; set; }
	}
}
