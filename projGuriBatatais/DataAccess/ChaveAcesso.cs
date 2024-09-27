using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using projGuriBatatais.Models;

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
        public List<ChaveAcessoViewModel> SelecionarTodos()
        {
            try
            {
                string cmdSQL = "SELECT * From ChaveAcesso";
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);
                DataTable dtChave = new DataTable();

                con.Open();
                daPesquisa.Fill(dtChave);
                con.Close();

                // Converter DataTable para List<ChaveAcessoModel>
                List<ChaveAcessoViewModel> listaChaves = new List<ChaveAcessoViewModel>();
                foreach (DataRow row in dtChave.Rows)
                {
                    listaChaves.Add(new ChaveAcessoViewModel
                    {
                        IdChaveAcesso = (int)row["IdChaveAcesso"],
                        ChaveCoordenacao = row["ChaveCoordenacao"].ToString(),
                        ChaveProfessor = row["ChaveProfessor"].ToString(),
                        ChaveAluno = row["ChaveAluno"].ToString()
                    });
                }

                return listaChaves;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
