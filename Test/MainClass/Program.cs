using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Ionic.Zip;
using Newtonsoft.Json;
using SharpCompress.Archives.Rar;

namespace Test.MainClass
{
    public class File
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TypeInfo
    {
        static Dictionary<string, FileTypes> _types;

        public TypeInfo()
        {
            _types = new Dictionary<string, FileTypes>
            {
                {"JSON", new JSON()},
                {"TXT", new TXT()},
                {"XML", new XML()}
            };
        }

        public void TypeOutput(string type)
        {
            _types.TryGetValue(type, out var fileTypes);
            fileTypes?.Type();
        }
    }
    internal static class Program
    {
        
        public static void Main(string[] args)
        {
            using (StreamReader input = new StreamReader("input.txt")) // TXT
            {
                Console.WriteLine(input.ReadLine());
            }
            
            
            var obj = JsonConvert.DeserializeObject<File>(System.IO.File.ReadAllText("input.json")); // JSON
            Console.WriteLine(obj?.Name);
            
            
            XmlTextReader xmlRead = new XmlTextReader("input.xml"); // XML
            xmlRead.WhitespaceHandling = WhitespaceHandling.None;
            while (xmlRead.Read())
            {
                if (xmlRead.NodeType == XmlNodeType.Text) Console.WriteLine(xmlRead.Value);
            }

            using (ZipFile zipFile = ZipFile.Read("input.zip")) // ZIP + TXT
            {
                foreach (ZipEntry item in zipFile)
                {
                    using (StreamReader input = new StreamReader(item.FileName)) 
                    {
                        Console.WriteLine(input.ReadLine());
                    }
                }
            }

            using (var archive = RarArchive.Open("input.rar")) // RAR + TXT
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    using (StreamReader input = new StreamReader(entry.Key)) 
                    {
                        Console.WriteLine(input.ReadLine());
                    }
                }
            }
            
            using (ZipFile zipFile = ZipFile.Read("inputJSON.zip")) // ZIP + JSON
            {
                foreach (ZipEntry item in zipFile)
                {
                    var obj1 = JsonConvert.DeserializeObject<File>(System.IO.File.ReadAllText(item.FileName)); 
                    Console.WriteLine(obj1?.Name);
                }
            }
            
            using (var archive = RarArchive.Open("inputJSON.rar")) // RAR + JSON
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    var obj1 = JsonConvert.DeserializeObject<File>(System.IO.File.ReadAllText(entry.Key)); 
                    Console.WriteLine(obj1?.Name);
                }
            }
            
            
            using (ZipFile zipFile = ZipFile.Read("inputJSON.zip")) // ZIP + XML
            {
                foreach (ZipEntry item in zipFile)
                {
                    XmlTextReader xmlRead1 = new XmlTextReader(item.FileName); 
                    xmlRead.WhitespaceHandling = WhitespaceHandling.None;
                    while (xmlRead.Read())
                    {
                        if (xmlRead1.NodeType == XmlNodeType.Text) Console.WriteLine(xmlRead.Value);
                    }
                }
            }
            
            using (var archive = RarArchive.Open("inputJSON.rar")) // RAR + XML
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    XmlTextReader xmlRead1 = new XmlTextReader(entry.Key); 
                    xmlRead.WhitespaceHandling = WhitespaceHandling.None;
                    while (xmlRead.Read())
                    {
                        if (xmlRead1.NodeType == XmlNodeType.Text) Console.WriteLine(xmlRead.Value);
                    }
                }
            }
            //System.IO.File.Decrypt("input.xml");
            //Console.WriteLine(Path.GetExtension("input.xml"));
            TypeInfo typeInfo = new TypeInfo();
            typeInfo.TypeOutput("XML");
            typeInfo.TypeOutput("JSON");
            typeInfo.TypeOutput("TXT");
        }
    }
}
    
