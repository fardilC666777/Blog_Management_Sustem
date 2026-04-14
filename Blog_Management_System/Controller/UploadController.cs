using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Blog_Management_System.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        [Route("api/upload")]
        public HttpResponseMessage UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                // Check if file exists in request
                if (httpRequest.Files.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No file uploaded.");

                // Get the first file
                var postedFile = httpRequest.Files[0];
                
                string uploadFolder = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string filePath = Path.Combine(uploadFolder, postedFile.FileName);
                postedFile.SaveAs(filePath);

                // Return success
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = "success",
                    file_name = postedFile.FileName,
                    file_size = postedFile.ContentLength,
                    saved_path = filePath
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
