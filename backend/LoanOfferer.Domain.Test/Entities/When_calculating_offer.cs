using FluentAssertions.Common;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.Test.TestHelpers;
using LoanOfferer.Domain.ValueObjects;
using Moq;
using Xunit;

namespace LoanOfferer.Domain.Test.Entities
{
    public class When_calculating_offer
    {
        [Theory]
        [InlineData(1, 1000)]
        [InlineData(49, 1000)]
        [InlineData(50, 2000)]
        [InlineData(51, 2000)]
        [InlineData(69, 2000)]
        [InlineData(70, 3500)]
        [InlineData(71, 3500)]
        [InlineData(89, 3500)]
        [InlineData(90, 5000)]
        [InlineData(91, 5000)]
        [InlineData(100, 5000)]
        public void It_should_correctly_set_max_loan_amount_using_scoring_service(int scoringValue, int maxLoanAmountValue)
        {
            // Given
            var loanOffer = new LoanOfferBuilder().Build();
            var expectedScoring = new Score(scoringValue);
            var expectedMaxLoanAmountValue = new LoanAmount(maxLoanAmountValue);
            var externalServiceMock = new Mock<IScoringService>();
            externalServiceMock.Setup(service => service.GetScore(loanOffer.PeselNumber))
                               .Returns(expectedScoring);

            // When
            loanOffer.CalculateOffer(externalServiceMock.Object);

            // Then
            externalServiceMock.Verify(service => service.GetScore(loanOffer.PeselNumber), Times.Once);
            loanOffer.MaxLoanAmount.Value.IsSameOrEqualTo(expectedMaxLoanAmountValue.Value);
        }
    }
}