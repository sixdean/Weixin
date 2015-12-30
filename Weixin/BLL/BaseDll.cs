using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Weixin.DAL;
using Weixin.Model.Common;

namespace Weixin.BLL
{
    public class BaseDll
    {
        /// <summary>
        ///     逻辑层基类。提供与数据访问层的选择。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        public abstract class BOBase<T, K>
            where T : EntityBase<K>, new()
            where K : class, INotifyPropertyChanging, INotifyPropertyChanged, new()
        {
            //数据库连接串
            private string _ConnectionString = string.Empty;
            //LINQ2 DataContext
            private WeixinDataContext _DataContext;


            /// <summary>
            ///     构造函数
            /// </summary>
            public BOBase()
            {
                //SecurityManager = new SecurityManager(DataContext);
                //SessionManager = new SessionManager(DataContext);
            }

            /// <summary>
            ///     构造函数
            /// </summary>
            /// <param name="dataContext"></param>
            public BOBase(WeixinDataContext dataContext)
            {
                _DataContext = dataContext;
                //SecurityManager = new SecurityManager(DataContext);
                //SessionManager = new SessionManager(DataContext);
            }

            //2012-02-23 add ConnectionString
            public string ConnectionString
            {
                get { return _ConnectionString; }
                set { _ConnectionString = value; }
            }

            /// <summary>
            ///     DataContext
            /// </summary>
            public WeixinDataContext DataContext
            {
                get
                {
                    if (null == _DataContext)
                        InitializeDataContext();
                    return _DataContext;
                }
            }

            //private SessionManager sessionManager;

            //public SessionManager SessionManager
            //{
            //    get
            //    {
            //        return sessionManager;
            //    }
            //    set
            //    {
            //        sessionManager = value;
            //    }
            //}

            //private SecurityManager securityManager;

            //public SecurityManager SecurityManager
            //{
            //    get
            //    {
            //        return securityManager;
            //    }
            //    set
            //    {
            //        securityManager = value;
            //    }
            //}

            /// <summary>
            ///     增加
            /// </summary>
            /// <param name="modelEntity"></param>
            public virtual string Add(T modelEntity)
            {
                if (null == modelEntity.ID)
                {
                    modelEntity.ID = CreateEntityID();
                }
                DataContext.GetTable<K>().InsertOnSubmit(modelEntity.DBEntity);
                DataContext.SubmitChanges();
                return modelEntity.ID;
            }

            /// <summary>
            ///     更新
            /// </summary>
            /// <param name="modelEntity"></param>
            public virtual void Update(T modelEntity)
            {
                var orgModelEntity = GetById(modelEntity.ID);
                var orgDbEntity = orgModelEntity.DBEntity;
                modelEntity.UpdateDBEntity(orgDbEntity);
                DataContext.SubmitChanges();
            }

            /// <summary>
            ///     删除
            /// </summary>
            /// <param name="modelEntity"></param>
            public virtual void Delete(T modelEntity)
            {
                var obj = GetById(modelEntity.ID);
                DataContext.GetTable<K>().DeleteOnSubmit(obj.DBEntity);
                DataContext.SubmitChanges();
            }

            public virtual void Delete(List<T> modelEntityList)
            {
                var ls = new List<K>();
                foreach (var item in modelEntityList)
                {
                    var obj = GetById(item.ID);
                    ls.Add(obj.DBEntity);
                }
                DataContext.GetTable<K>().DeleteAllOnSubmit(ls);
                DataContext.SubmitChanges();
            }

            public virtual void Delete(List<string> IDs)
            {
                var ls = new List<K>();
                foreach (var item in IDs)
                {
                    var obj = GetById(item);
                    ls.Add(obj.DBEntity);
                }
                DataContext.GetTable<K>().DeleteAllOnSubmit(ls);
                DataContext.SubmitChanges();
            }

            /// <summary>
            /// </summary>
            /// <param name="ID"></param>
            public virtual void DeleteByID(string ID)
            {
                var obj = GetById(ID);
                DataContext.GetTable<K>().DeleteOnSubmit(obj.DBEntity);
                DataContext.SubmitChanges();
            }

            public virtual List<T> GetAll()
            {
                var ret = new List<T>();
                foreach (var item in DataContext.GetTable<K>())
                {
                    var obj = new T();
                    obj.CopyFrom(item);
                    ret.Add(obj);
                }
                return ret;
            }

            protected virtual List<Z> ToList<Z>(IEnumerable<Z> objs)
                where Z : class
            {
                if (null == objs)
                {
                    return objs.ToList();
                }
                return new List<Z>();
            }

            /// <summary>
            ///     GetById abstract方法，需要由子类实现
            /// </summary>
            /// <param name="ID"></param>
            /// <returns></returns>
            public abstract T GetById(string ID);

            /// <summary>
            ///     生成新的实体对象标识号
            /// </summary>
            /// <returns></returns>
            protected virtual string CreateEntityID()
            {
                return Guid.NewGuid().ToString();
            }

            /// <summary>
            ///     初始化DataContext
            /// </summary>
            private void InitializeDataContext()
            {
                if (string.Empty == _ConnectionString)
                {
                    if (ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString != null &&
                        string.Empty !=
                        ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString)
                    {
                        ConnectionString =
                            ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString;
                        _DataContext = new WeixinDataContext(ConnectionString);
                    }
                    else
                    {
                        _DataContext = new WeixinDataContext();
                    }
                }
                else
                {
                    _DataContext = new WeixinDataContext(_ConnectionString);
                }
            }
        }

        /// <summary>
        ///     逻辑层基类
        /// </summary>
        public abstract class BOBase
        {
            //数据库连接串
            private string _ConnectionString = string.Empty;
            //LINQ2 DataContext
            private WeixinDataContext _DataContext;


            /// <summary>
            ///     构造函数
            /// </summary>
            public BOBase()
            {
            }

            /// <summary>
            ///     构造函数
            /// </summary>
            /// <param name="dataContext"></param>
            public BOBase(WeixinDataContext dataContext)
            {
                _DataContext = dataContext;
            }

            public string ConnectionString
            {
                get { return _ConnectionString; }
                set { _ConnectionString = value; }
            }

            /// <summary>
            ///     DataContext
            /// </summary>
            public WeixinDataContext DataContext
            {
                get
                {
                    if (null == _DataContext)
                        InitializeDataContext();
                    return _DataContext;
                }
            }

            /// <summary>
            ///     初始化DataContext
            /// </summary>
            private void InitializeDataContext()
            {
                if (string.Empty == _ConnectionString)
                {
                    if (ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString != null &&
                        string.Empty !=
                        ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString)
                    {
                        ConnectionString =
                            ConfigurationManager.ConnectionStrings["WeixinConnectionString"].ConnectionString;
                        _DataContext = new WeixinDataContext(ConnectionString);
                    }
                    else
                    {
                        _DataContext = new WeixinDataContext();
                    }
                }
                else
                {
                    _DataContext = new WeixinDataContext(_ConnectionString);
                }
            }
        }
    }
}