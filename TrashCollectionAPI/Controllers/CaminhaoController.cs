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
    public class CaminhaoController : ControllerBase
    {
        private readonly ICaminhaoService _service;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;


        public CaminhaoController(ICaminhaoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca todas os caminhoes.
        /// </summary>
        /// <returns>Uma ação que retorna uma lista de CaminhaoViewModel.</returns>
        [HttpGet("BuscarTodosCaminhoes")]
        public ActionResult<IEnumerable<CaminhaoViewModel>> BuscaTodosCaminhoes()
        {
            var Caminhoes = _service.GetAllCaminhoes();
            if (Caminhoes != null)
            {
                var viewModelList = _mapper.Map<IEnumerable<CaminhaoViewModel>>(Caminhoes);
                return Ok(viewModelList);
            }
            return BadRequest();
        }

        /// <summary>
        /// Busca um caminhao com base do id fornecido.
        /// </summary>
        /// <param name="Id">id do caminhao que deseja buscar.</param>
        /// <returns>Retorna um caminhao.</returns>
        [HttpGet("BuscarCaminhao{Id}")]
        public ActionResult<CaminhaoViewModel> BuscarCaminhao([FromRoute] int Id)
        {
            if (Id < 0)
            {
                return BadRequest("Id não pode ser negativo.");
            }

            var Caminhao = _service.GetCaminhaoById(Id);
            if (Caminhao == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CaminhaoViewModel>(Caminhao);
            return Ok(viewModel);
        }

        /// <summary>
        /// Cadastra um caminhao.
        /// </summary>
        /// <param name="ViewModel">Dados para criar um registro de um novo caminhão.</param>
        /// <returns>Retorna o caminhao criado.</returns>
        [HttpPost("CadastrarCaminhao")]
        public ActionResult CadastrarCaminhao([FromBody] CaminhaoViewModel ViewModel)
        {
            var Caminhao = _mapper.Map<CaminhaoModel>(ViewModel);   
            _service.AddNewCaminhao(Caminhao);
            return CreatedAtAction(nameof(BuscaTodosCaminhoes), new { id = Caminhao.IdCaminhao }, ViewModel);
        }

        /// <summary>
        /// Exclui um caminhao com base do id fornecido.
        /// </summary>
        /// <param name="id">id do caminhao que deseja excluir.</param>
        /// <returns>200</returns>
        [HttpDelete("DeletarCaminhao{Id}")]
        public ActionResult DeletarCaminhao([FromBody] int id)
        {
            _service.DeleteCaminhao(id);
            return Ok("Caminhao excluido com sucesso");
        }

        /// <summary>
        /// Atualiza os dados de um caminhao com base do id fornecido.
        /// </summary>
        /// <param name="caminhao">Dados do caminhao do tipo CaminhaoViewModel.</param>
        /// <returns>200</returns>
        [HttpPut]
        public ActionResult AtualizarCaminhao([FromBody] CaminhaoViewModel caminhao)
        {
            var caminhaoModel = _mapper.Map<CaminhaoModel>(caminhao);
            _service.UpdateCaminhao(caminhaoModel);
            return Ok("Os dados foram atualizados com sucesso");
        }
    }
}
