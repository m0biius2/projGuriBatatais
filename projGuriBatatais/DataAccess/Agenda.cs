using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace projGuriBatatais.DataAccess
{
    // classe de acesso a tabela Agenda do banco
    public class Agenda
    {
        // atributos
        // privados
        private SqlConnection con; // conexao com o banco

        // publicos
        // atributos das colunas da tabela do banco
        public int idAgenda;
        public string titulo;
        public string comunicado;
        public string links;
        public string imagens;
        public string idUsuario;
        public DateTime data;
        public int idCorComunicado;

        // metodos
        // metodo construtor
        public Agenda()
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
                // comando sql
                string cmdSQL = $"Insert Into Agenda(Titulo, Comunicado, IdUsuario, Data, IdCorComunicado) " +
                                $"Values(@Titulo, @Comunicado, @IdUsuario, @Data, @IdCorComunicado)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@Titulo", SqlDbType.VarChar);
                cmd.Parameters.Add("@Comunicado", SqlDbType.VarChar);
                cmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@Data", SqlDbType.DateTime);
                cmd.Parameters.Add("@IdCorComunicado", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@Titulo"].Value = titulo;
                cmd.Parameters["@Comunicado"].Value = comunicado;
                cmd.Parameters["@IdUsuario"].Value = idUsuario;
                cmd.Parameters["@Data"].Value = data;
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

        // metodo alterar que altera os dados da tabela
        public bool Alterar()
        {
            try
            {
                // dados a seres alterados
                string cmdSQL = $"Update Agenda Set Titulo = @Titulo, Comunicado = @Comunicado, " +
                                $"Data = @Data, IdCorComunicado = @IdCorComunicado " +
                                $"Where IdAgenda = @IdAgenda";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdAgenda", SqlDbType.Int);
                cmd.Parameters.Add("@Titulo", SqlDbType.VarChar);
                cmd.Parameters.Add("@Comunicado", SqlDbType.VarChar);
                cmd.Parameters.Add("@Data", SqlDbType.DateTime);
                cmd.Parameters.Add("@IdCorComunicado", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdAgenda"].Value = idAgenda;
                cmd.Parameters["@Titulo"].Value = titulo;
                cmd.Parameters["@Comunicado"].Value = comunicado;
                cmd.Parameters["@Data"].Value = data;
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

        // metodo deletar que deleta os dados da tabela
        public bool Deletar()
        {
            try
            {
                // dados a seres deletados
                string cmdSQL = $"Delete From Agenda " +
                                $"Where IdAgenda In (@IdAgenda)";

                // prepara a conexao com o banco para identificar o comando a ser executado
                SqlCommand cmd = new SqlCommand(cmdSQL, con);

                // cria parametros dos valores das colunas
                cmd.Parameters.Add("@IdAgenda", SqlDbType.Int);

                // transforma os parametros em variaveis
                cmd.Parameters["@IdAgenda"].Value = idAgenda;

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
                string cmdSQL = "SELECT IdAgenda, Titulo, Comunicado, Usuario.NomeUsuario as Gestor, Data, " +
                                "Cor.NomeCor as Cor, Ag.IdCorComunicado as IdCor " +
                                "FROM Agenda Ag " +
                                "Join Usuario " +
                                "on Ag.IdUsuario = Usuario.IdUsuario " +
                                "Join CorComunicado Cor " +
                                "on Ag.IdCorComunicado = Cor.IdCorComunicado " +
                                "ORDER BY IdAgenda DESC";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtAgenda = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtAgenda);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtAgenda;
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
                string cmdSQL = "Select * From Agenda " +
                                "Where IdAgenda = @IdAgenda " +
                                "Order By IdAgenda";

                // busca dados do banco
                SqlDataAdapter daPesquisa = new SqlDataAdapter(cmdSQL, con);


                // cria parametros dos valores das colunas
                daPesquisa.SelectCommand.Parameters.Add("@IdAgenda", SqlDbType.Int);

                // transforma os parametros em variaveis
                daPesquisa.SelectCommand.Parameters["@IdAgenda"].Value = idAgenda;

                // abre conexao com o banco
                con.Open();

                // cria uma tabela para exibicao dos dados
                DataTable dtAgenda = new DataTable();

                // preenche a tabela para exibicao dos dados com dados do banco
                int qtdLinhasAfetadas = daPesquisa.Fill(dtAgenda);

                // fecha conexao com o banco
                con.Close();

                // retorna a tabela de exibicao
                return dtAgenda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
