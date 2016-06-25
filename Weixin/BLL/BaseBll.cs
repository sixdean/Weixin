using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Weixin.DAL;
using Weixin.Model.Common;

namespace Weixin.BLL
{
    public abstract class BaseBll<T> where T : class
    {
        #region Merber Variables


        //数据库连接串
        private string _connectionString = string.Empty;
        // DataContext
        private WeixinDataContext _dataContext;

        #endregion

        #region Constructors
        public BaseBll()
        {
        }
        public BaseBll(WeixinDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #endregion

        #region Property
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public WeixinDataContext DataContext
        {
            get
            {
                if (null == _dataContext)
                    InitializeDataContext();
                return _dataContext;
            }
        }
        #endregion

        #region Methods
        public abstract T GetById(string id);

        public virtual IQueryable<T> GetAll()
        {
            return DataContext.GetTable<T>();
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {

            DataContext.GetTable<T>().InsertOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        public virtual void Add(List<T> entityList)
        {
            var table = DataContext.GetTable<T>();
            foreach (var entity in entityList)
            {
                table.InsertOnSubmit(entity);
            }
            DataContext.SubmitChanges();
        }

        public virtual void Update()
        {
            DataContext.SubmitChanges();
        }

        public virtual void UpdateDbEntity(T dbEntity, T entity)
        {
            foreach (var property in typeof(T).GetProperties().Where(property => property.PropertyType.IsValueType || property.PropertyType.Name.StartsWith("String")))
            {
                property.SetValue(dbEntity, property.GetValue(entity, null), null);
            }
            DataContext.SubmitChanges();
        }

        public virtual void Delete(T entity)
        {
            DataContext.GetTable<T>().DeleteOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        public virtual void Delete(List<T> entityList)
        {
            var table = DataContext.GetTable<T>();
            foreach (var entity in entityList)
            {
                table.DeleteOnSubmit(entity);
            }
            DataContext.SubmitChanges();
        }

        public virtual void DeleteAll<TK>() where TK : class
        {
            DataContext.ExecuteCommand(string.Format(@"delete from {0}", DataContext.GetTable<TK>().Context.Mapping.GetTable(typeof(TK)).TableName));
        }

        /// <summary>
        ///     初始化DataContext
        /// </summary>
        private void InitializeDataContext()
        {
            if (string.Empty == _connectionString)
            {
                if (
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                    _dataContext = new WeixinDataContext(ConnectionString);
                }
                else
                {
                    _dataContext = new WeixinDataContext();
                }
                //_dataContext = new WeixinDataContext("Data Source=DEAN;Initial Catalog=Weixin;Integrated Security=True");
                //_dataContext = new WeixinDataContext("server=192.168.1.103;uid=dean;pwd=123456;database=Weixin");
            }
            else
            {
                _dataContext = new WeixinDataContext(_connectionString);
            }
        }

        protected virtual string CreateEntityId()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion

    }
    public abstract class BaseBll
    {
        #region Member Variables
        private string _connectionString = string.Empty;
        private WeixinDataContext _dataContext;
        #endregion

        #region Constructors
        public BaseBll()
        {
        }


        public BaseBll(WeixinDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #endregion


        #region Property
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        ///  DataContext
        /// </summary>
        public WeixinDataContext DataContext
        {
            get
            {
                if (null == _dataContext)
                    InitializeDataContext();
                return _dataContext;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// 初始化DataContext
        /// </summary>
        private void InitializeDataContext()
        {
            if (string.Empty == _connectionString)
            {
                if (
                    !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                    _dataContext = new WeixinDataContext(ConnectionString);
                }
                else
                {
                    _dataContext = new WeixinDataContext();
                }
                //_dataContext = new WeixinDataContext("Data Source=DEAN;Initial Catalog=Weixin;Integrated Security=True");
                //_dataContext = new WeixinDataContext("server=192.168.1.103;uid=dean;pwd=123456;database=Weixin");
            }
            else
            {
                _dataContext = new WeixinDataContext(_connectionString);
            }
        }

        protected virtual string CreateEntityId()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion

    }
}

