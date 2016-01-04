namespace Weixin.BLL
{
    public static class FactoryBll<T> where T : class, new()
    {
        public static T Instance
        {
            get { return new T(); }
        }
    }
}