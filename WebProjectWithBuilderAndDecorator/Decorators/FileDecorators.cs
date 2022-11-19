using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text.Json;
using Ionic.Zip;
using System.IO.Compression;
using System.IO;
using SharpCompress.Archives.Rar;
using WebProjectWithBuilderAndDecorator.Models;
using Newtonsoft.Json;

namespace WebProjectWithBuilderAndDecorator.Decorators
{
    class ZipInDecorator: FileDecorator
    {
        public ZipInDecorator(IFileImprovement file) : base(file) { }
        public override object FileImprovement()
        {
           object improve = base.FileImprovement();

            FileStream file = File.OpenRead(((FileItem)improve).InFilePath);

            ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read);
                
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                       ((FileItem)improve).archiveStream = entry.Open();
                        return improve;
                    }
                return improve;      
        }

    }
    class RarInDecorator : FileDecorator
    {
        public RarInDecorator(IFileImprovement file) : base(file) { }
        public override object  FileImprovement()
        {
            object improve = base.FileImprovement();
            FileStream file = File.OpenRead((((FileItem)improve).InFilePath));
            RarArchive rar = RarArchive.Open(file);
            foreach (RarArchiveEntry entry in rar.Entries)
            {
                ((FileItem)improve).archiveStream = entry.OpenEntryStream();
                return improve;
            }
            return improve;
        }

    }

    class TxtInDecorator : FileDecorator
    {
        public TxtInDecorator(IFileImprovement file) : base(file) { }

        public override object FileImprovement()
        {
            var improve = base.FileImprovement();
            if (((FileItem)improve).archiveStream != null)
            {

                using (StreamReader input = new StreamReader(((FileItem)improve).archiveStream))
                {
                    ((FileItem)improve).SetExpression(input.ReadLine());
                }
                    (((FileItem)improve).archiveStream).Close();
                
            }
            else
            {
               using (StreamReader input = new StreamReader(((FileItem)improve).InFilePath))
               {
                    ((FileItem)improve).SetExpression(input.ReadLine());
               }
               
            }
            return improve;
        }
    }
    class XmlInDecorator : FileDecorator
    {
        public XmlInDecorator(IFileImprovement file) : base(file) { }
         public override object FileImprovement()
         {
            var improve = base.FileImprovement();

            if (((FileItem)improve).archiveStream != null)
            {

                using (XmlReader xml = XmlReader.Create(((FileItem)improve).archiveStream))
                {
                    if (xml.MoveToContent() == XmlNodeType.Element)
                    {
                        ((FileItem)improve).SetExpression(xml.ReadElementContentAsString());
                    }
                    (((FileItem)improve).archiveStream).Close();
                }
            }
            else
            {
                XmlTextReader xmlRead = new XmlTextReader(((FileItem)improve).InFilePath); // XML
                xmlRead.WhitespaceHandling = WhitespaceHandling.None;
                while (xmlRead.Read())
                {
                    if (xmlRead.NodeType == XmlNodeType.Text)
                    {
                        ((FileItem)improve).SetExpression(xmlRead.Value);
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
        public override object FileImprovement()
        {
           
            var improve = base.FileImprovement();

            if (((FileItem)improve).archiveStream != null)
            {

                    var inp = JsonDocument.Parse(((FileItem)improve).archiveStream);
                var obj = inp.Deserialize<JsonFile>();
                ((FileItem)improve).SetExpression(obj?.expression);
                   // var obj = System.Text.Json.JsonSerializer.Deserialize(inp, typeof(JsonFile));

            }
            else
            {
                var obj = JsonConvert.DeserializeObject<JsonFile>(System.IO.File.ReadAllText(((FileItem)improve).InFilePath)); // JSON
                ((FileItem)improve).SetExpression(obj?.expression);
            }
            return improve;
        }
    }
    class MD5InDecorator : FileDecorator
    {
        public MD5InDecorator(IFileImprovement file) : base(file) { }
        public override object FileImprovement()
        {
            object improve = base.FileImprovement();
            improve += "\n + MD5 1!";
            return improve;
        }

    }
}