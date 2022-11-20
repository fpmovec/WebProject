using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text.Json;
using Ionic.Zip;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using SharpCompress.Archives.Rar;
using WebProjectWithBuilderAndDecorator.Models;
using Newtonsoft.Json;

namespace WebProjectWithBuilderAndDecorator.Decorators
{
    class ZipInDecorator: FileDecorator
    {
        public ZipInDecorator(IFileImprovement file) : base(file) { }
        public override FileItem FileImprovement()
        {
           FileItem improve = base.FileImprovement();

            FileStream file = File.OpenRead(improve.InFilePath);

            ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read);
                
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                       improve.archiveStream = entry.Open();
                        return improve;
                    }
                return improve;      
        }

    }
    class RarInDecorator : FileDecorator
    {
        public RarInDecorator(IFileImprovement file) : base(file) { }
        public override FileItem  FileImprovement()
        {
            FileItem improve = base.FileImprovement();
            FileStream file = File.OpenRead((improve.InFilePath));
            RarArchive rar = RarArchive.Open(file);
            foreach (RarArchiveEntry entry in rar.Entries)
            {
                improve.archiveStream = entry.OpenEntryStream();
                return improve;
            }
            return improve;
        }

    }

    class TxtInDecorator : FileDecorator
    {
        public TxtInDecorator(IFileImprovement file) : base(file) { }

        public override FileItem FileImprovement()
        {
            var improve = base.FileImprovement();
            if (improve.archiveStream != null)
            {

                using (StreamReader input = new StreamReader(improve.archiveStream))
                {
                    improve.SetExpression(input.ReadLine());
                }
                    (improve.archiveStream).Close();
                
            }
            else
            {
               using (StreamReader input = new StreamReader(improve.InFilePath))
               {
                    improve.SetExpression(input.ReadLine());
               }
               
            }
            return improve;
        }
    }
    class XmlInDecorator : FileDecorator
    {
        public XmlInDecorator(IFileImprovement file) : base(file) { }
         public override FileItem FileImprovement()
         {
            var improve = base.FileImprovement();

            if (improve.archiveStream != null)
            {

                using (XmlReader xml = XmlReader.Create(improve.archiveStream))
                {
                    if (xml.MoveToContent() == XmlNodeType.Element)
                    {
                        improve.SetExpression(xml.ReadElementContentAsString());
                    }
                    (improve.archiveStream).Close();
                }
            }
            else
            {
                XmlTextReader xmlRead = new XmlTextReader(improve.InFilePath); // XML
                xmlRead.WhitespaceHandling = WhitespaceHandling.None;
                while (xmlRead.Read())
                {
                    if (xmlRead.NodeType == XmlNodeType.Text)
                    {
                        improve.SetExpression(xmlRead.Value);
                    }
                }
            }
            return improve;
         }
    }

    public class JsonFile
    {
        [JsonProperty("expression")]
        public string expression { get; set; }
    }
    class JsonDecorator : FileDecorator
    {
        public JsonDecorator(IFileImprovement file) : base(file) { }
        public override FileItem FileImprovement()
        {
           
            var improve = base.FileImprovement();

            if (improve.archiveStream != null)
            {

                    var inp = JsonDocument.Parse(improve.archiveStream);
                var obj = inp.Deserialize<JsonFile>();
                improve.SetExpression(obj?.expression);
                   // var obj = System.Text.Json.JsonSerializer.Deserialize(inp, typeof(JsonFile));

            }
            else
            {
                var obj = JsonConvert.DeserializeObject<JsonFile>(System.IO.File.ReadAllText(improve.InFilePath)); // JSON
                improve.SetExpression(obj?.expression);
            }
            return improve;
        }
    }
    class MD5InDecorator : FileDecorator
    {
        public MD5InDecorator(IFileImprovement file) : base(file) { }
        public override FileItem FileImprovement()
        {
            object improve = base.FileImprovement();
            improve += "\n + MD5 1!";
            return improve;
        }

    }
}