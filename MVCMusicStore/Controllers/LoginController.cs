using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMusicStore.Code.Util;
using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MVCMusicStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly MusicStoreEntities _context;
        public LoginController(MusicStoreEntities context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //usuario logado nao pode logar dnv :)
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _context.Tab_Usuario.FirstOrDefaultAsync(x => x.Email == userLogin.Email &&
                       x.Password == userLogin.Password).Result;
                    if (usuario != null)
                    {
                        var lastHeartBeat = _context.OnlineLogs.Where(x => x.IdUsuario == usuario.UsuarioId).OrderByDescending(x => x.LastHeartBeat).FirstOrDefault();

                        if(lastHeartBeat != null)
                        {
                            if (lastHeartBeat.IsOnlineNow())
                            {
                                ViewBag.Erro = "Usuario ja esta logado em outra maquina.";
                                return View();
                            }
                        }

                        Login(usuario);
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.Erro = "Usuario ou senha incorretos";
                }
            }
            catch (Exception e)
            {
                ViewBag.Erro = "Ocorreu um erro ao tentar se logar! Tente novamente dentro de alguns meses!";
            }
            return View();
        }

        [Route("CriarUsuario")]
        public IActionResult CreateUser()
        {
            return View("CreateNewUser");
        }

        [HttpPost]
        [Route("CriarUsuario")]
        public IActionResult CreateUser(Usuario usuario)
        {
            Usuario userOnDb = _context.Tab_Usuario.FirstOrDefault(x => x.Email == usuario.Email);

            if (userOnDb != null)
                return View("CreateNewUser", usuario);
            if (!ModelState.IsValid)
                return View("CreateNewUser", usuario);

            usuario.Login = usuario.Email;
            _context.Tab_Usuario.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private async void Login(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, "Usuario_Comum"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.SerialNumber, usuario.UsuarioId.ToString())
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Email");

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


        public void SalvarHeartBeat()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var a = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.SerialNumber);

                OnlineLog onlineLog = new OnlineLog
                {
                    IdUsuario = int.Parse(a.Value),
                    LastHeartBeat = DateTime.Now
                };

                _context.OnlineLogs.Add(onlineLog);
                _context.SaveChanges();
            }
        }
    }
}
