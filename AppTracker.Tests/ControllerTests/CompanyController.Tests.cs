using System;
using Xunit;
using Moq;
using AppTracker.Controllers;
using AppTracker.Models.Repositories.Interfaces;
using AppTracker.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AppTracker.Models.DB;

namespace AppTracker.Tests.ControllerTests
{
    public class CompanyResponseTests
    {
        private const int sampleCompanyId = 1;

        private CompanyDTO GetSampleCompanyDTO()
        {
            var dto = new CompanyDTO()
            {
                Id = 1,
                Name = "Sample Company",
                Address1 = "123 Abc Rd.",
                Address2 = "Cupertino, CA 95070",
                Address3 = null,
                Notes = "Good company culture"
            };

            return dto;
        }

        [Fact]
        public void GET_GetCompany_Ok()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.GetId(sampleCompanyId)).Returns(GetSampleCompanyDTO());
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.GetCompany(sampleCompanyId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GET_GetCompany_NotFound()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.GetId(It.IsAny<int>())).Returns((CompanyDTO) null);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.GetCompany(5);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GET_GetCompanyContacts_Ok()
        {
            // Arrange
            var mockCompanyRepo = new Mock<ICompanyRepo>();
            mockCompanyRepo.Setup(r => r.CompanyExists(It.IsAny<int>())).Returns(true);
            var mockContactRepo = new Mock<IContactRepo>();
            mockContactRepo.Setup(r => r.GetContactsByCompany(It.IsAny<int>())).Returns(new List<ContactDTO>());
            var controller = new CompaniesController(mockCompanyRepo.Object, mockContactRepo.Object);

            // Act
            var result = controller.GetCompanyContacts(sampleCompanyId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GET_GetCompanyContacts_NotFound()
        {
            // Arrange
            var mockCompanyRepo = new Mock<ICompanyRepo>();
            mockCompanyRepo.Setup(r => r.CompanyExists(It.IsAny<int>())).Returns(false);
            var mockContactRepo = new Mock<IContactRepo>();
            mockContactRepo.Setup(r => r.GetContactsByCompany(It.IsAny<int>())).Returns(new List<ContactDTO>());
            var controller = new CompaniesController(mockCompanyRepo.Object, mockContactRepo.Object);

            // Act
            var result = controller.GetCompanyContacts(sampleCompanyId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PUT_PutCompany_NoContentResult()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.CompanyExists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(r => r.EditCompany(It.IsAny<int>(), It.IsAny<Company>())).Returns(true);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.PutCompany(sampleCompanyId, new Company());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PUT_PutCompany_NotFound()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.CompanyExists(It.IsAny<int>())).Returns(false);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.PutCompany(sampleCompanyId, new Company());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PUT_PutCompany_BadRequest()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.CompanyExists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(r => r.EditCompany(It.IsAny<int>(), It.IsAny<Company>())).Returns(false);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.PutCompany(sampleCompanyId, new Company());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void POST_PostCompany_BadRequest()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.CreateCompany(It.IsAny<Company>())).Returns((CompanyDTO) null);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.PostCompany(new Company());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void POST_PostCompany_Created()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.CreateCompany(It.IsAny<Company>())).Returns(GetSampleCompanyDTO);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.PostCompany(new Company());

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void DELETE_DeleteCompany_NotFound()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.DeleteCompany(It.IsAny<int>())).Returns((CompanyDTO) null);
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.DeleteCompany(sampleCompanyId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DELETE_DeleteCompany_Ok()
        {
            // Arrange
            var mockRepo = new Mock<ICompanyRepo>();
            mockRepo.Setup(r => r.DeleteCompany(It.IsAny<int>())).Returns(new CompanyDTO());
            var controller = new CompaniesController(mockRepo.Object, null);

            // Act
            var result = controller.DeleteCompany(sampleCompanyId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
