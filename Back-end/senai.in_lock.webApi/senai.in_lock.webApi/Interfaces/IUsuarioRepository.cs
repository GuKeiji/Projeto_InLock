using senai.in_lock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> ListarTodos();
        UsuarioDomain BuscarPorId(int idUsuario);
        void Deletar(int idUsuario);
        void AtualizarIdCorpo(UsuarioDomain usuarioAtualizado);
        void Inserir(UsuarioDomain novoUsuario);
    }
}
