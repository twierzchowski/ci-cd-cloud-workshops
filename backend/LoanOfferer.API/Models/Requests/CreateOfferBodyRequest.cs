namespace LoanOfferer.API.Models.Requests
{
    public class CreateOfferBodyRequest
    {
        public string PeselNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
