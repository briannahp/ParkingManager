using NUnit.Framework;
using ParkingAPI.Services;
using ParkingAPI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountService> _mockAccountService;
        private AccountController _accountController;

        [SetUp]
        public void SetUp()
        {
            _mockAccountService = new Mock<IAccountService>();
            _accountController = new AccountController(_mockAccountService.Object);
        }

        [Test]
        public async Task GetAccounts_ShouldReturnEveryAccount()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account { Id = 1, FamilyName = "Peterson Family", Email = "brianna@example.com", Phone = "123-456-7890" },
                new Account { Id = 2, FamilyName = "Tran Family", Email = "sydney@example.com", Phone = "999-999-9999" }
            };
            _mockAccountService.Setup(s => s.GetAllAccounts()).ReturnsAsync(accounts);

            // Act
            var result = await _accountController.GetAccounts();

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(accounts, Is.EqualTo(okResult.Value));
        }
        [Test]
        public async Task GetAccounts_ShouldReturnEmptyList_WhenAccountsTableEmpty()
        {
            // Arrange
            _mockAccountService.Setup(s => s.GetAllAccounts()).ReturnsAsync(new List<Account>());

            // Act
            var result = await _accountController.GetAccounts();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var accounts = okResult.Value as IEnumerable<Account>;
            Assert.That(accounts, Is.Empty);
        }

        [Test]
        public async Task GetAccount_ShouldReturnAccountById()
        {
            // Arrange
            var account = new Account { Id = 3, FamilyName = "Bach", Email = "christina@example.com", Phone = "777-777-7777" };
            _mockAccountService.Setup(s => s.GetAccountById(3)).ReturnsAsync(account);

            // Act
            var result = await _accountController.GetAccount(3);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(account));
            
        }

        [Test]
        public async Task GetAccount_ShouldReturn404_WhenAccountIDIsInvalid()
        {
            // Arrange
            _mockAccountService.Setup(s => s.GetAccountById(3)).ReturnsAsync((Account)null);

            // Act
            var result = await _accountController.GetAccount(3);

            // Assert
             Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task UpdateAccount_ShouldReturnNoContent()
        {
            // Arrange
            var account = new Account { Id = 5, FamilyName = "Ramirez", Email = "daniel@example.com", Phone = "123-456-5334" };
            _mockAccountService.Setup(s => s.UpdateAccount(account)).Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.UpdateAccount(5, account);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteAccount_ShouldReturnNoContent()
        {
            // Arrange
            var account = new Account { Id = 1, FamilyName = "Nguyen Family", Email = "kristy@gmail.com", Phone = "888-888-8888" };
            _mockAccountService.Setup(s => s.GetAccountById(1)).ReturnsAsync(account);
            _mockAccountService.Setup(s => s.DeleteAccount(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _accountController.DeleteAccount(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task DeleteAccount_ShouldReturn404_WhenAccountDoesNotExist()
        {
            // Arrange
            _mockAccountService.Setup(s => s.GetAccountById(1)).ReturnsAsync((Account)null);

            // Act
            var result = await _accountController.DeleteAccount(1);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
    
         }
     
     }
}
