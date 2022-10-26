using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebProjectWithBuilderAndDecorator.Models
{
    public interface IFileImprovement
    {
       object FileImprovement(); 
    }
    public class FileItem : IFileImprovement
    {
        public string InFilePath { get; set; }
        public string InArchieveType { get; set; }
        public string InFileType { get; set; }
        public string OutFileType { get; set; }
        public string OutArchieveType { get; set; }
        private string _expression { get; set; }

        public Stream archiveStream;
        public object FileImprovement()
        {
            return this;
        }
        public void SetExpression(string exp)
        {
            _expression = exp;
        }
        public string GetExpression()
        {
            return _expression;
        }
        public override string ToString() =>
         new StringBuilder()
            .Append(InFilePath + "\n")
            .Append(InArchieveType + "\n")
            .Append(InFileType + "\n")
            .Append(OutFileType + "\n")
            .Append(OutArchieveType)
            .ToString();
        
    }

    class FileDecorator : IFileImprovement
    {
        private readonly IFileImprovement _file;
        public FileDecorator(IFileImprovement file)
        {
            _file = file;
        }

        public virtual object FileImprovement()
        {
            return _file.FileImprovement();
        }
    }
}