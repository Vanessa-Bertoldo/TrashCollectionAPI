using AutoMapper;
using Moq;
using System.Collections.Generic;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;
using Xunit;
using Microsoft.AspNetCore.Mvc;

public class ColetaControllerTest
{
    private readonly Mock<IColetaService> _mockColetaService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ColetaController _controller;

    public ColetaControllerTest()
    {
        _mockColetaService = new Mock<IColetaService>();
        _mockMapper = new Mock<IMapper>();

        _controller = new ColetaController(_mockColetaService.Object, _mockMapper.Object);
    }

    [Fact]
    public void BuscaTodasColeta_ReturnsOkObjectResult()
    {
        // Arrange
        var coletasList = new List<ColetaModel> { new ColetaModel { IdColeta = 1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() } };
        var viewModelList = new List<ColetaViewModel> { new ColetaViewModel { IdColeta = 1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaViewModel>() } };

        _mockColetaService.Setup(s => s.GetAllColetas()).Returns(coletasList);
        _mockMapper.Setup(m => m.Map<IEnumerable<ColetaViewModel>>(coletasList)).Returns(viewModelList);

        // Act
        var result = _controller.BuscaTodasColeta();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<ColetaViewModel>>(okResult.Value);
        Assert.Equal(viewModelList, returnValue);
    }

    [Fact]
    public void AgendarColeta_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var viewModel = new ColetaViewModel { IdColeta = 1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaViewModel>() };
        var coletaModel = new ColetaModel { IdColeta = 1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() };

        _mockMapper.Setup(m => m.Map<ColetaModel>(viewModel)).Returns(coletaModel);
        _mockColetaService.Setup(s => s.AddNewColeta(coletaModel)).Verifiable();

        // Act
        var result = _controller.AgendarColeta(viewModel);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.BuscaTodasColeta), createdAtActionResult.ActionName);
        Assert.Equal(coletaModel.IdColeta, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(viewModel, createdAtActionResult.Value);
    }

    [Fact]
    public void BuscarColeta_ColetaNaoEncontrada_ReturnsNotFound()
    {
        // Arrange
        var coletaId = 1;
        _mockColetaService.Setup(s => s.GetColetaById(coletaId)).Returns((ColetaModel)null);

        // Act
        var result = _controller.BuscarColeta(coletaId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void BuscarColeta_ColetaEncontrada_ReturnsOk()
    {
        // Arrange
        var coletaId = 1;
        var coletaModel = new ColetaModel { IdColeta = coletaId, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() };
        var viewModel = new ColetaViewModel { IdColeta = coletaId, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaViewModel>() };

        _mockColetaService.Setup(s => s.GetColetaById(coletaId)).Returns(coletaModel);
        _mockMapper.Setup(m => m.Map<ColetaViewModel>(coletaModel)).Returns(viewModel);

        // Act
        var result = _controller.BuscarColeta(coletaId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ColetaViewModel>(okResult.Value);
        Assert.Equal(viewModel, returnValue);
    }

    [Fact]
    public void DeleteColeta_ColetaNaoEncontrada_ReturnsNotFound()
    {
        // Arrange
        var coletaId = 1;
        _mockColetaService.Setup(s => s.GetColetaById(coletaId)).Returns((ColetaModel)null);

        // Act
        var result = _controller.DeleteColeta(coletaId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteColeta_ColetaEncontrada_ReturnsNoContent()
    {
        // Arrange
        var coletaId = 1;
        var coletaModel = new ColetaModel { IdColeta = coletaId, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro", DataColeta = DateTime.Now, Rotas = new List<RotaModel>() };

        _mockColetaService.Setup(s => s.GetColetaById(coletaId)).Returns(coletaModel);
        _mockColetaService.Setup(s => s.DeleteColeta(coletaId)).Verifiable();

        // Act
        var result = _controller.DeleteColeta(coletaId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void AtualizarColeta_ReturnsOk()
    {
        var coletaViewModel = new ColetaViewModel
        {
            IdColeta = 1,
            NumeroVolume = 15.5,
            DataRegistro = DateTime.Now,
            NomeBairro = "Centro"
        };

        var coletaModel = new ColetaModel
        {
            IdColeta = 1,
            NumeroVolume = 15.5,
            DataRegistro = DateTime.Now,
            NomeBairro = "Centro"
        };

        _mockMapper.Setup(m => m.Map<ColetaModel>(coletaViewModel)).Returns(coletaModel);

        // Act
        var result = _controller.AtualizarColeta(coletaViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Os dados foram atualizados com sucesso", okResult.Value);
    }
}
