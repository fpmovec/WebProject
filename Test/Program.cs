using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;
using Ionic.Zip;
using SharpCompress.Archives.Rar;

namespace Test
{
    public class File
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    internal class Program
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
                    Console.WriteLine(obj?.Name);
                }
            }
            
            using (var archive = RarArchive.Open("inputJSON.rar")) // RAR + JSON
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    var obj1 = JsonConvert.DeserializeObject<File>(System.IO.File.ReadAllText(entry.Key)); 
                    Console.WriteLine(obj?.Name);
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
                        if (xmlRead.NodeType == XmlNodeType.Text) Console.WriteLine(xmlRead.Value);
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
                        if (xmlRead.NodeType == XmlNodeType.Text) Console.WriteLine(xmlRead.Value);
                    }
                }
            }
            
        }
    }
}
    
