using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MultipartUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post(HttpPostedFileBase fileUpload)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data charset=utf-8");
                client.DefaultRequestHeaders.Accept.Clear();


                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();


                multipartFormDataContent.Add(new StringContent("test", UTF8Encoding.UTF8), "ExampleParameter");
                multipartFormDataContent.Add(new StringContent("tes12321", UTF8Encoding.UTF8), "tee");
                multipartFormDataContent.Add(new StringContent("te3123st", UTF8Encoding.UTF8), "ExampleParaasdasmeter");
                multipartFormDataContent.Add(new StringContent("tes2131231t", UTF8Encoding.UTF8), "qq");



                MemoryStream target = new MemoryStream();
                fileUpload.InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                var fileContent = new ByteArrayContent(data);

                string filePath = Path.GetExtension(fileUpload.FileName);

                multipartFormDataContent.Add(fileContent, "PostImageFile", Guid.NewGuid().ToString() + filePath);

                HttpResponseMessage httpResponceMessage = client.PostAsync("https://localhost:44337/" + "api/Upload/UploadFile/", multipartFormDataContent).Result;
                httpResponceMessage.EnsureSuccessStatusCode();

                string response = httpResponceMessage.Content.ReadAsStringAsync().Result;
            }
            return View();
        }
    }
}