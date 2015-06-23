using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using System.IO;
using System.Data.SQLite;
using Data.Mapping;
using Data.Model;



namespace Data.Session
{
    public class NHibernateSession : IDisposable
    {
        private static ISessionFactory _sessionFactory;
        private static string _DbPath;
        //private static System.Threading.ThreadStart t = new System.Threading.ThreadStart(SSH2StartTunnel);
        //private readonly static System.Threading.Thread _newThread = new System.Threading.Thread(t);

        public static string DbFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_DbPath))
                {
                    GetDBPath();                    
                }
                return _DbPath;
            }
            set { _DbPath = value; }
        }
        private static void GetDBPath()
        {
            string c = ConfigurationManager.AppSettings.Get("AssetsDB");          
            
            if (c == null || string.IsNullOrWhiteSpace(c) || string.Compare(c,"{AppDir}")==0)
            {
                //throw new ConfigurationErrorsException("Database configuration error!");
               string basedir = AppDomain.CurrentDomain.BaseDirectory;           
                basedir = c.Replace("{AppDir}",  string.Concat(basedir,"AssetsDB.DB"));
              
                System.Diagnostics.Debug.WriteLine(string.Format("dbpath = {0}", basedir));
                _DbPath = basedir;       
            }          
            else { _DbPath = c; }
           
        }
     
        public static ISessionFactory SessionFactory
        {
            get
            {
                
                if (_sessionFactory == null || string.IsNullOrWhiteSpace(_DbPath) || !File.Exists(_DbPath))
                    InitializeSessionFactory();
              
                return _sessionFactory;
            }
        }
        public static void test()
        {
            SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_sessionFactory != null)
                {
                    _sessionFactory.Dispose();
                    _sessionFactory = null;
                }
        }
        ~NHibernateSession()
        {
            Dispose(false);
        }

        public static void InitializeSessionFactory()
        {
            //  //_newThread.Start();
            GetDBPath();
            string SchemaExportPath = Path.Combine(System.Environment.CurrentDirectory, "Mappings");

            System.Diagnostics.Debug.WriteLine(SchemaExportPath);
            if (!Directory.Exists(SchemaExportPath))
            {
                Directory.CreateDirectory(SchemaExportPath);
            }
                     
            _sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(_DbPath)) //DbFile).ShowSql()) 
                //  .Database(SQLiteConfiguration.Standard.UsingFileWithPassword(DbFile,"asdfqwer1234"))                
            .Mappings(m => m.FluentMappings
                         .AddFromAssemblyOf<User>()
                         .AddFromAssemblyOf<Item>()
                        .AddFromAssemblyOf<ItemCategory>()      
                        .AddFromAssemblyOf<ItemStatus>()
                  .ExportTo(SchemaExportPath))            //<--- path to mapping files   '

                .ExposeConfiguration(BuildSchema)				

                .BuildConfiguration()				 
                .BuildSessionFactory();

            //using (var session = OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        var item = new Item();
            //        item.ItemStatus = ItemStatus.Approved;
            //        session.Save(item);
            //        transaction.Commit();
            //    }
            //}

            //  //throw new TimeoutException("Unable to Connect", new Exception());



           // _sessionFactory=    Fluently.Configure()
           //     .Database(
           //     SQLiteConfiguration.Standard.UsingFile(DbFile))
           //     .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Person>())
           //     .BuildSessionFactory();
           //// System.Diagnostics.Debug.WriteLine("db path is {0}", _sessionFactory.GetCurrentSession().Connection.Database);
        }
        public static ISession OpenSession()
        {           
            return SessionFactory.OpenSession();
            throw new Exception("Open Session Error");
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            // config.Configure();
            string UpdateScriptPath = Path.Combine(System.Environment.CurrentDirectory, "Scripts");
            try
            {
                config.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, (TimeSpan.FromMinutes(10).TotalSeconds).ToString());

                if (!Directory.Exists(UpdateScriptPath))
                {
                    Directory.CreateDirectory(UpdateScriptPath);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }

            Action<string> updateExport = x =>
                {
                    try
                    {
                        using (var file = new FileStream(UpdateScriptPath + "\\updatescript.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        using (var sw = new StreamWriter(file))
                        {
                            sw.Write(x);
                            sw.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("unable to write to file");
                    }
                };
            
            if (!File.Exists(_DbPath))
            {
                try
                {
                    // this NHibernate tool takes a configuration (with mapping info in)
                    // and exports a database schema from it
                    new SchemaExport(config) //.Execute(true, true, false);			
                        .Create(false, true);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                try
                {
                    new SchemaUpdate(config).Execute(updateExport, true); //don't rely to automapping/autoupdate. might break schema.
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }
    }


    public class SqlStatementInterceptor : EmptyInterceptor
    {

        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            return sql;
        }
    }


}
