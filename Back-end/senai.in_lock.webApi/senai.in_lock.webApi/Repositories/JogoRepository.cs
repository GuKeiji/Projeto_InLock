using senai.in_lock.webApi.Domains;
using senai.in_lock.webApi.Interfaces;
using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private string stringConexao = @"Data Source=PC-GAMER-GUKEIJ\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdCorpo(JogoDomain jogoAtualizado)
        {
            if (jogoAtualizado.nomeJogo != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = @"UPDATE JOGO SET nomeJogo = @nomeJogo, 
                                                               descricao = @descricao, 
                                                               dataLancamento = @dataLancamento, 
                                                               valor = @valor, 
                                                               idEstudio = @idEstudio 
                                                         WHERE idJogo = @idJogo";
                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                        cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricao);
                        cmd.Parameters.AddWithValue("@dataLancamento", jogoAtualizado.dataLancamento);
                        cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valor);
                        cmd.Parameters.AddWithValue("@idEstudio", jogoAtualizado.idEstudio);
                        cmd.Parameters.AddWithValue("@idJogo", jogoAtualizado.idJogo);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = @"SELECT idJogo,
                                                  nomeJogo,
                                                  descricao,
                                                  dataLancamento,
                                                  valor,
                                                  idEstudio
                                             FROM JOGO
                                            WHERE idJogo = @idJogo";
                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(reader[0]),
                            nomeJogo = reader[1].ToString(),
                            descricao = reader[2].ToString(),
                            dataLancamento = Convert.ToDateTime(reader[3]),
                            valor = Convert.ToInt32(reader[4]),
                            idEstudio = Convert.ToInt32(reader[5])
                        };

                        return jogoBuscado;
                    }

                    return null;
                }
            }
        }

        public void Deletar(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM JOGO WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("idJogo", idJogo);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO JOGO (nomeJogo, descricao, dataLancamento, valor, idEstudio) VALUES (@nomeJogo, @descricao, @dataLancamento, @valor, @idEstudio)";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> listaJogos = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll =   @"SELECT J.idJogo, 
	                                               J.nomeJogo [Nome do Jogo], 
	                                               J.descricao, 
	                                               J.dataLancamento [Data de Lançamento], 
	                                               J.valor, 
	                                               E.nomeEstudio 
                                              FROM JOGO J
                                             INNER JOIN ESTUDIO E
                                                ON J.idEstudio = E.idEstudio";
                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(rdr[0]),
                            nomeJogo = rdr[1].ToString(),
                            descricao = rdr[2].ToString(),
                            dataLancamento = Convert.ToDateTime(rdr[3]),
                            valor = Convert.ToInt32(rdr[4]),

                            estudio = new EstudioDomain
                            {
                                nomeEstudio = rdr[5].ToString()
                            }
                        };

                        listaJogos.Add(jogo);
                    }
                }
            }
            return listaJogos;
        }
    }
}
