using Xunit;
using AppTracker.Models.Repositories;
using AppTracker.Models.DB;
using AppTracker.Providers;
using AppTracker.Models.Repositories.Interfaces;
using Moq;
using AppTracker.Providers.Interfaces;
using AppTracker.Models.DTO.Metrics;

namespace AppTracker.Tests.ProviderTests
{
    public class MetricsProviderTests
    {
        [Fact]
        public void HeardBack_ZeroDenominator()
        {
            int numeratorCompliment = 0, denominator = 0;
            var mockAppRepo = new Mock<IApplicationRepo>();
            mockAppRepo.Setup(r => r.GetTotalAppCount()).Returns(denominator);
            mockAppRepo.Setup(r => r.GetPendingAppCount()).Returns(numeratorCompliment);
            var provider = new MetricsProvider(mockAppRepo.Object);
            var formatEnum = Format.Decimal;
            var expected = new HeardBackDTO(formatEnum.ToString(), 0);

            var actual = provider.HeardBack(formatEnum);

            Assert.Equal(expected.format, actual.format);
            Assert.Equal(expected.value, actual.value);
        }

        [Fact]
        public void HeardBack_CorrectPercentage()
        {
            int numeratorCompliment = 2, denominator = 3;
            var mockAppRepo = new Mock<IApplicationRepo>();
            mockAppRepo.Setup(r => r.GetTotalAppCount()).Returns(denominator);
            mockAppRepo.Setup(r => r.GetPendingAppCount()).Returns(numeratorCompliment);
            var provider = new MetricsProvider(mockAppRepo.Object);
            var formatEnum = Format.Percentage;
            var expected = new HeardBackDTO(formatEnum.ToString(), (decimal) 33.33);

            var actual = provider.HeardBack(formatEnum);

            Assert.Equal(expected.format, actual.format);
            Assert.Equal(expected.value, actual.value);
        }

        [Fact]
        public void HeardBack_CorrectDecimal()
        {
            int numeratorCompliment = 2, denominator = 3;
            var mockAppRepo = new Mock<IApplicationRepo>();
            mockAppRepo.Setup(r => r.GetTotalAppCount()).Returns(denominator);
            mockAppRepo.Setup(r => r.GetPendingAppCount()).Returns(numeratorCompliment);
            var provider = new MetricsProvider(mockAppRepo.Object);
            var formatEnum = Format.Decimal;
            var expected = new HeardBackDTO(formatEnum.ToString(), (decimal) 0.33);

            var actual = provider.HeardBack(formatEnum);

            Assert.Equal(expected.format, actual.format);
            Assert.Equal(expected.value, actual.value);
        }
    }
}
