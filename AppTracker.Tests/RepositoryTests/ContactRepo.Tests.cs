using System;
using Xunit;
using AppTracker.Models.Repositories;
using AppTracker.Models.DB;

namespace AppTracker.Tests.RepositoryTests
{
    public class ContactRepoTests
    {
        [Fact]
        public void CreateContact_FirstAndLastIsNullOrWhiteSpace()
        {
            var repo = new ContactRepo(null);
            var contact = new Contact() { FirstName = null, LastName = null };

            var dto = repo.CreateContact(contact);

            Assert.Null(dto);
        }

        [Fact]
        public void EditContact_FirstAndLastIsNullOrWhiteSpace()
        {
            var id = 1;
            var repo = new ContactRepo(null);
            var contact = new Contact() { Id = id, FirstName = null, LastName = null };

            var result = repo.EditContact(id, contact);

            Assert.False(result);
        }

        [Fact]
        public void EditContact_IdsNotMatching()
        {
            int id = 1, id2 = 2;
            var repo = new ContactRepo(null);
            var contact = new Contact() { Id = id, FirstName = "Kevin", LastName = null };

            var result = repo.EditContact(id2, contact);

            Assert.False(result);
        }
    }
}
