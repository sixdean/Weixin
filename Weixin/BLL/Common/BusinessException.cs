using System;
using System.Runtime.Serialization;
using System.Text;

namespace Weixin.BLL.Common
{
    [Serializable]
    public class BusinessException : Exception
    {
        #region Member Variables
        private const string _className = "BusinessException";
        private const int _hResult = -2146232832;
        #endregion


        #region Constructors
        public BusinessException()
            : base()
        {
            HResult = _hResult;
        }

        public BusinessException(string message)
            : base(message)
        {
            HResult = _hResult;
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = _hResult;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
        #endregion

        #region Operators
        public static implicit operator string(BusinessException ex)
        {
            return ex.ToString();
        }
        #endregion
        #region Methods

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", _className, this.Message);

            if (this.InnerException != null)
            {
                sb.AppendFormat(" ---> {0} <---", base.InnerException.ToString());
            }

            if (this.StackTrace != null)
            {
                sb.Append(Environment.NewLine);
                sb.Append(base.StackTrace);
            }

            return sb.ToString();
        }

        #endregion
    }
}