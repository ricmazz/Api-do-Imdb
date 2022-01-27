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
    public class UsuariosController : ControllerBase
    {
        private readonly IMDBContext _IMDBContext;

        public UsuariosController(IMDBContext IMDBContext)
        {
            _IMDBContext = IMDBContext;
        }

        /// <summary>
        /// Funcionalidades dos Usuários
        /// </summary>
        [HttpGet]
        [Route("Usuarios/GerarRelacaoUsuarios")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<Usuarios> GerarRelacaoUsuarios()
        {
            return _IMDBContext.Usuarios;
        }

        /// <summary>
        /// Funcionalidade de Cadastro de Usuário
        /// </summary>
        /// <param name="usuario" >
        /// Recebe JSON com as propriedades NomeUsuario (string) e se o usuario esta inativo (true/false).
        /// </param>
        [HttpPost]
        [Route("Usuarios/CadastroUsuario")]
        public void CadastroUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _IMDBContext.Usuarios.Add(usuario);
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
        /// <param name="usuario"></param>
        [HttpPost]
        [Route("Usuarios/AlteracaoUsuario")]
        public void AlteracaoUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _IMDBContext.Usuarios.Update(usuario);
                _IMDBContext.SaveChanges();
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
        [Route("Usuarios/InativarUsuario")]
        public void InativarUsuario(int id)
        {
            try
            {
                var item = _IMDBContext.Usuarios.FirstOrDefault(x => x.IdUsuario == id);

                if (item != null)
                {
                    item.Inativo = true;
                    _IMDBContext.Usuarios.Update(item);
                    _IMDBContext.SaveChanges();
                }
                else
                {
                    new Exception("Não identificado usuario para inativar.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
