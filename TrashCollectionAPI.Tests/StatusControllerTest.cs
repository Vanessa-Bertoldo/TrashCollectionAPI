using AutoMapper;
using Moq;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class StatusControllerTest
{
    private readonly Mock<IStatusService> _mockStatusService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly StatusController _controller;

    public StatusControllerTest()
    {
        _mockStatusService = new Mock<IStatusService>();
        _mockMapper = new Mock<IMapper>();

        _controller = new StatusController(_mockStatusService.Object, _mockMapper.Object);
    }

    [Fact]
    public void CadastrarStatus_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var viewModel = new StatusViewModel { IdStatus = 0, NomeStatus = "DISPONIVEL" };
        var statusModel = new StatusModel { IdStatus = 0, NomeStatus = "INDISPONIVEL" };

        _mockMapper.Setup(m => m.Map<StatusModel>(viewModel)).Returns(statusModel);
        _mockStatusService.Setup(s => s.AddNewStatus(statusModel)).Verifiable();

        // Act
        var result = _controller.CadastrarStatus(viewModel);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.BuscarTodosStatus), createdAtActionResult.ActionName);
        Assert.Equal(statusModel.IdStatus, createdAtActionResult.RouteValues["id"]);
        Assert.Equal(viewModel, createdAtActionResult.Value);
    }

    [Fact]
    public void BuscarTodosStatus_ReturnsOkObjectResult()
    {
        // Arrange
        var statusList = new List<StatusModel> { new StatusModel { IdStatus = 0, NomeStatus = "DISPONIVEL" } };
        var viewModelList = new List<StatusViewModel> { new StatusViewModel { IdStatus = 0, NomeStatus = "DISPONIVEL" } };

        _mockStatusService.Setup(s => s.GetAllStatus()).Returns(statusList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StatusViewModel>>(statusList)).Returns(viewModelList);

        // Act
        var result = _controller.BuscarTodosStatus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<StatusViewModel>>(okResult.Value);
        Assert.Equal(viewModelList, returnValue);
    }

    [Fact]
    public void BuscarStatus_ReturnsOkObjectResult()
    {
        // Arrange
        var statusId = 1;
        var statusModel = new StatusModel { IdStatus = statusId, NomeStatus = "DISPONIVEL" };
        var viewModel = new StatusViewModel { IdStatus = statusId, NomeStatus = "DISPONIVEL" };

        _mockStatusService.Setup(s => s.GetStatusById(statusId)).Returns(statusModel);
        _mockMapper.Setup(m => m.Map<StatusViewModel>(statusModel)).Returns(viewModel);

        // Act
        var result = _controller.BuscarStatus(statusId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<StatusViewModel>(okResult.Value);
        Assert.Equal(viewModel, returnValue);
    }

    [Fact]
    public void BuscarStatus_ReturnsNotFoundResult()
    {
        // Arrange
        var statusId = 1;
        _mockStatusService.Setup(s => s.GetStatusById(statusId)).Returns((StatusModel)null);

        // Act
        var result = _controller.BuscarStatus(statusId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void DeleteColeta_ReturnsOkResult()
    {
        // Arrange
        var statusId = 1;
        _mockStatusService.Setup(s => s.DeleteStatus(statusId)).Verifiable();

        // Act
        var result = _controller.DeleteColeta(statusId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Status excluido com sucesso", okResult.Value);
    }
}
