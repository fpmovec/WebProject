using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjectWithBuilderAndDecorator.Builder;
using WebProjectWithBuilderAndDecorator.Directors;
using WebProjectWithBuilderAndDecorator.Decorators;
using System.Reflection.Emit;
using System.IO;
using SharpCompress.Archives.Rar;
using WebProjectWithBuilderAndDecorator.TypeInterface;
using AutoFixture;

namespace WebProjectWithBuilderAndDecorator.Models
{
    public class TemporaryClass
    {
        public string InFilePath { get; set; }
        public string InArchieveType { get; set; }
        public string InFileType { get; set; }
        public string OutFileType { get; set; }
        public string OutArchieveType { get; set; }

        public FileItem IsArchieve()
        { 
            var builder = new FileBuilder();

            if (string.Equals(InArchieveType, "zip"))
            {
                var director = new InZipDirector(builder);
                director.Build(InFilePath, InFileType, OutFileType, OutArchieveType);
            }
            else if (string.Equals(InArchieveType, "rar"))
            {
                var director = new InRarDirector(builder);
                director.Build(InFilePath, InFileType, OutFileType, OutArchieveType);
            }
            else if (string.Equals(InArchieveType, null))
            {
                var director = new WithoutInArchieveDirector(builder);
                director.Build(InFilePath, InFileType, OutFileType, OutArchieveType);
            }

            return builder?.GetFileProduct();           
        }

        public void MainOperations(FileItem obj)
        {
            TypeInfo typeInfo = new TypeInfo();
            if (obj.InArchieveType == "zip")
            {
                ZipInDecorator zipInDecorator = new ZipInDecorator(obj);
                typeInfo.TypeOut(((FileItem)zipInDecorator.FileImprovement()).InFileType,
                    (FileItem)zipInDecorator.FileImprovement());
              
            }
            else if (obj.InArchieveType == "rar")
            {
                RarInDecorator rarInDecorator = new RarInDecorator(obj);
                typeInfo.TypeOut(((FileItem)rarInDecorator.FileImprovement()).InFileType,
                     (FileItem)rarInDecorator.FileImprovement());
              
            }
            else
            {
                typeInfo.TypeOut(((FileItem)obj.FileImprovement()).InFileType,
                     (FileItem)obj.FileImprovement());
            }
            string line = obj.GetExpression();
        }
    }
    public class TypeInfo
    {
        private static Dictionary<string, IFileTypes> _types;
        public TypeInfo()
        {
            _types = new Dictionary<string, IFileTypes>();  
            _types.Add("txt", new TxtType { });
            _types.Add("xml", new XmlType { });
            _types.Add("json", new JsonType { });
        }
        public void TypeOut(string type, object obj)
        {
            _types.TryGetValue(type, out IFileTypes fileTypes);
            fileTypes.Type(obj);
        }
    }
}