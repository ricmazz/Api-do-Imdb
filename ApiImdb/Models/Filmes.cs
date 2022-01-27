using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Models
{
    public class Filmes
    {
        /// <summary>
        /// Id do Filme
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFilme { get; set; }
        /// <summary>
        /// Nome do Filme
        /// </summary>
        /// <example>Matrix</example>
        public string NomeFilme { get; set; }
        /// <summary>
        /// Genero do Filme
        /// </summary>
        /// <example>Ficção</example>
        public string Genero { get; set; }
        /// <summary>
        /// Indica quem são os atores do filme
        /// </summary>
        /// <exemple>Brad Pitt, Wagner Moura e Tom Holland</exemple>
        public string Atores { get; set; }
        /// <summary>
        /// Indica quem é o diretor(a) do filme
        /// </summary>
        /// <example>Fernando Meireles</example>
        public string Diretor { get; set; }
    }
}
