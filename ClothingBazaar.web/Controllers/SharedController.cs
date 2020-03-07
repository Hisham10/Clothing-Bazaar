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
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; //Need to write this to allow Json request

            try
            {
                //The formData that we have sent from the Create View is received in 'REQUEST' in the controller.
                //So catching that request and storing in a variable named file.
                //Since we're not taking any parameters in this method so we have to catch it somehow. And that is being done using Request.
                //So checking the files that we have in the request we received and picking the first file of the request.
                var file = Request.Files[0]; //Catching the request received from the server along with the file that was uploaded
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName); //Creates random alphanumeric string for the name of the file.
                var path = Path.Combine(Server.MapPath("~/content/images/"), fileName);

                file.SaveAs(path);

                result.Data = new { success = true, ImageUrl = string.Format("/content/images/{0}", fileName) }; //Sending Json reult data
                //The variable names used here are totally random and are used to catch these variables in the view.
                //As we can see that in Create view we have used these variables to assign the image on the screen
                //when a user uploads an image. 
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, Message = ex.Message };
            }

            return result;
        }
    }
}