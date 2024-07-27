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
    public class UsuarioController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo registro de usuario
        /// </summary>
        /// <param name="usuario">Dados do usuario que deseja cadastrar.</param>
        /// <returns>201</returns>
        [HttpPost("CadastrarUsuario")]
        public IActionResult CadastrarUsuario([FromBody] UserViewModel usuario)
        {
            var user = _mapper.Map<UserModel>(usuario);
            _service.RegisterUser(user);
            return Created();
        }

        /// <summary>
        /// Busca o registro de um usuario
        /// </summary>
        /// <param name="auth">Busca o usuario de login.</param>
        /// <returns>200</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginViewModel auth)
        {
            var authLogin = _mapper.Map<AuthModel>(auth);
            var result = _service.VerificaLogin(authLogin);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
