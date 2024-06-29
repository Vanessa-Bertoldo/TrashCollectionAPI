using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;

namespace TrashCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service;
        private readonly IMapper _mapper;
        private DatabaseContext @object;

        public ColetaController(DatabaseContext @object)
        {
            this.@object = @object;
        }

        public ColetaController(IColetaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todas as coletas agendadas.
        /// </summary>
        /// <returns>Uma ação que retorna uma lista de ColetaViewModel.</returns>
        [HttpGet(Name = "BuscaTodasColeta")]
        public ActionResult<IEnumerable<ColetaViewModel>> BuscaTodasColeta()
        {
            var coletas = _service.GetAllColetas();
            var viewModelList = _mapper.Map<IEnumerable<ColetaViewModel>>(coletas);
            return Ok(viewModelList);
        }

        // <summary>
        /// Agenda uma nova coleta com base nos dados fornecidos no ColetaViewModel.
        /// </summary>
        /// <param name="viewModel">O modelo de visualização contendo os dados da coleta.</param>
        /// <returns>Uma ação que retorna o resultado da criação da coleta.</returns>
        [HttpPost(Name = "AgendarColeta")]
        public ActionResult AgendarColeta([FromBody] ColetaViewModel viewModel)
        {
            var coleta = _mapper.Map<ColetaModel>(viewModel);
            if (coleta != null)
            {
                _service.AddNewColeta(coleta);
                return CreatedAtAction(nameof(BuscaTodasColeta), new { id = coleta.IdColeta }, viewModel);
            }
            return NotFound();
        }


    }
}
