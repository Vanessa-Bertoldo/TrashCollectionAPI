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
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service;
        private readonly IMapper _mapper;

        public ColetaController(IColetaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ColetaViewModel>> Get()
        {
            var coletas = _service.GetAllColetas();
            var viewModelList = _mapper.Map<IEnumerable<ColetaViewModel>>(coletas);
            return Ok(viewModelList);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ColetaViewModel viewModel)
        {
            var coleta = _mapper.Map<ColetaModel>(viewModel);
            if (coleta != null)
            {
                _service.AddNewColeta(coleta);
                return CreatedAtAction(nameof(Get), new { id = coleta.IdColeta }, viewModel);
            }
            return NotFound();
        }


    }
}
