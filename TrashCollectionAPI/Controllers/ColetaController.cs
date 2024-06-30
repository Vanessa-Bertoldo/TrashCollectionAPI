using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;

namespace TrashCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service;
        private readonly IMapper _mapper;

        public ColetaController(IColetaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todas as coletas agendadas.
        /// </summary>
        /// <returns>Uma ação que retorna uma lista de ColetaViewModel.</returns>
        [HttpGet("BuscaTodasColeta")]
        public ActionResult<IEnumerable<ColetaViewModel>> BuscaTodasColeta()
        {
            var coletas = _service.GetAllColetas();
            var viewModelList = _mapper.Map<IEnumerable<ColetaViewModel>>(coletas);
            return Ok(viewModelList);
        }

        /// <summary>
        /// Agenda uma nova coleta com base nos dados fornecidos no ColetaViewModel.
        /// </summary>
        /// <param name="viewModel">O modelo de visualização contendo os dados da coleta.</param>
        /// <returns>Uma ação que retorna o resultado da criação da coleta.</returns>
        [HttpPost("AgendarColeta")]
        public ActionResult AgendarColeta([FromBody] ColetaViewModel ViewModel)
        {
            var Coleta = _mapper.Map<ColetaModel>(ViewModel);
            _service.AddNewColeta(Coleta);
            return CreatedAtAction(nameof(BuscaTodasColeta), new { id = Coleta.IdColeta }, ViewModel);
        }

        /// <summary>
        /// Busca uma coleta com base do id fornecido.
        /// </summary>
        /// <param name="id">id da coleta que deseja buscar.</param>
        /// <returns>Retorna uma coleta.</returns>
        [HttpGet("BuscarColeta{Id}")]
        public ActionResult<ColetaViewModel> BuscarColeta([FromRoute] int Id)
        {
            var coleta = _service.GetColetaById(Id);
            if (coleta == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ColetaViewModel>(coleta);
            return Ok(viewModel);
        }

        /// <summary>
        /// Efetua a deleção de uma coleta com base do id fornecido.
        /// </summary>
        /// <param name="id">id da coleta que deseja deletar.</param>
        /// <returns>Retorna status 200.</returns>
        [HttpDelete("DeleteColeta/{id}")]
        public ActionResult DeleteColeta(int id)
        {
            var coleta = _service.GetColetaById(id);
            if (coleta != null)
            {
                _service.DeleteColeta(id);
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Atualiza os dados de uma coleta com base do id fornecido.
        /// </summary>
        /// <param name="coleta">Dados da coleta do tipo CaminhaoViewModel.</param>
        /// <returns>200</returns>
        [HttpPut("{id}")]
        public ActionResult AtualizarColeta([FromRoute] int id, [FromBody] ColetaViewModel coleta)
        {
            var exists = _service.GetColetaById(id);
            if (exists != null)
            {
                var coletaModel = _mapper.Map<ColetaModel>(coleta);
                _service.UpdateColeta(coletaModel);
                return Ok("Os dados foram atualizados com sucesso");
            }
            return NotFound();
        }
    }
}
