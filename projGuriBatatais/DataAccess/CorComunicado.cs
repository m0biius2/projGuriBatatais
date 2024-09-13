using System.Data;
using System.Data.SqlClient;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projGuriBatatais.DataAccess
{
    // classe de acesso a tabela CorComunicado do banco
    public class CorComunicado
    {
        // atributos
        // privados
        private SqlConnection con; // conexao com o banco

        // publicos
        // atributos das colunas da tabela do banco
        public int idCorComunicado;
        public string nomeCor;

        // metodos
        // metodo construtor
        public CorComunicado()
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

        // metodo inserir que insere os dados na tabela
        public bool Inserir()
        {
            try
            {
                // dados a serem inseridos na tabela
                string cmdSQL = $"Insert Into CorComunicado(NomeCor) " +
                                $"Values(@NomeCor)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@NomeCor", SqlDbType.VarChar);

                // transforma os parametros em variaveis
                cmd.Parameters["@NomeCor"].Value = nomeCor;

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
                string cmdSQL = $"Update Agenda Set NomeCor = @NomeCor " +
                                $"Where IdCorComunicado = @IdCorComunicado";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdCorComunicado", SqlDbType.Int);
                cmd.Parameters.Add("@NomeCor", SqlDbType.VarChar);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdCorComunicado"].Value = idCorComunicado;
                cmd.Parameters["@NomeCor"].Value = nomeCor;

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
                string cmdSQL = $"Delete From CorComunicado " +
                                $"Where IdCorComunicado In (@IdCorComunicado)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdCorComunicado", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdCorComunicado"].Value = idCorComunicado;

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
                string cmdSQL = "SELECT * From CorComunicado " +
                                "Order By IdCorComunicado";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtCorComunicado = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtCorComunicado);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtCorComunicado;
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
                string cmdSQL = "Select * From CorComunicado " +
                                "Where IdCorComunicado = @IdCorComunicado " +
                                "Order By IdCorComunicado";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdCorComunicado", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdCorComunicado"].Value = idCorComunicado;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtCorComunicado = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtCorComunicado);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtCorComunicado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
