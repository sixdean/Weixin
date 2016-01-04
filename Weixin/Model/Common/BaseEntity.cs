using System;

namespace Weixin.Model.Common
{
    [Serializable]
    public abstract class BaseEntity
    {
        public abstract string ID { get; set; }
        public abstract string DisplayName { get; } 
    }
}