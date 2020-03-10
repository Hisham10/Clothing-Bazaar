using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingBazaar.web.Controllers
{
    public class SharedController : Controller
    {

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult(); //Accessing JSON
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0]; //Catching the request received from the server along with the file that was uploaded
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName); //Creates random alphanumeric string for the name of the file.
                var path = Path.Combine(Server.MapPath("~/content/images/"), fileName);

                file.SaveAs(path);

                result.Data = new { success = true, ImageUrl = string.Format("/content/images/{0}", fileName) }; 
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, Message = ex.Message };
            }

            return result;
        }
    }
}