using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;
using Xunit;

namespace TrashCollectionAPI.Tests.Controllers
{
    public class RotaControllerTests
    {
        private readonly Mock<IRotaService> _mockRotaService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RotaController _controller;

        public RotaControllerTests()
        {
            _mockRotaService = new Mock<IRotaService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new RotaController(_mockRotaService.Object, _mockMapper.Object);
        }

        [Fact]
        public void BuscarTodasRotas_ReturnsOk()
        {
            // Arrange
            var mockRotas = new List<RotaModel>
            {
                new RotaModel { IdRota = 1, NomeRota = "Rota 1" },
                new RotaModel { IdRota = 2, NomeRota = "Rota 2" }
            };
                    _mockRotaService.Setup(s => s.GetAllRotas()).Returns(mockRotas);

                    var expectedViewModels = new List<RotaViewModel>
            {
                new RotaViewModel { IdRota = 1, NomeRota = "Rota 1" },
                new RotaViewModel { IdRota = 2, NomeRota = "Rota 2" }
            };
            _mockMapper.Setup(m => m.Map<IEnumerable<RotaViewModel>>(mockRotas)).Returns(expectedViewModels);

            // Act
            var actionResult = _controller.BuscarTodasRotas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var viewModelList = Assert.IsAssignableFrom<IEnumerable<RotaViewModel>>(okResult.Value);
            Assert.Equal(expectedViewModels.Count, viewModelList.Count());
        }

        [Fact]
        public void CadastrarRota_ReturnsCreated()
        {
            // Arrange
            var viewModel = new RotaViewModel { IdRota = 1, NomeRota = "Rota 1" };
            var rotaModel = new RotaModel { IdRota = 1, NomeRota = "Rota 1" };

            _mockMapper.Setup(m => m.Map<RotaModel>(viewModel)).Returns(rotaModel);

            // Act
            var result = _controller.CadastrarRota(viewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(RotaController.BuscarTodasRotas), createdAtActionResult.ActionName);
            Assert.Equal(rotaModel.IdRota, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(viewModel, createdAtActionResult.Value);
        }

        [Fact]
        public void BuscarRota_ExistingId_ReturnsOk()
        {
            // Arrange
            var rotaModel = new RotaModel { IdRota = 1, NomeRota = "Rota 1" };
            _mockRotaService.Setup(s => s.GetRotaById(1)).Returns(rotaModel);

            var expectedViewModel = new RotaViewModel { IdRota = 1, NomeRota = "Rota 1" };
            _mockMapper.Setup(m => m.Map<RotaViewModel>(rotaModel)).Returns(expectedViewModel);

            // Act
            var result = _controller.BuscarRota(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<RotaViewModel>(okResult.Value);
            Assert.Equal(expectedViewModel.IdRota, viewModel.IdRota);
            Assert.Equal(expectedViewModel.NomeRota, viewModel.NomeRota);
        }

        [Fact]
        public void BuscarRota_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            _mockRotaService.Setup(s => s.GetRotaById(It.IsAny<int>())).Returns((RotaModel)null);

            // Act
            var result = _controller.BuscarRota(999); // Um ID que não existe

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteColeta_ReturnsOk()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _controller.DeleteColeta(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rota excluido com sucesso", okResult.Value);
            _mockRotaService.Verify(s => s.DeleteRota(id), Times.Once);
        }

        [Fact]
        public void BuscarTodasRotas_IdColeta_ReturnsOk()
        {
            // Arrange
            var idColeta = 1;
            var mockRotas = new List<RotaModel>
            {
                new RotaModel { IdRota = 1, NomeRota = "Rota 1", IdColeta = idColeta },
                new RotaModel { IdRota = 2, NomeRota = "Rota 2", IdColeta = idColeta }
            };
                    _mockRotaService.Setup(s => s.GetAllRotas(idColeta)).Returns(mockRotas);

                    var expectedViewModels = new List<RotaViewModel>
            {
                new RotaViewModel { IdRota = 1, NomeRota = "Rota 1" },
                new RotaViewModel { IdRota = 2, NomeRota = "Rota 2" }
            };
            _mockMapper.Setup(m => m.Map<IEnumerable<RotaViewModel>>(mockRotas)).Returns(expectedViewModels);

            // Act
            var actionResult = _controller.BuscarTodasRotas(idColeta);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var viewModelList = Assert.IsAssignableFrom<IEnumerable<RotaViewModel>>(okResult.Value);
            Assert.Equal(expectedViewModels.Count, viewModelList.Count());
        }
    }
}
