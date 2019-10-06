using System.Threading.Tasks;
using LoanOfferer;
using LoanOfferer.API.Models.Requests;
using LoanOfferer.API.Models.Responses;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanOffererAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateOfferController : ControllerBase
    {
        // POST api/offer
        [HttpPost]
        public async Task<CreateOfferResponse> Post([FromBody] CreateOfferBodyRequest request)
        {
            var service = CreateOfferService();
            var loanOffer = await service.CreateOfferAsync(request.PeselNumber, request.EmailAddress);
            return new CreateOfferResponse{Id = loanOffer.Id.ToString(), MaxLoanAmount = loanOffer.MaxLoanAmount.Value};
        }

        // PUT api/offer
        [HttpPut]
        public async void Put([FromBody] RequestLoanBodyRequest request)
        {
            var service = CreateRequestLoanService();
            await service.RequestLoan(request.OfferId, request.RequestedAmount);
            
        }
        
        private static RequestLoanService CreateRequestLoanService()
        {
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferSQLiteRepository(loanOfferFactory);
            //TODO:
            //var emailConfig = new EnvironmentVariablesEmailServiceConfig();
            var emailService = new FakeEmailNotificationService();
            var service = new RequestLoanService(loanOfferRepository, emailService);

            return service;
        }
        
        private static CreateOfferService CreateOfferService()
        {
            var externalApiScoringServiceConfig = new EnvironmentVariablesExternalApiScoringServiceConfig();
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferSQLiteRepository(loanOfferFactory);
            var scoringService = new ExternalApiScoringService(externalApiScoringServiceConfig);
            var service = new CreateOfferService(loanOfferFactory, loanOfferRepository, scoringService);
            
            return service;
        }
    }
}
