using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjectWithBuilderAndDecorator.Directors
{
    public class InZipDirector
    {
        private IFileProduct _build;
        public InZipDirector(IFileProduct build)
        {
            _build = build;
        }
        public void Build(string inPath, string inFileType, string outFileType, string outArchieve)
        {
            _build.InFilePathBuilder(inPath);
            _build.InArchieveTypeBuilder("zip");
            _build.InFileTypeBuilder(inFileType);
            _build.OutFileTypeBuilder(outFileType);
            _build.OutArchieveTypeBuilder(outArchieve);
        }
    }
    public class InRarDirector
    {
        private IFileProduct _build;
        public InRarDirector(IFileProduct build)
        {
            _build = build; 
        }
        public void Build(string inPath, string inFileType, string outFileType, string outArchieve)
        {
            _build.InFilePathBuilder(inPath);
            _build.InArchieveTypeBuilder("rar");
            _build.InFileTypeBuilder(inFileType);
            _build.OutFileTypeBuilder(outFileType);
            _build.OutArchieveTypeBuilder(outArchieve);
        }
    }
    public class WithoutInArchieveDirector
    {
        private IFileProduct _build;
        public WithoutInArchieveDirector(IFileProduct build)
        {
            _build = build; 
        }
        public void Build(string inPath, string inFileType, string outFileType, string outArchieve)
        {
            _build.InFilePathBuilder(inPath);
            _build.InFileTypeBuilder(inFileType);
            _build.OutFileTypeBuilder(outFileType);
            _build.OutArchieveTypeBuilder(outArchieve);
        }
    }
}