using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private string stringConexao = @"Data Source=PC-GAMER-GUKEIJ\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";

        public void AtualizarIdCorpo(EstudioDomain estudioAtualizado)
        {
            if (estudioAtualizado.nomeEstudio != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdateBody = "UPDATE ESTUDIO SET nomeEstudio = @nomeEstudio WHERE idEstudio = @idEstudio";
                    using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                    {
                        cmd.Parameters.AddWithValue("@nomeEstudio", estudioAtualizado.nomeEstudio);
                        cmd.Parameters.AddWithValue("@idEstudio", estudioAtualizado.idEstudio);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public EstudioDomain BuscarPorId(int idEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeEstudio, idEstudio FROM ESTUDIO WHERE idEstudio = @idEstudio";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        EstudioDomain estudioBuscado = new EstudioDomain
                        {
                            idEstudio = Convert.ToInt32(reader[1]),
                            nomeEstudio = reader[0].ToString()
                        };

                        return estudioBuscado;
                    }

                    return null;
                }
            }
        }

        public void Deletar(int idEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ESTUDIO WHERE idEstudio = @idEstudio";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idEstudio", idEstudio);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(EstudioDomain novoEstudio)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ESTUDIO (nomeEstudio) VALUES (@nomeEstudio)";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEstudio", novoEstudio.nomeEstudio);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> listaEstudios = new List<EstudioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idEstudio, nomeEstudio FROM ESTUDIO";
                con.Open();
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain
                        {
                            idEstudio = Convert.ToInt32(rdr[0]),
                            nomeEstudio = rdr[1].ToString()
                        };

                        listaEstudios.Add(estudio);
                    }
                }
            }
            return listaEstudios;

        }
    }
}