//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:43:58
// Description: TODO >> This is a script Description.
//============================================================

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace Zoranner.Engine.Extensions
{
    public static class SerializerExtensions
    {
        /// <summary>
        /// 利用XML序列化和反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static T DeepCopyWithXmlSerializer<T>(this T target)
        {
            object result;
            using (var ms = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, target);
                ms.Seek(0, SeekOrigin.Begin);
                result = xml.Deserialize(ms);
                ms.Close();
            }

            return (T) result;
        }

        /// <summary>
        /// 利用二进制序列化和反序列实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static T DeepCopyWithBinarySerialize<T>(this T target)
        {
            object result;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                // 序列化成流
                bf.Serialize(ms, target);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化成对象
                result = bf.Deserialize(ms);
                ms.Close();
            }

            return (T) result;
        }

        /// <summary>
        /// 利用DataContractSerializer序列化和反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static T DeepCopyWithDataContractSerializer<T>(this T target)
        {
            object result;
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, target);
                ms.Seek(0, SeekOrigin.Begin);
                result = ser.ReadObject(ms);
                ms.Close();
            }

            return (T) result;
        }
    }
}