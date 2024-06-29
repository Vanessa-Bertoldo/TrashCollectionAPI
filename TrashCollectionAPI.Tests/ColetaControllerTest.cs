using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrashCollectionAPI.Controllers;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Tests
{
    public class ColetaControllerTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ColetaController _controller;
        private readonly DbSet<ColetaModel> _mockSet;

        public ColetaControllerTest()
        {
            _mockContext = new Mock<DatabaseContext>();
            _mockSet = MockDbSet();
            _controller = new ColetaController(_mockContext.Object);
        }

        private DbSet<ColetaModel> MockDbSet()
        {
            var data = new List<ColetaModel>
            {
                new ColetaModel { IdColeta =1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "Centro" , DataColeta = DateTime.Now, Rotas = []},
                new ColetaModel { IdColeta =1, NumeroVolume = 20.5, DataRegistro = DateTime.Now, NomeBairro = "BH" , DataColeta = DateTime.Now, Rotas = []},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ColetaModel>>();

            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ColetaModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfColetas()
        {
            // Act
            // Invoca o método Index do controlador para testar seu comportamento
            var result = _controller.Get();

            // Assert
            // Verifica se o resultado obtido é do tipo ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);

            // Verifica se o modelo retornado pelo ViewResult pode ser atribuído a uma coleção de ColetaModel
            var model = Assert.IsAssignableFrom<IEnumerable<ColetaModel>>(viewResult.Model);

            // Confirma se o número de coletas no modelo é igual a 2, conforme esperado
            Assert.Equal(2, model.Count());

            // Confirma se o número de coletas no modelo é maior que 0
            Assert.True(model.Count() > 0, "The number of coletas should be greater than 0");
        }

    }
}
