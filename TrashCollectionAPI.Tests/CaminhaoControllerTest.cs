using AutoMapper;
using Moq;
using System.Collections.Generic;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;
using Xunit;
using Microsoft.AspNetCore.Mvc;

public class CaminhaoControllerTest
{
    private readonly Mock<ICaminhaoService> _mockCaminhaoService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CaminhaoController _controller;

    public CaminhaoControllerTest()
    {
        _mockCaminhaoService = new Mock<ICaminhaoService>();
        _mockMapper = new Mock<IMapper>();

        _controller = new CaminhaoController(_mockCaminhaoService.Object, _mockMapper.Object);
    }

    [Fact]
    public void BuscaTodosCaminhoes_ReturnsOkObjectResult()
    {
        // Arrange
        var caminhoesList = new List<CaminhaoModel> { new CaminhaoModel { IdCaminhao = 0, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 } };
        var viewModelList = new List<CaminhaoViewModel> { new CaminhaoViewModel { IdCaminhao = 0, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 } };

        _mockCaminhaoService.Setup(s => s.GetAllCaminhoes()).Returns(caminhoesList);
        _mockMapper.Setup(m => m.Map<IEnumerable<CaminhaoViewModel>>(caminhoesList)).Returns(viewModelList);

        // Act
        var result = _controller.BuscaTodosCaminhoes();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CaminhaoViewModel>>(okResult.Value);
        Assert.Equal(viewModelList, returnValue);
    }

    [Fact]
    public void BuscaTodosCaminhoes_ReturnsBadRequest()
    {
        // Arrange
        _mockCaminhaoService.Setup(s => s.GetAllCaminhoes()).Returns((List<CaminhaoModel>)null);

        // Act
        var result = _controller.BuscaTodosCaminhoes();

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void BuscarCaminhao_ReturnsOkObjectResult()
    {
        // Arrange
        var caminhaoId = 1;
        var caminhaoModel = new CaminhaoModel { IdCaminhao = caminhaoId, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 };
        var viewModel = new CaminhaoViewModel { IdCaminhao = caminhaoId, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 };

        _mockCaminhaoService.Setup(s => s.GetCaminhaoById(caminhaoId)).Returns(caminhaoModel);
        _mockMapper.Setup(m => m.Map<CaminhaoViewModel>(caminhaoModel)).Returns(viewModel);

        // Act
        var result = _controller.BuscarCaminhao(caminhaoId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<CaminhaoViewModel>(okResult.Value);
        Assert.Equal(viewModel, returnValue);
    }

    [Fact]
    public void BuscarCaminhao_CaminhaoNaoEncontrado_ReturnsNotFound()
    {
        // Arrange
        var caminhaoId = 999;
        _mockCaminhaoService.Setup(s => s.GetCaminhaoById(caminhaoId)).Returns((CaminhaoModel)null);

        // Act
        var result = _controller.BuscarCaminhao(caminhaoId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void CadastrarCaminhao_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var viewModel = new CaminhaoViewModel { IdCaminhao = 0, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 };
        var caminhaoModel = new CaminhaoModel { IdCaminhao = 0, Modelo = "MODELO1", NumeroCapacidade = 0, HNumeroMaxCapacidade = 80 };

        _mockMapper.Setup(m => m.Map<CaminhaoModel>(viewModel)).Returns(caminhaoModel);
        _mockCaminhaoService.Setup(s => s.AddNewCaminhao(caminhaoModel)).Verifiable();

        // Act
        var result = _controller.CadastrarCaminhao(viewModel);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.BuscaTodosCaminhoes), createdAtActionResult.ActionName);
        Assert.Equal(caminhaoModel.IdCaminhao, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(viewModel, createdAtActionResult.Value);
    }

    [Fact]
    public void DeletarCaminhao_ReturnsOkResult()
    {
        // Arrange
        var caminhaoId = 1;
        _mockCaminhaoService.Setup(s => s.DeleteCaminhao(caminhaoId)).Verifiable();

        // Act
        var result = _controller.DeletarCaminhao(caminhaoId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Caminhao excluido com sucesso", okResult.Value);
    }

    [Fact]
    public void BuscarCaminhao_NegativeId_ReturnsBadRequest()
    {
        // Arrange
        var negativeId = -1;

        // Act
        var result = _controller.BuscarCaminhao(negativeId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Id não pode ser negativo.", badRequestResult.Value);
    }

    [Fact]
    public void AtualizarCaminhao_ReturnsOk()
    {
        int idCaminhao = 1;
        var caminhaoViewModel = new CaminhaoViewModel
        {
            IdCaminhao = idCaminhao,
            Modelo = "Modelo Teste",
            NumeroCapacidade = 10,
            HNumeroMaxCapacidade = 100,
            IdStatus = 1
        };

        var caminhaoModel = new CaminhaoModel
        {
            IdCaminhao = idCaminhao,
            Modelo = "Modelo Teste",
            NumeroCapacidade = 10,
            HNumeroMaxCapacidade = 100,
            IdStatus = 1
        };

        _mockCaminhaoService.Setup(s => s.GetCaminhaoById(idCaminhao)).Returns(caminhaoModel);
        _mockMapper.Setup(m => m.Map<CaminhaoModel>(caminhaoViewModel)).Returns(caminhaoModel);

        // Act
        var result = _controller.AtualizarCaminhao(idCaminhao, caminhaoViewModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Os dados foram atualizados com sucesso", okResult.Value);
    }

    [Fact]
    public void AtualizarCaminhao_ReturnsNotFound()
    {
        int idCaminhao = 1;
        _mockCaminhaoService.Setup(s => s.GetCaminhaoById(idCaminhao)).Returns((CaminhaoModel)null);

        // Act
        var result = _controller.AtualizarCaminhao(idCaminhao, new CaminhaoViewModel());

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
}
