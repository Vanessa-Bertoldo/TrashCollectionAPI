using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;

namespace TrashCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ControllerBase
    {
        private readonly IRotaService _service;
        private readonly IMapper _mapper;

        public RotaController(IRotaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todas as rotas.
        /// </summary>
        /// <returns>Uma ação que retorna uma lista de RotaViewModel.</returns>
        [HttpGet("BuscarRotas")]
        public ActionResult<IEnumerable<RotaViewModel>> BuscarTodasRotas()
        {
            var rotas = _service.GetAllRotas();
            var viewModelList = _mapper.Map<IEnumerable<RotaViewModel>>(rotas);
            return Ok(viewModelList);
        }

        /// <summary>
        /// Agenda uma nova coleta com base nos dados fornecidos no RotaViewModel.
        /// </summary>
        /// <param name="viewModel">O modelo de visualização contendo os dados da rota.</param>
        /// <returns>Uma ação que retorna o resultado da criação da rota.</returns>
        [HttpPost("CadastrarRota")]
        public IActionResult CadastrarRota([FromBody]RotaViewModel viewModel)
        {
            var rota = _mapper.Map<RotaModel>(viewModel);
            _service.AddNewRota(rota);
            return CreatedAtAction(nameof(BuscarTodasRotas), new { id = rota.IdRota }, viewModel);
        }

        /// <summary>
        /// Busca uma rota com base do id fornecido.
        /// </summary>
        /// <param name="id">id da rota que deseja buscar.</param>
        /// <returns>Retorna uma rota.</returns>
        [HttpGet("BuscarColeta{Id}")]
        public ActionResult<RotaViewModel> BuscarRota([FromRoute] int Id)
        {
            var coleta = _service.GetRotaById(Id);
            if (coleta == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<RotaViewModel>(coleta);
            return Ok(viewModel);
        }

        /// <summary>
        /// Exclui uma rota com base do id fornecido.
        /// </summary>
        /// <param name="id">id do rota que deseja excluir.</param>
        /// <returns>200</returns>
        [HttpDelete("DeleteStatus/{id}")]
        public ActionResult DeleteColeta([FromRoute] int id)
        {
            _service.DeleteRota(id);
            return Ok("Rota excluido com sucesso");
        }

        /// <summary>
        /// Busca as rota com base do id fornecido.
        /// </summary>
        /// <param name="id">id da coleta que deseja buscar as rotas.</param>
        /// <returns>Retorna uma rota.</returns>
        [HttpGet("BuscarRotasDeUmaColeta{idColeta}")]
        public ActionResult<IEnumerable<RotaViewModel>> BuscarTodasRotas([FromRoute] int idColeta)
        {
            var rotas = _service.GetAllRotas(idColeta);
            var viewModelList = _mapper.Map<IEnumerable<RotaViewModel>>(rotas);
            return Ok(viewModelList);
        }
    }
}
