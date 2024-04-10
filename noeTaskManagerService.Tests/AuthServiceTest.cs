using FluentAssertions;
using noeTaskManagerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noeTaskManagerService.Tests
{
    public class AuthServiceTest
    {
        [Fact]
        public void AuthService_Initialise_ReturnAuthServiceInstance()
        {
            //Act
            var result = AuthService.GetInstance();

            //Assert
            result.Should().BeOfType<AuthService>();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        public void AuthService_SignIn_ThrowAuthException(string email, string password)
        {
            //Arrange
            AuthService authService = AuthService.GetInstance();

            //Act
            Func<Task> result = async () => await authService.SignIn(email, password);

            //Assert
            result.Should().ThrowAsync<AuthException>().WithMessage("No valid credentials were provided");
        }

        [Theory]
        [InlineData(" ", " ", " ", " ")]
        public void AuthService_SignUp_ThrowAuthException(string firstName, string lastName, string email, string password)
        {
            //Arrange
            AuthService authService = AuthService.GetInstance();

            //Act
            Func<Task> result = async () => await authService.SignUp(firstName, lastName, email, password);

            //Assert
            result.Should().ThrowAsync<AuthException>().WithMessage("No valid credentials were provided");
        }

    }
}
