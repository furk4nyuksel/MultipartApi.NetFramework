using MultipartApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MultipartApi.Controllers
{
    public class UploadController : ApiController
    {

        public UploadController()
        {

        }


        public string UploadFile( )
        {
            try
            {
                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("~/Content");
                string root2 = HttpContext.Current.Server.MapPath("~/Content");
                var provider = new MultipartFormDataStreamProvider(root);
 

                const string storagePath = @"C:\\";

 

                // Read the form data and return an async task.
                var task = Request.Content.ReadAsMultipartAsync(provider).
                    ContinueWith<HttpResponseMessage>(t =>
                    {
 

                        foreach (var key in provider.Contents)
                        {
                            Console.WriteLine(key.Headers.ContentDisposition);
                        }

                        // This illustrates how to get the file names.
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            string fileName = file.Headers.ContentDisposition.FileName;
                            if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                            {
                                fileName = fileName.Trim('"');
                            }
                            if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                            {
                                fileName = Path.GetFileName(fileName);
                            }
                            File.Copy(file.LocalFileName, Path.Combine(root2, fileName),true);
                        }
                        return Request.CreateResponse(HttpStatusCode.OK);
                    });

            }
            catch (System.Exception e)
            {

            }
            return string.Empty;
        }
    }
}
