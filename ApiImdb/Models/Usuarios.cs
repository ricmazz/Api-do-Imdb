using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Models
{
    /// <summary>
    /// Modelo do Cadastro de Usuários
    /// </summary>
    public class Usuarios
    {
        /// <summary>
        /// Id do Usuário
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        /// <example>Ricardo</example>
        public string NomeUsuario { get; set; }
        /// <summary>
        /// Indica se o usuário está ativo.
        /// </summary>
        /// <example>false</example>
        public bool Inativo { get; set; }
    }
}
