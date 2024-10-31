using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace projGuriBatatais.DataAccess
{
    public class Arquivo
    {
        // atributos
        // privados
        private SqlConnection con; // conexao com o banco

        // publicos
        // atributos das colunas da tabela do banco
        public int idArquivo;
        public string caminho;
        public int idUsuario;
        public DateTime data;

        // metodos
        // metodo construtor
        public Arquivo()
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
                string cmdSQL = $"Insert Into Arquivo(Caminho, IdUsuario, Data) " +
                                $"Values(@Caminho, @IdUsuario, @Data)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@Caminho", SqlDbType.VarChar);
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@Data", SqlDbType.DateTime);

                // transforma os parametros em variaveis
                cmd.Parameters["@Caminho"].Value = caminho;
                cmd.Parameters["@IdUsuario"].Value = idUsuario;
                cmd.Parameters["@Data"].Value = data;

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
                string cmdSQL = $"Update Arquivo Set Caminho = @Caminho, IdUsuario = @IdUsuario, Data = @Data " +
                                $"Where IdArquivo = @IdArquivo";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdArquivo", SqlDbType.Int);
                cmd.Parameters.Add("@Caminho", SqlDbType.VarChar);
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@Data", SqlDbType.DateTime);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdArquivo"].Value = idArquivo;
                cmd.Parameters["@Caminho"].Value = caminho;
                cmd.Parameters["@IdUsuario"].Value = idUsuario;
                cmd.Parameters["@Data"].Value = data;

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
                string cmdSQL = $"Delete From Arquivo " +
                                $"Where IdArquivo In (@IdArquivo)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdArquivo", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdArquivo"].Value = idArquivo;

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
                string cmdSQL = "SELECT * FROM Arquivo";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                //// cria parametros dos valores das colunas
                //daPesquisa.SelectCommand.Parameters.Add("@IdUsuario", SqlDbType.Int);

                //// transforma os parametros em variaveis
                //daPesquisa.SelectCommand.Parameters["@IdUsuario"].Value = idUsuario;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtArquivo = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtArquivo);

                // fecha conexao com o banco
                con.Close();

                if(qtdLinhasAfetadas > 0 )
                {
                    // retorna a tabela de exibicao
                    return dtArquivo;
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

        // metodo que seleciona os dados da tabela por id
        public DataTable SelecionarPorId()
        {
            try
            {
                // dados a serem selecionados
                string cmdSQL = "Select * From Arquivo " +
                                "Where IdArquivo = @IdArquivo " +
                                "Order By IdArquivo";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdArquivo", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdArquivo"].Value = idArquivo;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtArquivo = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtArquivo);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtArquivo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
