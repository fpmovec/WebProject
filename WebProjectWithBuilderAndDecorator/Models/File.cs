using AngouriMath;
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
       FileItem FileImprovement(); 
    }
    public class FileItem : IFileImprovement
    {
        public string InFilePath { get; set; }
        public string InArchieveType { get; set; }
        public string InFileType { get; set; }
        public string OutFileType { get; set; }
        public string OutArchieveType { get; set; }
        [JsonProperty("MD5_Expression")]
        public string EncryptedExpression { get; set; }
        [JsonProperty("Expression")]
        private string Expression { get; set; }

        public Stream archiveStream;
        public FileItem FileImprovement()
        {
            return this;
        }
        public double ExpressionParsing()
        {
            Entity expression = GetExpression();
            return (double)expression.EvalNumerical();
        }

        public void SetExpression(string exp)
        {
            Expression = exp;
        }
        public string GetExpression()
        {
            return Expression;
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

        public virtual FileItem FileImprovement()
        {
            return _file.FileImprovement();
        }
    }
}