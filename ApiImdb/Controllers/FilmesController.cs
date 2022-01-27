using ApiImdb.Context;
using ApiImdb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiImdb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IMDBContext _IMDBContext;

        public FilmesController(IMDBContext IMDBContext)
        {
            _IMDBContext = IMDBContext;
        }

        /// <summary>
        /// Funcionalidade para cadastrar um filme novo
        /// </summary>
        /// <param name="idUsuario">Id do Usuario</param>
        /// <param name="filme">Filme a cadastrar</param>
        [HttpPost]
        [Route("Filmes/CadastrarFilme")]
        public void CadastrarFilme(int idUsuario, [FromBody] Filmes filme)
        {
            try
            {
                var item = _IMDBContext.Administradores.FirstOrDefault(x => x.IdUsuario == idUsuario && !x.Inativo);

                if (item != null)
                {
                    _IMDBContext.Filmes.Add(filme);
                    _IMDBContext.SaveChanges();
                }
                else
                {
                    new Exception("Usuário não é administrador, obrigatório ser administrador para poder incluir um filme.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcionalidade para votar em um filme
        /// </summary>
        /// <param name="voto">Objeto do Tipo Votacoes</param>
        [HttpPost]
        [Route("Filmes/VotarFilme")]
        public void CadastrarFilme([FromBody] Votacoes voto)
        {
            try
            {
                if (voto.Voto < 0 || voto.Voto > 4)
                {
                    throw new Exception("Nota inválida, deve ser entre 0 e 4.");
                }

                _IMDBContext.Votacoes.Add(voto);
                _IMDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Funcionalidade para gerar relação de votação de filme com filtros.
        /// </summary>
        /// <param name="tipoFiltro">Tipo de filtro que será utilizado</param>
        /// <param name="diretor">Filtro de diretores do filme</param>
        /// <param name="nome">Filtro de nome do filme</param>
        /// <param name="genero">Filuro de genero do filme</param>
        /// <param name="atores">Filtro de atores do filme.</param>
        [HttpGet]
        [Route("Filmes/ListarFilmes")]
        public string ListarFilmes(string tipoFiltro, string diretor, string nome, string genero, string atores)
        {
            try
            {
                List<JObject> listRetorno = new List<JObject>();
                List<Filmes> listFilmes = _IMDBContext.Filmes.ToList<Filmes>();


                if (!String.IsNullOrWhiteSpace(tipoFiltro))
                {
                    switch (tipoFiltro)
                    {
                        case "D":
                            if (!String.IsNullOrWhiteSpace(diretor))
                            {
                                listFilmes = listFilmes.Where(filme => filme.Diretor == diretor).ToList<Filmes>();
                            }
                            break;
                        case "N":
                            if (!String.IsNullOrWhiteSpace(nome))
                            {
                                listFilmes = listFilmes.Where(filme => filme.NomeFilme == nome).ToList<Filmes>();
                            }
                            break;
                        case "G":
                            if (!String.IsNullOrWhiteSpace(genero))
                            {
                                listFilmes = listFilmes.Where(filme => filme.Genero == genero).ToList<Filmes>();
                            }
                            break;
                        case "A":
                            if (!String.IsNullOrWhiteSpace(atores))
                            {
                                listFilmes = listFilmes.Where(filme => filme.Atores == atores).ToList<Filmes>();
                            }
                            break;
                    }
                }

                if (listFilmes.Count > 0)
                {
                    List<Votacoes> listVotacoes = _IMDBContext.Votacoes.ToList<Votacoes>();
                    List<Votacoes> listResultadoVotacoes = new List<Votacoes>();
                    listFilmes = listFilmes.OrderBy(filme => filme.NomeFilme).Distinct<Filmes>().ToList<Filmes>();

                    foreach (Filmes filme in listFilmes)
                    {
                        decimal mediaVotos = decimal.Zero;
                        JObject objeto = new JObject();

                        if (listVotacoes.Where(voto => voto.IdFilme == filme.IdFilme).ToList<Votacoes>().Count > 0)
                        {
                            mediaVotos = listVotacoes.Where(voto => voto.IdFilme == filme.IdFilme).Sum(voto => voto.Voto) / listVotacoes.Where(voto => voto.IdFilme == filme.IdFilme).ToList<Votacoes>().Count();
                        }

                        objeto.Add("IdFilme", filme.IdFilme);
                        objeto.Add("NomeFilme", filme.NomeFilme);
                        objeto.Add("Diretor", filme.Diretor);
                        objeto.Add("Genero", filme.Genero);
                        objeto.Add("Atores", filme.Atores);
                        objeto.Add("Media", mediaVotos);

                        listRetorno.Add(objeto);
                    }

                    listRetorno = listRetorno.OrderBy(obj => obj.GetValue("Media").ToString()).ThenBy(obj => obj.GetValue("NomeFilme").ToString()).ToList<JObject>();
                }


                return Newtonsoft.Json.JsonConvert.SerializeObject(listRetorno);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
