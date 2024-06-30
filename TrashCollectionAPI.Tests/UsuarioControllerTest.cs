using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;

namespace TrashCollectionAPI.Tests
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UsuarioController(_mockAuthService.Object, _mockMapper.Object);
        }
        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var loginViewModel = new LoginViewModel
            {
                Usuario = "testuser",
                Senha = "invalidpassword" 
            };

            var authModel = new AuthModel
            {
                Username = loginViewModel.Usuario,
                Password = loginViewModel.Senha
            };

            _mockMapper.Setup(m => m.Map<AuthModel>(It.IsAny<LoginViewModel>())).Returns(authModel);

            _mockAuthService.Setup(m => m.VerificaLogin(It.IsAny<AuthModel>())).Returns((TokenUserModel)null);

            // Act
            var result = _controller.Login(loginViewModel);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }


    }
}
