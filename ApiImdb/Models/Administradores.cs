using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Models
{
    public class Administradores
    {
        /// <summary>
        /// Id do Administrador
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdministrador { get; set; }
        /// <summary>
        /// Id do Usuário
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Informa se o administrador está ativo ou inativo.
        /// </summary>
        public bool Inativo { get; set; }
    }
}
