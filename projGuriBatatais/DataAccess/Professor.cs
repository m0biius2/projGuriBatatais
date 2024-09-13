using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace projGuriBatatais.DataAccess
{
    // classe de acesso a tabela Professor do banco
    public class Professor
    {
        // atributos
        // privados
        private SqlConnection con; // conexao com o banco

        // publicos
        // atributos das colunas da tabela do banco
        public int idProfessor;
        public string nomeCompleto;
        public string nomeUsuario;
        public string senha;
        public bool cGraves;
        public bool cAgudas;
        public bool metais;
        public bool madeiras;
        public bool percussao;
        public bool coral;

        // metodos
        // metodo construtor
        public Professor()
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
                string cmdSQL = $"Insert Into Professor(NomeCompleto, NomeUsuario, Senha, " +
                                $"CGraves, CAgudas, Metais, Madeiras, Percussao, Coral) " +
                                $"Values(@NomeCompleto, @NomeUsuario, @Senha" +
                                $"@CGraves, @CAgudas, @Metais, @Madeiras, @Percussao, @Coral)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar);
                cmd.Parameters.Add("@Senha", SqlDbType.VarChar);
                cmd.Parameters.Add("@CGraves", SqlDbType.Int);
                cmd.Parameters.Add("@CAgudas", SqlDbType.Int);
                cmd.Parameters.Add("@Metais", SqlDbType.Int);
                cmd.Parameters.Add("@Madeiras", SqlDbType.Int);
                cmd.Parameters.Add("@Percussao", SqlDbType.Int);
                cmd.Parameters.Add("@Coral", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                cmd.Parameters["@Senha"].Value = senha;
                cmd.Parameters["@CGraves"].Value = cGraves;
                cmd.Parameters["@CAgudas"].Value = cAgudas;
                cmd.Parameters["@Metais"].Value = metais;
                cmd.Parameters["@Madeiras"].Value = madeiras;
                cmd.Parameters["@Percussao"].Value = percussao;
                cmd.Parameters["@Coral"].Value = coral;

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
                string cmdSQL = $"Update Professor Set NomeCompleto = @NomeCompleto, NomeUsuario = @NomeUsuario, Senha = @Senha, " +
                                $"CGraves = @CGraves, CAgudas = @CAgudas, Metais = @Metais, Madeiras = @Madeiras, Percussao = @Percussao, Coral = @Coral " +
                                $"Where IdProfessor = @IdProfessor";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdProfessor", SqlDbType.Int);
                cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar);
                cmd.Parameters.Add("@Senha", SqlDbType.VarChar);
                cmd.Parameters.Add("@CGraves", SqlDbType.Int);
                cmd.Parameters.Add("@CAgudas", SqlDbType.Int);
                cmd.Parameters.Add("@Metais", SqlDbType.Int);
                cmd.Parameters.Add("@Madeiras", SqlDbType.Int);
                cmd.Parameters.Add("@Percussao", SqlDbType.Int);
                cmd.Parameters.Add("@Coral", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdProfessor"].Value = idProfessor;
                cmd.Parameters["@NomeCompleto"].Value = nomeCompleto;
                cmd.Parameters["@NomeUsuario"].Value = nomeUsuario;
                cmd.Parameters["@Senha"].Value = senha;
                cmd.Parameters["@CGraves"].Value = cGraves;
                cmd.Parameters["@CAgudas"].Value = cAgudas;
                cmd.Parameters["@Metais"].Value = metais;
                cmd.Parameters["@Madeiras"].Value = madeiras;
                cmd.Parameters["@Percussao"].Value = percussao;
                cmd.Parameters["@Coral"].Value = coral;

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
                string cmdSQL = $"Delete From Professor " +
                                $"Where IdProfessor In (@IdProfessor)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdProfessor", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdProfessor"].Value = idProfessor;

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
                string cmdSQL = "SELECT IdProfessor, NomeCompleto, NomeUsuario, Senha, " +
                                "CGraves, CAgudas, Metais, Madeiras, Percussao, Coral";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtProfessor = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtProfessor);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtProfessor;
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
                string cmdSQL = "Select * From Professor " +
                                "Where IdProfessor = @IdProfessor " +
                                "Order By IdProfessor";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdProfessor", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdProfessor"].Value = idProfessor;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtProfessor = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtProfessor);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtProfessor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
