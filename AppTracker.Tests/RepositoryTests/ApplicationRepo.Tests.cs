using System;
using Xunit;
using AppTracker.Models.Repositories;
using AppTracker.Models.DB;


namespace AppTracker.Tests.RepositoryTests
{
    public class ApplicationRepoTests
    {
        [Fact]
        public void CreateApplication_CompanyIsNull()
        {
            var repo = new ApplicationRepo(null);
            var app = new Application() { CompanyId = null };

            var result = repo.CreateApplication(app);

            Assert.Null(result);
        }

        [Fact]
        public void EditApplication_IdsNotMatching()
        {
            int id = 1, id2 = 2;
            int companyId = 1;
            var repo = new ApplicationRepo(null);
            var app = new Application() { Id = id, CompanyId = companyId };

            var result = repo.EditApplication(id2, app);

            Assert.False(result);
        }

        [Fact]
        public void EditApplication_CompanyIsNull()
        {
            var id = 1;
            var repo = new ApplicationRepo(null);
            var app = new Application() { Id = id, CompanyId = null };

            var result = repo.EditApplication(id, app);

            Assert.False(result);
        }
    }
}
