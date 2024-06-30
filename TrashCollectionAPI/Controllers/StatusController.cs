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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _service;
        private readonly IMapper _mapper;
        public DatabaseContext Context { get; set; }
        public StatusController(IStatusService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        /// <summary>
        /// Cria um novo registro de status
        /// </summary>
        /// <param name="viewModel">Dados do status que deseja inserir.</param>
        /// <returns>201</returns>
        [HttpPost("CadastrarStatus")]
        public ActionResult CadastrarStatus(StatusViewModel viewModel)
        {
            var Status = _mapper.Map<StatusModel>(viewModel);
            _service.AddNewStatus(Status);
            return CreatedAtAction(nameof(BuscarTodosStatus), new { id = Status.IdStatus }, viewModel);
        }

        /// <summary>
        /// Lista todos os status
        /// </summary>
        /// <returns>200</returns>
        [HttpGet("ListarStatus")]
        public ActionResult<IEnumerable<StatusViewModel>> BuscarTodosStatus()
        {
            var status = _service.GetAllStatus();
            var viewModelList = _mapper.Map<IEnumerable<StatusViewModel>>(status);
            return Ok(viewModelList);
        }

        /// <summary>
        /// Busca um status com base do id fornecido.
        /// </summary>
        /// <param name="id">id do status que deseja buscar.</param>
        /// <returns>200</returns>
        [HttpGet("BuscarStatus{Id}")]
        public ActionResult<StatusViewModel> BuscarStatus([FromRoute] int Id)
        {
            var coleta = _service.GetStatusById(Id);
            if (coleta == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<StatusViewModel>(coleta);
            return Ok(viewModel);
        }

        /// <summary>
        /// Exclui um status com base do id fornecido.
        /// </summary>
        /// <param name="id">id do status que deseja excluir.</param>
        /// <returns>200</returns>
        [HttpDelete("DeleteStatus/{id}")]
        public ActionResult DeleteColeta([FromRoute] int id)
        {
            _service.DeleteStatus(id);
            return Ok("Status excluido com sucesso");
        }
    }
}
