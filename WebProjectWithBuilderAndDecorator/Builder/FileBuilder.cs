using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjectWithBuilderAndDecorator.Models;

namespace WebProjectWithBuilderAndDecorator.Builder
{
    public class FileBuilder : IFileProduct
    {
        private FileItem file;
        public FileBuilder()
        {
            file = new FileItem();
        }
        public void InFilePathBuilder(string path)
        {
            file.InFilePath = path;
        }
        public void InArchieveTypeBuilder(string inArchieveType)
        {
            file.InArchieveType = inArchieveType;
        }
        public void InFileTypeBuilder(string inFileType)
        {
            file.InFileType = inFileType;   
        }
        public void OutFileTypeBuilder(string outFileType)
        {
            file.OutFileType = outFileType;
        }
        public void OutArchieveTypeBuilder(string outArchieveType)
        {
            file.OutArchieveType = outArchieveType;
        }
        public FileItem GetFileProduct()
        {
            FileItem _fileProduct = file;
            file = new FileItem();
            return _fileProduct;
        }
    }
}