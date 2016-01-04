using System;
using Weixin.Model.Common;

namespace Weixin.DAL
{
    partial class WeixinDataContext
    {

    }

    partial class SysUser
    {

    }

    partial class Menu : BaseEntity
    {

        public override string ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public override string DisplayName
        {
            get { return _Name; }
        }
    }
}