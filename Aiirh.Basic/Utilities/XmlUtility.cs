using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Aiirh.Basic.Utilities
{
    public static class XmlUtility
    {
        public static void WriteXmlToFileSystem<T>(string fileLocation, T request)
        {
            File.WriteAllBytes(fileLocation, ConvertRequestIntoByteArray(request));
        }

        public static byte[] ConvertRequestIntoByteArray<T>(T request)
        {
            var wsSerializer = new XmlSerializer(request.GetType());
            using var ms = new MemoryStream();
            using (var xml = XmlWriter.Create(ms, new XmlWriterSettings() { Encoding = Encoding.UTF8, Indent = true }))
            {
                wsSerializer.Serialize(xml, request);
                xml.Flush();
            }
            var data = ms.ToArray();
            return data;
        }

        public static string Serialize<T>(T obj, XmlWriterSettings settings)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(obj.GetType());
            using var stream = new Utf8StringWriter();
            using var writer = XmlWriter.Create(stream, settings);
            serializer.Serialize(writer, obj, emptyNamespaces);
            return stream.ToString();
        }

        public static string Serialize<T>(T obj)
        {
            return Serialize(obj, new Utf8StringWriter());
        }

        public static string Serialize<T>(T obj, StringWriter stringWriter)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var writer = stringWriter;
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }

        public static string SerializeAndTrim<T>(this T item, int truncateFirstLines, int truncateEndLines)
        {
            var xmlNode = Serialize(item);
            var lines = xmlNode.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var trimmedLines = lines.Skip(truncateFirstLines).Take(lines.Length - truncateFirstLines - truncateEndLines);
            return string.Join("\r\n", trimmedLines);
        }

        public static (string first, string last) SerializeAndTake<T>(this T item, int takeFirstLines, int takeEndLines)
        {
            var xmlNode = Serialize(item);
            var lines = xmlNode.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var firstLines = lines.Take(takeFirstLines);
            var endLines = lines.Skip(lines.Length - takeEndLines).Take(takeEndLines);
            return (string.Join("\r\n", firstLines), string.Join("\r\n", endLines));
        }

        public static byte[] SerializeObject<T>(T item)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            serializer.Serialize(ms, item);
            return ms.ToArray();
        }

        public static byte[] SerializeObject<T>(T item, XmlWriterSettings settings)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            using var writer = XmlWriter.Create(ms, settings);
            serializer.Serialize(writer, item);
            return ms.ToArray();
        }

        public static T Deserialize<T>(string stringXml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using XmlReader reader = new XmlTextReader(new StringReader(stringXml));
            return (T)serializer.Deserialize(reader);
        }

        public static T Deserialize<T>(byte[] dataXml)
        {
            var str = Encoding.Default.GetString(dataXml);
            return Deserialize<T>(str);
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
