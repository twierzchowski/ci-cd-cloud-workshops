namespace LoanOfferer.API.Models.Requests
{
    public class RequestLoanBodyRequest
    {
        public string OfferId { get; set; }
        public int RequestedAmount { get; set; }
    }
}
