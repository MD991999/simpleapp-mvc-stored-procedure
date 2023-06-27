using products_crud_application.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using products_crud_application.Models;
using System.IO;
using Microsoft.Ajax.Utilities;

namespace products_crud_application.Controllers
{
    public class ProductController : Controller
    {
Product_DAL _product_DAL = new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            var productList = _product_DAL.GetAllProducts();
            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently product is not available in your database";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = _product_DAL.GetAllProductsbyId(id).FirstOrDefault();
            try
            {
                if (product != null)
                {
                    return View(product);

                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid ID";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception error)
            {

                TempData["displayerrormessage"] = error.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Product/Create
            [HttpPost]
            public ActionResult Create(product product1)
            {
            bool isInserted = false;

            try
            {
                    // TODO: Add insert logic here
                    if(ModelState.IsValid)
                {
                    isInserted = _product_DAL.InsertProducts(product1);
                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save date successfully";
                    }
                }
                    return RedirectToAction("Index"); //redirect to index page
                }
                catch(Exception error)
                {
                TempData["ErrorMessage"] = error.Message;

                return View();
                //here if there is any error the page will redirect to create page itself

                }
            }

            // GET: Product/Edit/5
            public ActionResult Edit(int id)
            {
            var product = _product_DAL.GetAllProductsbyId(id).FirstOrDefault();//FirstOrDefault() is a LINQ method that is used to retrieve the first element from a sequence or collection that matches a specified condition. 
            return View(product);
            }

            // POST: Product/Edit/5
            [HttpPost,ActionName("Edit")]
            public ActionResult Editdetails(product product)
            {
            bool isupdated = false;

            try
            {
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    isupdated = _product_DAL.UpdateProducts(product);
                    if (isupdated)
                    {
                        TempData["UpdateSuccessMessage"] = "Updated succesfully";
                    }
                    else
                    {
                        TempData["UpdateErrorMessage"] = "Updation cancelled";
                    }
                }
                    return RedirectToAction("Index");
                }
                catch(Exception error)
                {
                TempData["UpdationvalidationErrorMessage"] = error.Message;
                    return View();
                }
            }
        
            // GET: Product/Delete/5
            public ActionResult Delete(int id)
            {

            try
            {
                var item = _product_DAL.GetAllProductsbyId(id).FirstOrDefault();

                if (item == null)
                {
                    TempData["deleteidexistsmessage"] = "Invalid ID";
                    return RedirectToAction("Index");
                }

                else
                {
                    return View(item);
                }
            }
            catch (Exception error)
            {
                TempData["deleteexceptionerrormessage"]=error.Message;
                return View();
            }


        }

            // POST: Product/Delete/5
            [HttpPost,ActionName("Delete")] //In this example, the action method is named DeleteConfirmation, but by using [HttpPost, ActionName("Delete")], you specify that the action should be treated as the "Delete" action. So, when the form is submitted, it will invoke the DeleteConfirmation action method, but the URL and routing will still show the action as "Delete".
        public ActionResult DeleteConfirmation(int id)
            {
            try
            {
                // TODO: Add delete logic here
string result=_product_DAL.DeleteProduct(id);
                if (result.Contains("deleted")) {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch(Exception error)
            {
                TempData["ErrorMessage"] = error.Message;
                return View();
            }

        }
    }
}
