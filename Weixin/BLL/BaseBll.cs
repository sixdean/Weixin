using System;
using System.Collections.Generic;
using System.Configuration;
using Weixin.DAL;

namespace Weixin.BLL
{
    public class BaseBll
    {
        //数据库连接串
        private string _connectionString = string.Empty;
        // DataContext
        private WeixinDataContext _dataContext;


        /// <summary>
        ///     构造函数
        /// </summary>
        public BaseBll()
        {
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="dataContext"></param>
        public BaseBll(WeixinDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        ///     DataContext
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

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add<T>(T entity)
        {
            DataContext.GetTable(typeof(T)).InsertOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        public virtual void Add<T>(List<T> entityList)
        {
            var table = DataContext.GetTable(typeof(T));
            foreach (var entity in entityList)
            {
                table.InsertOnSubmit(entity);
            }
            DataContext.SubmitChanges();
        }


        public virtual void Update<T>(T entity)
        {
            DataContext.GetTable(typeof(T)).Attach(entity);
            DataContext.SubmitChanges();
        }


        public virtual void Delete<T>(T entity)
        {
            DataContext.GetTable(typeof(T)).DeleteOnSubmit(entity);
            DataContext.SubmitChanges();
        }

        public virtual void Delete<T>(List<T> entityList)
        {
            var table = DataContext.GetTable(typeof(T));
            foreach (var entity in entityList)
            {
                table.DeleteOnSubmit(entity);
            }
            DataContext.SubmitChanges();
        }

        /// <summary>
        ///     初始化DataContext
        /// </summary>
        private void InitializeDataContext()
        {
            if (string.Empty == _connectionString)
            {
                //if (
                //    !string.IsNullOrEmpty(
                //        ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString))
                //{
                //    ConnectionString = ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString;
                //    _dataContext = new WeixinDataContext(ConnectionString);
                //}
                //else
                //{
                //    _dataContext = new WeixinDataContext();
                //}
                _dataContext = new WeixinDataContext("Data Source=DEAN;Initial Catalog=Weixin;Integrated Security=True");
            }
            else
            {
                _dataContext = new WeixinDataContext(_connectionString);
            }
        }

        protected virtual string CreateEntityID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

