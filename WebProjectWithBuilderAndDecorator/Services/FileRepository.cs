using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjectWithBuilderAndDecorator.Models;
namespace WebProjectWithBuilderAndDecorator.Services
{
    public class FileRepository
    {
        private const string CacheKey = "FileStore";

        public FileRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var files = new TemporaryClass[]
                    {
                        new TemporaryClass{}
                    };
                    ctx.Cache[CacheKey] = files;
                }
            }
        }

        public TemporaryClass[] GetAllFiles()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (TemporaryClass[])ctx.Cache[CacheKey]; 
            }
            else throw new NullReferenceException();
        }

        public bool SaveFile(TemporaryClass file)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                   
                    var currentData = ((TemporaryClass[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(file);
                    file.MainOperations(file.IsArchieve());
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}