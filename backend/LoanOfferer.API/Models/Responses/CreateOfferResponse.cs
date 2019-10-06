using System;

namespace LoanOfferer.API.Models.Responses
{
    public class CreateOfferResponse
    {
        public string Id { get; set; }
        public int MaxLoanAmount { get; set; }
    }
}
