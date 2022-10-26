using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectWithBuilderAndDecorator.Models;

namespace WebProjectWithBuilderAndDecorator
{
    public interface IFileProduct
    {
        void InFilePathBuilder(string path);
        void InArchieveTypeBuilder(string inArchieveType);
        void InFileTypeBuilder(string inFileType);
        void OutFileTypeBuilder(string outFileType);
        void OutArchieveTypeBuilder(string outArchieveType);

        FileItem GetFileProduct();
    }
}
