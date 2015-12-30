using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace Weixin.Model.Common
{
    /// <summary>
    ///     实体基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class EntityBase<T> : INotifyPropertyChanged
        where T : class, INotifyPropertyChanging, INotifyPropertyChanged, new()
    {
        #region protected 属性

        protected T DbEntity
        {
            get { return DBEntity; }
            set
            {
                DBEntity = value;
                SendPropertyChanged("DbEntity");
            }
        }

        #endregion

        #region Private 方法

        private void InitEntityPropertyInfos()
        {
            var dbEntityType = typeof(T);
            dbEntityPropertyInfos = dbEntityType.GetProperties();
            dbEntityKeys = new List<string>();
            foreach (var propertyInfo in dbEntityPropertyInfos)
            {
                var dbEntityPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnAttribute));
                if ((null != dbEntityPropertyAttribute) && (((ColumnAttribute)dbEntityPropertyAttribute).IsPrimaryKey))
                {
                    dbEntityKeys.Add(propertyInfo.Name);
                }
            }
        }

        #endregion

        #region static

        //辅助对象，用于同步
        private static readonly object _lockObject = new object();
        //缓存LINQ实体PropertyInfo
        private static PropertyInfo[] dbEntityPropertyInfos;
        private static List<string> dbEntityKeys;

        #endregion

        #region private 属性

        //对应的LINQ实体对象

        //存放每个属性是否已经修改

        private readonly Dictionary<string, bool> _changeMark;

        #endregion

        #region abstract

        /// <summary>
        ///     ID  abstract属性,需要由子类实现
        /// </summary>
        public abstract string ID { get; set; }

        public abstract string DisplayName { get; }

        #endregion

        #region 构造函数

        /// <summary>
        ///     构造函数
        /// </summary>
        public EntityBase()
        {
            //初始化信息

            if (dbEntityPropertyInfos == null)
            {
                //锁定，避免并发时重复初始化
                lock (_lockObject)
                {
                    if (dbEntityPropertyInfos == null)
                    {
                        InitEntityPropertyInfos();
                    }
                }
            }
            DBEntity = new T();
            _changeMark = new Dictionary<string, bool>();

            //注册属性更改事件
            PropertyChanged += _dbEntity_PropertyChanged;
        }

        private void _dbEntity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //当属性发生了改变,则记录下来
            _changeMark[e.PropertyName] = true;
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="dbEntity"></param>
        public EntityBase(T dbEntity)
        {
            //初始化信息
            if (dbEntityPropertyInfos == null)
            {
                //锁定，避免并发时重复初始化
                lock (_lockObject)
                {
                    if (dbEntityPropertyInfos == null)
                    {
                        InitEntityPropertyInfos();
                    }
                }
            }
            DBEntity = dbEntity ?? new T();
            _changeMark = new Dictionary<string, bool>();
            PropertyChanged += _dbEntity_PropertyChanged;
        }

        #endregion

        #region 事件

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanged(string propertyName)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Public 方法

        /// <summary>
        ///     返回LINQ实体
        /// </summary>
        /// <returns></returns>
        public T DBEntity { get; private set; }

        /// <summary>
        ///     更新LINQ实体
        /// </summary>
        /// <param name="dbEntity"></param>
        public virtual void UpdateDBEntity(T dbEntity)
        {
            foreach (var propertyInfo in dbEntityPropertyInfos)
            {
                if (dbEntityKeys.Contains(propertyInfo.Name))
                {
                }
                if (IsPropertyChange(propertyInfo.Name))
                {
                    propertyInfo.SetValue(dbEntity, propertyInfo.GetValue(DBEntity, null), null);
                }
            }
        }

        public void CopyFrom(EntityBase<T> obj)
        {
            DbEntity = obj.DbEntity;
        }

        public void CopyFrom(T obj)
        {
            DbEntity = obj;
        }

        /// <summary>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetPropertyValue(string propertyName)
        {
            return (from propertyInfo in dbEntityPropertyInfos
                    where propertyInfo.Name == propertyName
                    select propertyInfo.GetValue(DBEntity, null)).FirstOrDefault();
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            foreach (var propertyInfo in dbEntityPropertyInfos.Where(propertyInfo => propertyInfo.Name == propertyName))
            {
                propertyInfo.SetValue(DBEntity, value, null);
                break;
            }
        }

        #endregion

        #region protected 方法

        /// <summary>
        ///     标记该属性已经修改
        /// </summary>
        /// <param name="propertyName">属性名</param>
        protected void MarkPropertyChange(string propertyName)
        {
            _changeMark[propertyName] = true;
        }

        /// <summary>
        ///     检查该属性是否已经修改
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool IsPropertyChange(string propertyName)
        {
            return _changeMark.ContainsKey(propertyName) && _changeMark[propertyName];
        }

        #endregion
    }
}

