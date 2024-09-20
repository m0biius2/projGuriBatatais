using System.Data;
using System.Data.SqlClient;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projGuriBatatais.DataAccess
{
    public class Usuario
    {
        private SqlConnection con;

        public int idUsuario;
        public string nomeCompleto;
        public string nomeUsuario;
        public string senha;
        public string curso;
        public string tipo;

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
                // dados a serem inseridos na tabela
                string cmdSQL = $"Insert Into Usuario(NomeCompleto, NomeUsuario, Senha, Curso, Tipo) " +
                                $"Values(@NomeCompleto, @NomeUsuario, @Senha, @Curso, @Tipo)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@Senha", SqlDbType.VarChar);
                cmd.Parameters.Add("@Curso", SqlDbType.VarChar);
                cmd.Parameters.Add("@Tipo", SqlDbType.VarChar);

                // transforma os parametros em variaveis
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                cmd.Parameters["@Senha"].Value = senha;
                cmd.Parameters["@Curso"].Value = curso;
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
                // dados a seres alterados
                string cmdSQL = $"Update Usuario Set NomeCompleto = @NomeCompleto, NomeUsuario = @NomeUsuario, " +
                                $"Curso = @Curso " +
                                $"Where IdUsuario = @IdUsuario";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@Curso", SqlDbType.VarChar);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdUsuario"].Value = idUsuario;
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                cmd.Parameters["@Curso"].Value = curso;

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
    }
}
