using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task Cadastro(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityResult result = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Não foi possível cadastrar o usuário");
            }
        }
        
        public async Task Login(LoginUsuarioDto usuarioDto)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDto.UserName, usuarioDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Não foi possível logar");
            }
        }
    }
}
