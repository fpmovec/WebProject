using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjectWithBuilderAndDecorator.Models;
using WebProjectWithBuilderAndDecorator.Services;

namespace WebProjectWithBuilderAndDecorator.Controllers
{
    public class WebServiceController : ApiController
    {
        private FileRepository fileRepository;
        public WebServiceController()
        {
            this.fileRepository = new FileRepository();
        }

        public TemporaryClass[] Get() => fileRepository.GetAllFiles();

       public HttpResponseMessage Post(TemporaryClass file)
       {
            System.Threading.Thread.Sleep(200);
           this.fileRepository.SaveFile(file);
            var response = Request.CreateResponse<TemporaryClass>(HttpStatusCode.Created, file);
            return response;
       }
    }
}
