using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Weixin.Common
{
    public class XmlSerializerHelper
    { 

        /// <summary>
        ///  将object对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToXml<T>(T t)
        {
            var ser = new XmlSerializer(t.GetType());
            using (var mem = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(mem, Encoding.UTF8))
                {
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    ser.Serialize(writer, t, ns);
                    return Encoding.UTF8.GetString(mem.ToArray());
                }
            }
        }

        /// <summary>
        ///  将XML反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(string source)
        {
            var mySerializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(source)))
            {
                return (T)mySerializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 二进制方式序列化对象
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            return ms.ToString();
        }

        /// <summary>
        ///  二进制方式反序列化对象
        /// </summary>
        /// <returns></returns>
        public static T DeSerialize<T>(string str) where T : class
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            var formatter = new BinaryFormatter();
            var t = formatter.Deserialize(ms) as T;
            return t;
        }
    }
}