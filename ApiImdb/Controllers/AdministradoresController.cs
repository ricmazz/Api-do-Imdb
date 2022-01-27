using ApiImdb.Context;
using ApiImdb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiImdb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly IMDBContext _IMDBContext;

        /// <summary>
        /// Funcionalidades dos Administradores
        /// </summary>
        public AdministradoresController(IMDBContext IMDBContext)
        {
            _IMDBContext = IMDBContext;
        }

        [HttpGet]
        [Route("Administradores/GerarRelacaoAdministradores")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<Administradores> GerarRelacaoAdministradores()
        {
            return _IMDBContext.Administradores;
        }

        /// <summary>
        /// Funcionalidades para trazer todos os usuários que não são administradores.
        /// </summary>
        [HttpGet]
        [Route("Administradores/GerarRelacaoNaoAdministradores")]
        [ApiExplorerSettings]
        public IEnumerable<Usuarios> GerarRelacaoNaoAdministradores()
        {
            return _IMDBContext.Usuarios.ToArray<Usuarios>().Where(usuario => _IMDBContext.Administradores.ToList<Administradores>().Count(adm => adm.IdUsuario == usuario.IdUsuario) == 0).OrderBy(o => o.NomeUsuario);
        }

        /// <summary>
        /// Funcionalidade de Cadastro de Administrador
        /// </summary>
        /// <param name="administrador" >
        /// Recebe JSON com as propriedades NomeUsuario (string) e se o usuario esta inativo (true/false).
        /// </param>
        [HttpPost]
        [Route("Administradores/CadastroUsuario")]
        public void CadastroUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _IMDBContext.Usuarios.Add(usuario);
                _IMDBContext.SaveChanges();

                _IMDBContext.Administradores.Add(new Administradores
                {
                    IdUsuario = usuario.IdUsuario,
                    Inativo = false
                });

                _IMDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcionalidade para edição do usuário
        /// </summary>
        /// <param name="administrador"></param>
        [HttpPost]
        [Route("Administradores/AlteracaoUsuario")]
        public void AlteracaoUsuario([FromBody] Administradores administrador)
        {
            try
            {
                var item = _IMDBContext.Administradores.FirstOrDefault(x => x.IdUsuario == administrador.IdUsuario);

                if (item != null)
                {
                    item.Inativo = administrador.Inativo;
                    _IMDBContext.Administradores.Update(item);
                    _IMDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Não localizado o usuário administrador.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Funcionalidade para inativar o cadastro do usuário
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [Route("Administradores/InativarUsuario")]
        public void InativarUsuario(int id)
        {
            try
            {
                var item = _IMDBContext.Administradores.FirstOrDefault(x => x.IdUsuario == id);

                if (item != null)
                {
                    item.Inativo = true;
                    _IMDBContext.Administradores.Update(item);
                    _IMDBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Não localizado o usuário administrador.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
