using LoanOfferer.API.Models.Requests;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanOffererAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestLoanController : ControllerBase
    {
        //[HttpGet]
        //public async 
        // PUT api/loan
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

     
        
    }
}
