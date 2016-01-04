using System;
using System.Linq;
using System.Reflection;

namespace Weixin.Model.Common
{
    [Serializable]
    public abstract class BaseEntity<T>
    {
        public virtual T UpdateDbEntity(T dbEntity,T entity)
        {
            foreach (var property in typeof(T).GetProperties().Where(property => property.PropertyType.IsValueType || property.PropertyType.Name.StartsWith("String")))
            {
                property.SetValue(dbEntity,property.GetValue(entity, null),null);
            }
            return dbEntity;
        }
    }
}