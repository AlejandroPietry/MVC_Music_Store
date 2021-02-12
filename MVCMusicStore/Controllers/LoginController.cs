﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MVCMusicStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System;

namespace MVCMusicStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //usuario logado ao pode logar dnv :)
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuario.Login == "alevrauvrau" && usuario.Senha == "ale123")
                    {
                        Login(usuario);
                        RedirectToAction("Index", "Home");
                    }
                    else
                        ViewBag.Erro = "Usuario ou senha incorretos";
                }
            }            
            catch(Exception)
            {
                ViewBag.Erro = "Ocorreu um erro ao tentar se logar! Tente novamente dentro de alguns meses!";
            }
            return View();
        }

        private async void Login(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Usuario_Comum")
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);

            /*
             Primeiro criamos uma lista de Claims, que é onde nós iremos atribuir as informações do usuário.
            Depois criamos uma ClaimsIdentity, que define o tempo de autenticação.
            Então nós criamos a ClaimPrincipal que é a junção das duas anteriores que será passado como parâmetro para autenticação.
            Após isso criei um AuthenticationProperties que é onde contém as propriedades de autenticação contendo algumas informações de persistência de dados e afins, entre elas, tempo de expiração, dados persistentes após fechamento do navegador e continuará autenticado mesmo se atualizar a página.
            E por fim nós autenticamos utilizando as informações da ClaimPrincipal e as configurações de AuthenticationProperties.
             */
        }
    }
}