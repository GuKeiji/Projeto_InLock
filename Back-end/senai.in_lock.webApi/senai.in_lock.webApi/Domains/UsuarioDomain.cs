using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Domains
{
    public class UsuarioDomain
    {
        public int idUsuario { get; set; }
        public int idTipoUsuario { get; set; }
        [Required(ErrorMessage = "Informe o e-mail")]
        public string email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O campo senha precisa ter no mínimo 5 e no máximo 20 caracteres.")]
        public string senha { get; set; }
        public TipoUsuarioDomain tipousuario { get; set; }
    }
}
