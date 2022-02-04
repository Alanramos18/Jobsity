using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Business.Services;
using JobsityChat.Data.Entities;
using JobsityChat.Data.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace JobsityChat.Business.Test.Services
{
    [TestFixture]
    public class ChatServiceTests
    {
        private Mock<IChatRepository> _repository;
        private ChatService _service;

        private CancellationToken _cancellationToken;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Mock<IChatRepository>(MockBehavior.Strict);
            _cancellationToken = new CancellationToken();

            _service = new ChatService(_repository.Object);
        }

        [Test]
        public async Task GetRoomsForIndexAsync_Returns_ChatList()
        {
            // Arrange
            var list = new List<Chat>
            {
                new Chat { Name = "NameTest" }
            };

            _repository.Setup(x => x.GetUserRoomsAsync(It.IsAny<string>(), _cancellationToken)).Returns(Task.FromResult(list));

            // Act
            var result = await _service.GetRoomsForIndexAsync("18", _cancellationToken);

            // Assert
            Assert.That(result, Is.TypeOf<List<Chat>>());
            Assert.That(result[0].Name, Is.EqualTo("NameTest"));
        }
    }
}
