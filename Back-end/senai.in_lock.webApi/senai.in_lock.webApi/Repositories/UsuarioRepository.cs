using senai.in_lock.webApi.Domains;
using senai.in_lock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.in_lock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = @"Data Source=PC-GAMER-GUKEIJ\SQLEXPRESS; initial catalog=inlock_games_tarde; user Id=sa; pwd=senai@132";
        public UsuarioDomain BuscarPorEmailSenha(string senha, string email)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelect = @"SELECT U.idUsuario,
                                              U.email,
                                              U.senha,
                                              U.idTipoUsuario,
                                              T.titulo
                                         FROM USUARIO U
                                        INNER JOIN TIPOUSUARIO T
                                           ON U.idTipoUsuario = T.idTipoUsuario
                                        WHERE email = @email
                                          AND senha = @senha";
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
 
                    cmd.Parameters.AddWithValue("@senha", senha);
                    
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(reader[0]),
                            email = reader[1].ToString(),
                            senha = reader[2].ToString(),
                            idTipoUsuario = Convert.ToInt32(reader[3]),

                            tipousuario = new TipoUsuarioDomain
                            {
                                titulo = reader[4].ToString()
                            }
                        };

                        return usuarioBuscado;
                    }
                return null;
                }

            }
        }
        
    }
}
