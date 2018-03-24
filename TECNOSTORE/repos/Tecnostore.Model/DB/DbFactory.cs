using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tecnostore.Model.DB.Model;
using Tecnostore.Model.DB.Repository;

namespace Tecnostore.Model.DB
{
    public class DbFactory
    {
        private static DbFactory _instance = null;

        private ISessionFactory _sessionFactory;

        public UserRepository UserRepository { get; set; }
        public ClienteRepository ClienteRepository { get; set; }
        public EnderecoRepository EnderecoRepository { get; set; }

        private DbFactory()
        {
            Conexao();

            this.UserRepository = new UserRepository(this.Session);
            this.ClienteRepository = new ClienteRepository(this.Session);
            this.EnderecoRepository = new EnderecoRepository(this.Session);

        }

        public static DbFactory Instance // me retortna a instancia do objeto
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbFactory();
                }

                return _instance;
            }
        }

        private void Conexao()
        {
            try
            {
                var server = "localhost";
                var port = "3306";
                var dbName = "db_Tecnostore";
                var user = "root";
                var psw = "247845";

                var stringConexao = "Persist Security Info=False;" +
                                    "server=" + server +
                                    ";port=" + port +
                                    ";database=" + dbName +
                                    ";uid=" + user +
                                    ";pwd=" + psw;

                try
                {
                    var mysql = new MySqlConnection();
                    mysql.Open(); 

                    if (mysql.State == ConnectionState.Open)
                    {
                        mysql.Close();
                    }
                }

                catch
                {
                    
                    CriarSchema(server, port, dbName, psw, user);
                }

                ConfigurarNHibernate(stringConexao);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Nao foi possivel conectar", ex);
            }
        }

        private void CriarSchema(string server, string port, string dbName, string psw, string user)
        {
            
            try
            {
                var stringConexao = "server=" + server +
                   ";user=" + user +
                   ";port=" + port +
                   ";password=" + psw + ";";

                var mySql = new MySqlConnection(stringConexao);
                var cmd = mySql.CreateCommand(); // vai tentar conectar com o mysql, nao com um banco especifico

                mySql.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `" + dbName + "`;";
                cmd.ExecuteNonQuery();
                mySql.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Não deu para criar o Schema!", ex);
            }
        }

        private void ConfigurarNHibernate(string stringConexao)
        {
            try
            {
                var config = new Configuration(); // using nHibernate config

                //Configuração do NHibernate

                config.DataBaseIntegration(i =>
                {
                    //Dialeto do banco de dados
                    i.Dialect<NHibernate.Dialect.MySQLDialect>();
                    // conexao string
                    i.ConnectionString = stringConexao;
                    // Drive de conexao com o banco
                    i.Driver<NHibernate.Driver.MySqlDataDriver>();
                    // Provedor de conexao
                    i.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    // GERA LOG DOS SQL EXECUTADOS NO CONSOLE
                    i.LogSqlInConsole = true;
                    // DESCONECTAR CASO QUEIRA VISUALIZAR O LOG DE SQL FORMATADO NO CONSOLE
                    i.LogFormattedSql = true;
                    // CRIA O SCHEMA DO BANCO DE DADOS SEMPRE QUE A CONFIGURATION FOR UTILIZADO
                    i.SchemaAction = SchemaAutoAction.Update;

                });

                //Realiza o mapeamento das classes, todas as classess que foram criadas vao para o NHibernate
                var maps = this.Mapeamento();
                config.AddMapping(maps);

                //Para verificar se a aplicação é Descktop ou Web
                // antes disso coloca a referencia no seu projeto de baixo
                // referencias - Add referencia - Assembly = Sistem.Web

                if (HttpContext.Current == null)
                {
                    config.CurrentSessionContext<ThreadStaticSessionContext>();
                }

                else
                {
                    config.CurrentSessionContext<WebSessionContext>();
                }

                this._sessionFactory = config.BuildSessionFactory();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel configurar o NHibernate", ex);
            }
        }

        private HbmMapping Mapeamento()
        {
            
            try
            {
                var mapper = new ModelMapper();

                // Para o NHibernate com apenas um objeto(Uma classe, nesse cado so uma: Esporte ou outra)
                // mapper.AddMapping(EsporteMap);



                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(UserMap)).GetTypes()
                    );
                return mapper.CompileMappingForAllExplicitlyAddedEntities();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar o mapeamento do modelo!!!", ex);
            }

        }

        public ISession Session // USING DO NHIBERNATE
        {
            get
            {
                try
                {
                    if (CurrentSessionContext.HasBind(_sessionFactory))
                        return _sessionFactory.GetCurrentSession();

                    //  ESSE VAR VAI DA ERRO E VOCE TEM QUE COLOCAR:
                    // private ISessionFactory _sessionFactory;
                    // LA ENCIMA NO DBFACTORY
                    var session = _sessionFactory.OpenSession();
                    session.FlushMode = FlushMode.Commit;

                    CurrentSessionContext.Bind(session);

                    return session;
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possivel criar a Sessão!!!", ex);
                }
            }
        }

        public void ClearSession()
        {
            this.Session.Clear();
        }

       
    }
}
