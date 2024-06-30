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
            var Caminhao = _service.GetCaminhaoById(Id);
            if (Caminhao != null)
            {
                var viewModel = _mapper.Map<CaminhaoViewModel>(Caminhao);
                return Ok(viewModel);
            }
            return BadRequest();
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

    }
}
