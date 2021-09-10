using senai.in_lock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Interfaces
{
    interface IJogoRepository
    {
        List<JogoDomain> ListarTodos();
        JogoDomain BuscarPorId(int idJogo);
        void Deletar(int idJogo);
        void AtualizarIdCorpo(JogoDomain jogoAtualizado);
        void Inserir(JogoDomain novoJogo);
    }
}
