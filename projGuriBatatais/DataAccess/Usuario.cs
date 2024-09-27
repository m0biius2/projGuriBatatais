using System.Data;
using System.Data.SqlClient;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projGuriBatatais.DataAccess
{
    public class Usuario
    {
        private SqlConnection con;

        DataTable m_TabUsuarios = new DataTable();

        public int idUsuario;
        public string nomeCompleto;
        public string nomeUsuario;
        public string senha;
        public string curso;
        public string tipo;
        ChaveAcesso o_ChaveAcesso = new ChaveAcesso();

        public Usuario()
        {
            try
            {
                // string de conexao
                string strCon;

                // configuracao que puxa a string de conexao do banco de dados pelo arquivo de configuracao, prepara para ler o arquivo
                IConfigurationRoot o_Config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@".\Configurations\GuriBatatais.json")
                    .Build();

                // string de conexao conectada com o banco pelo arquivo de configuracao desejado, le o arquivo
                strCon = o_Config.GetConnectionString(@"StringConexaoSQLServer");

                // prepara a conexao pela variavel con
                con = new SqlConnection(strCon);

                m_TabUsuarios.Columns.Add("IdUsuario", typeof(int));
                m_TabUsuarios.Columns.Add("NomeCompleto", typeof(string));
                m_TabUsuarios.Columns.Add("NomeUsuario", typeof(string));
                m_TabUsuarios.Columns.Add("Senha", typeof(string));
                m_TabUsuarios.Columns.Add("Curso", typeof(string));
                m_TabUsuarios.Columns.Add("Tipo", typeof(string));
                m_TabUsuarios.Columns.Add("ChaveAcesso", typeof(string));
            }
            catch (Exception ex)
            {
                // mensagem de erro
                throw new Exception(ex.Message);
            }
        }

        public bool Inserir()
        {
            try
            {
                string cmdSQL;

                if(curso != null)
                {
                    // dados a serem inseridos na tabela
                    cmdSQL = $"Insert Into Usuario(NomeCompleto, NomeUsuario, Senha, Curso, Tipo) " +
                             $"Values(@NomeCompleto, @NomeUsuario, @Senha, @Curso, @Tipo)";
                } else
                {
                    // dados a serem inseridos na tabela
                    cmdSQL = $"Insert Into Usuario(NomeCompleto, NomeUsuario, Senha, Tipo) " +
                             $"Values(@NomeCompleto, @NomeUsuario, @Senha, @Tipo)";
                }


                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@Senha", SqlDbType.VarChar);
                if (curso != null)
                {
                    cmd.Parameters.Add("@Curso", SqlDbType.VarChar);
                }
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar);

                // transforma os parametros em variaveis
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                cmd.Parameters["@Senha"].Value = senha;
                if(curso != null)
                {
                    cmd.Parameters["@Curso"].Value = curso;
                }
                cmd.Parameters["@Tipo"].Value = tipo;

                // abre conexao com o banco
                con.Open();

                // executa o comando identificado anteriormente
                cmd.ExecuteNonQuery();

                // fecha conexao com o banco
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // metodo alterar que altera os dados da tabela
        public bool Alterar()
        {
            try
            {
                string cmdSQL;

                if(curso != null)
                {
                    // dados a seres alterados
                    cmdSQL = $"Update Usuario Set NomeCompleto = @NomeCompleto, NomeUsuario = @NomeUsuario, " +
                             $"Curso = @Curso " +
                             $"Where IdUsuario = @IdUsuario";
                } else
                {
                    // dados a seres alterados
                    cmdSQL = $"Update Usuario Set NomeCompleto = @NomeCompleto, NomeUsuario = @NomeUsuario " +
                             $"Where IdUsuario = @IdUsuario";
                }

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);
                if(curso != null)
                {
                    cmd.Parameters.Add("@Curso", SqlDbType.VarChar);
                }

                // transforma os parametros em variaveis
                cmd.Parameters["@IdUsuario"].Value = idUsuario;
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                if(curso != null)
                {
                    cmd.Parameters["@Curso"].Value = curso;
                }

                // abre conexao com o banco
                con.Open();

                // executa o comando identificado anteriormente
                cmd.ExecuteNonQuery();

                // fecha conexao com o banco
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // metodo deletar que deleta os dados da tabela
        public bool Deletar()
        {
            try
            {
                // dados a seres deletados
                string cmdSQL = $"Delete From User " +
                                $"Where IdUsuario In (@IdUsuario)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdUsuario"].Value = idUsuario;

                // abre conexao com o banco
                con.Open();

                // executa o comando identificado anteriormente
                cmd.ExecuteNonQuery();

                // fecha conexao com o banco
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // metodo que seleciona os dados da tabela
        public DataTable SelecionarTodos()
        {
            try
            {
                // dados a serem selecionados
                string cmdSQL = "SELECT * From Usuario";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtUser = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtUser);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // metodo que seleciona os dados da tabela por id
        public DataTable SelecionarPorId()
        {
            try
            {
                // dados a serem selecionados
                string cmdSQL = "Select * From Usuario " +
                                "Where IdUsuario = @IdUsuario " +
                                "Order By IdUsuario";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdUsuario", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdUsuario"].Value = idUsuario;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtUser = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtUser);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ValidarLogin()
        {
            try
            {
                // dados a serem selecionados
                string cmdSQL = "Select * From Usuario " +
                                "Where NomeUsuario = @NomeUsuario AND Senha = @Senha " +
                                "Order By IdUsuario";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdUsuario", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdUsuario"].Value = idUsuario;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtUser = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtUser);

                if(qtdLinhasAfetadas > 0)
                {
                    return dtUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ValidarNomeUsuario(string nomeUsuario)
        {
            try
            {
                // Comando SQL para buscar pelo NomeUsuario
                string cmdSQL = "SELECT * FROM Usuario " +
                                "WHERE NomeUsuario = @NomeUsuario " +
                                "ORDER BY IdUsuario";

                // Cria o adaptador para realizar a busca no banco de dados
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // Adiciona o parâmetro @NomeUsuario
                daPesquisa.SelectCommand.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);

                // Define o valor do parâmetro como o nome do usuário
                daPesquisa.SelectCommand.Parameters["@NomeUsuario"].Value = nomeUsuario;

                // Abre a conexão com o banco de dados
                con.Open();

                // Cria uma tabela para armazenar os dados retornados
                DataTable dtUser = new DataTable();

                // Preenche a tabela com os dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtUser);

                // Fecha a conexão com o banco
                con.Close();

                // Se encontrou algum resultado, retorna a tabela
                if (qtdLinhasAfetadas > 0)
                {
                    return dtUser;
                }
                else
                {
                    return null; // Retorna null se o nome do usuário não existir
                }
            }
            catch (Exception ex)
            {
                // Trata qualquer exceção que ocorrer
                throw new Exception(ex.Message);
            }
        }
    }
}
