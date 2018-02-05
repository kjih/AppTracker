using System;
using Xunit;
using AppTracker.Models.Repositories;
using AppTracker.Models.DB;

namespace AppTracker.Tests.RepositoryTests
{
    public class CompanyRepoTests
    {
        [Fact]
        public void CreateCompany_NameIsNullOrWhiteSpace()
        {
            // Arrange
            var repo = new CompanyRepo(null);
            var company1 = new Company() { Name = "" };
            var company2 = new Company() { Name = " " };
            var company3 = new Company() { Name = null };

            // Act
            var result1 = repo.CreateCompany(company1);
            var result2 = repo.CreateCompany(company2);
            var result3 = repo.CreateCompany(company3);

            // Assert
            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
        }

        [Fact]
        public void EditCompany_IdNotMatching()
        {
            // Arrange
            var id = 1;
            var repo = new CompanyRepo(null);
            var company = new Company() { Id = 5, Name = "Company Name" };

            // Act
            var result = repo.EditCompany(id, company);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EditCompany_NameIsNullOrWhiteSpace()
        {
            // Arrange
            var id = 1;
            var repo = new CompanyRepo(null);
            var company1 = new Company() { Id = id, Name = "" };
            var company2 = new Company() { Id = id, Name = " " };
            var company3 = new Company() { Id = id, Name = null };

            // Act
            var result1 = repo.EditCompany(id, company1);
            var result2 = repo.EditCompany(id, company2);
            var result3 = repo.EditCompany(id, company3);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }
    }
}
