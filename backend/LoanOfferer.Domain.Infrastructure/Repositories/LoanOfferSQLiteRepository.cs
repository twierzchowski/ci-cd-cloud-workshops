using System.Linq;
using System.Threading.Tasks;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Infrastructure.SQLite;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoanOfferer.Domain.Infrastructure.Repositories
{
    public class LoanOfferSQLiteRepository: ILoanOfferRepository
    {
        private readonly ILoanOfferFactory _loanOfferFactory;
        
        public LoanOfferSQLiteRepository(ILoanOfferFactory loanOfferFactory)
        {
            _loanOfferFactory = loanOfferFactory;
        }

        public async Task AddAsync(ILoanOffer loanOffer)
        {
            using (var db = new LoanOfferContext())
            {
                db.LoanOffers.Add(
                    new LoanOfferDTO
                    {
                        Id = loanOffer.Id.Value,
                        Pesel = loanOffer.PeselNumber.Value,
                        EmailAddress = loanOffer.EmailAddress.Value,
                        MaxLoanAmount = loanOffer.MaxLoanAmount.Value,
                        RequestedLoanAmount = loanOffer.RequestedLoanAmount.Value
                    }
                );
                await db.SaveChangesAsync();
            }
        }

        public async Task<ILoanOffer> GetAsync(EntityIdentity offerId)
        {
            using (var db = new LoanOfferContext())
            {
                var loan = await db.LoanOffers.FirstOrDefaultAsync(loanOffer => loanOffer.Id == offerId.Value);
                if (loan == null)
                {
                    return null;
                }
                return _loanOfferFactory.Create(loan.Id.ToString(), loan.Pesel, loan.EmailAddress,loan.MaxLoanAmount);
            }
        }

        public async Task UpdateAsync(ILoanOffer loanOffer)
        {
            using (var db = new LoanOfferContext())
            {
                var loan = await db.LoanOffers.FirstOrDefaultAsync(x => x.Id == loanOffer.Id.Value);
                if (loan == null)
                {
                    return;
                }

                loan.RequestedLoanAmount = loanOffer.RequestedLoanAmount.Value;
                await db.SaveChangesAsync();
            }
        } 
    }
}
