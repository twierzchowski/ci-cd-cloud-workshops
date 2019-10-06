using System;
using Microsoft.EntityFrameworkCore;

namespace LoanOfferer.Domain.Infrastructure.SQLite
{
    public class LoanOfferContext : DbContext
    {
        public DbSet<LoanOfferDTO> LoanOffers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./LoanOfferer.db");
        } 
        
        
    }

    public class LoanOfferDTO
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string EmailAddress { get; set; }
        public int MaxLoanAmount { get; set; }
        public int RequestedLoanAmount { get; set; }
    }
}
