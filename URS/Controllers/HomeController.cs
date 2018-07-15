using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using URS.Utilities;
using URS.Model;
using Newtonsoft.Json;

namespace URS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            mailContact mc = new mailContact();
            return View();
        }




        [HttpPost]
        public ActionResult Index(mailContact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dynamic json = JsonConvert.DeserializeObject(model.orderProducts);
                    //int totalQty = json.itemTotal.qty;
                    //int totalPrice= json.itemTotal.price;
                    string subject = "Enquiry For Oil form URS-Form Products";
                    string toMail = "ursfarmproducts@gmail.com";
                    string body = "Name: " + model.inputName + "<br/>";
                    body += "Mobile No: " + model.inputMobNo + "<br/>";
                    body += "Mail : " + model.inputEmail + "<br/>";
                    body += "Message : " + model.inputComments + "<br/>";
                    body += "OrderDetails : <br/> ";
                    body += "1.  Product Name : " + json.itemSesame.prodName + "  -- Product Quantity : " + json.itemSesame.qty+ "  --  Product Price : " + json.itemSesame.price + "<br/>";
                    body += "2.  Product Name : " + json.itemPeanut.prodName + " -- Product Quantity : " + json.itemPeanut.qty + "  --  Product Price : " + json.itemPeanut.price + "<br/>";
                    body += "3.  Product Name : " + json.itemCoconut.prodName + " -- Product Quantity : " + json.itemCoconut.qty + "  --  Product Price : " + json.itemCoconut.price + "<br/>";
                    body += "4.  Product Name : " + json.itemCastor.prodName + " -- Product Quantity : " + json.itemCastor.qty + "  --  Product Price : " + json.itemCastor.price + "<br/>";
                    //body += "Total  Product Quantity : " + totalQty + " -- Total Product Price : " + totalPrice + "<br/>";
                    CommonUtil.SendEmail(subject, toMail, body, "Enquiry Mail!!!!!");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Sorry something went wrong contact us at:office@applecalendars.com");
                    //throw ex;
                }
            }
            string error = string.Join("; ", ModelState.Values
                                  .SelectMany(x => x.Errors)
                                  .Select(x => x.ErrorMessage));
            if (!string.IsNullOrEmpty(error))
            {
                model.ReturnMessage = error;
            }
            else
            {
                model.ReturnMessage = "Thank you for reaching out to us. We will get in touch with you shortly.";
            }
            return View("~/Views/Home/Index.cshtml", model);
        }

    }
}