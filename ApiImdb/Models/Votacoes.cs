using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Models
{
    public class Votacoes
    {
        /// <summary>
        /// Id do Voto
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVoto { get; set; }
        /// <summary>
        /// Id do Usuário que votou
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Id do filme votado
        /// </summary>
        public int IdFilme { get; set; }
        /// <summary>
        /// Valor do voto
        /// </summary>
        /// <example>Voto é de zero a quatro [ 0 - 4 ]</example>
        public int Voto { get; set; }
    }
}
