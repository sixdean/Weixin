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

        public abstract T GetById(string id);

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


        public virtual void Update(T entity)
        {
            DataContext.GetTable<T>().Attach(entity);
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

        protected virtual string CreateEntityId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

