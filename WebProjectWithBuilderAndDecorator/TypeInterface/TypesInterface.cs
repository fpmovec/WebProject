using Ionic.Zip;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using WebProjectWithBuilderAndDecorator.Models;
using WebProjectWithBuilderAndDecorator.Decorators;

namespace WebProjectWithBuilderAndDecorator.TypeInterface
{
    public interface IFileTypes
    {
        void Type(object obj);
    }

    public class XmlType : IFileTypes
    {
        public void Type(object obj)
        {
            XmlInDecorator decorator = new XmlInDecorator((FileItem)obj);
            obj = decorator.FileImprovement();
        }
    }
    public class JsonType : IFileTypes
    {
        public void Type(object obj)
        {
            JsonDecorator decorator = new JsonDecorator((FileItem)obj);
            obj= decorator.FileImprovement();
           
        }
    }
    public class TxtType : IFileTypes
    {
        public void Type(object obj)
        {
            TxtInDecorator decorator = new TxtInDecorator((FileItem)obj);
            obj = decorator.FileImprovement();
        }
    }
}