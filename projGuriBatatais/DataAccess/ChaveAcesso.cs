using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projGuriBatatais.DataAccess
{
    public class ChaveAcesso
    {
        private SqlConnection con;

        public int idChaveAcesso;
        public string chaveCoordenacao;
        public string chaveProfessor;
        public string chaveAluno;

        public ChaveAcesso()
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

        // metodo que seleciona os dados da tabela
        public DataTable SelecionarTodos()
        {
            try
            {
                // dados a serem selecionados
                string cmdSQL = "SELECT * From ChaveAcesso";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtChave = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtChave);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtChave;
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
                string cmdSQL = "Select * From ChaveAcesso " +
                                "Where IdChaveAcesso = @IdChaveAcesso " +
                                "Order By IdChaveAcesso";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdChaveAcesso", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdChaveAcesso"].Value = idChaveAcesso;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtChave = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtChave);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtChave;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
