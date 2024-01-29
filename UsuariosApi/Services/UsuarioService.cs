﻿using AutoMapper;
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
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

        public async Task<string> Login(LoginUsuarioDto usuarioDto)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioDto.UserName, usuarioDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Não foi possível logar");
            }
            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == usuarioDto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
