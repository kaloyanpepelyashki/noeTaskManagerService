using FluentAssertions;
using noeTaskManagerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace noeTaskManagerService.Tests
{
    public class PasswordEncryptorTest
    {
        [Theory]
        [InlineData(" ")]
        public void PasswordEncryptor_HashPassword_ThrowException(string password)
        {
            //Arrange

            //Act
            Action result = () => PasswordEncryptor.HashPassword(password);

            //Assert
            result.Should().Throw<PasswordEncryptorException>().WithMessage("No password was passed to the encryptor");
        }

        [Theory]
        [InlineData("testpasswordtobehashed")]
        public void PasswordEncryptor_HashPassword_ShouldNotThrowException(string password)
        {
            //Arrange

            //Act 
            Action result = () => PasswordEncryptor.HashPassword(password);

            //Assert
            result.Should().NotThrow<PasswordEncryptorException>();
        }

        [Theory]
        [InlineData("testpassword")]
        public void PasswordEncryptor_VerifyPassword_ShouldReturnTrue(string rawPassword)
        {
            //Arrange
            string passwordHash = PasswordEncryptor.HashPassword(rawPassword);

            //Act
            var result = PasswordEncryptor.VerifyPassword(rawPassword, passwordHash);
            Action actionResult = () => PasswordEncryptor.VerifyPassword(rawPassword, passwordHash);

            //Assert
            result.Should().Be(true);
            actionResult.Should().NotThrow<PasswordEncryptorException>();
        }

    }
}
