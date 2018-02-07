using Xunit;
using AppTracker.Models.Repositories;
using AppTracker.Models.DB;
using System;

namespace AppTracker.Tests.RepositoryTests
{
    public class ApplicationStatusRepoTests
    {
        [Fact]
        public void CreateStatus_TimestampIsNotNull()
        {
            var repo = new ApplicationStatusRepo(null);
            var status = new ApplicationStatus() { Timestamp = DateTime.Now };

            var result = repo.CreateStatus(status);

            Assert.Null(result);
        }
    }
}
