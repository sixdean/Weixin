namespace Weixin.Model.Common
{
    public class AjaxResult
    {

        public string result { get; set; }
        public string msg { get; set; }
        public AjaxResult(string r, string m)
        {
            result = r;
            msg = m;
        }


    }
}