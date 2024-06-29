using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrashCollectionAPI.Tests
{
    public class ColetaControllerTest
    {
        private readonly Mock<IColetaService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ColetaController _controller;

        public ColetaControllerTest()
        {
            _mockService = new Mock<IColetaService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ColetaController(_mockService.Object, _mockMapper.Object);

            ConfigureMockService();
            ConfigureMockMapper();
        }

        private void ConfigureMockService()
        {
            var coletas = new List<ColetaModel>
        {
            new ColetaModel { IdColeta = 1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() },
            new ColetaModel { IdColeta = 2, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "BH", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() },
        };
            _mockService.Setup(s => s.GetAllColetas()).Returns(coletas);

            _mockService.Setup(s => s.AddNewColeta(It.IsAny<ColetaModel>()))
                        .Callback<ColetaModel>(coleta =>
                        {
                            coleta.IdColeta = coletas.Count + 1; 
                            coletas.Add(coleta);
                        });
        }

        private void ConfigureMockMapper()
        {
            _mockMapper.Setup(m => m.Map<IEnumerable<ColetaViewModel>>(It.IsAny<IEnumerable<ColetaModel>>()))
                       .Returns((IEnumerable<ColetaModel> coletas) =>
                       {
                           return coletas.Select(coleta => new ColetaViewModel
                           {
                               IdColeta = coleta.IdColeta,
                               NumeroVolume = coleta.NumeroVolume,
                               DataRegistro = coleta.DataRegistro,
                           });
                       });

            _mockMapper.Setup(m => m.Map<ColetaModel>(It.IsAny<ColetaViewModel>()))
                       .Returns((ColetaViewModel viewModel) =>
                       {
                           return new ColetaModel
                           {
                               IdColeta = viewModel.IdColeta,
                               NumeroVolume = viewModel.NumeroVolume,
                               DataRegistro = viewModel.DataRegistro,
                           };
                       });
        }

        [Fact]
        public void AgendarColeta_ReturnsCreatedAtAction()
        {
            // Arrange
            var viewModel = new ColetaViewModel
            {
                IdColeta = 3, 
                NumeroVolume = 25.0,
                DataRegistro = DateTime.Now,
            };

            // Act
            var result = _controller.AgendarColeta(viewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.BuscaTodasColeta), createdAtActionResult.ActionName);
            Assert.Equal(viewModel.IdColeta, createdAtActionResult.RouteValues["id"]);
        }
    }


}
