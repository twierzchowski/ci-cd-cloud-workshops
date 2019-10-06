using System;
using System.Threading.Tasks;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Services
{
    public class FakeEmailNotificationService: IEmailNotificationService
    {
        public async Task SendLoanRequestedMessage(EmailAddress emailAddress, LoanAmount requestedLoanAmount)
        {
            Console.WriteLine("sending email...");
            //throw new System.NotImplementedException();
        }
    }
}
