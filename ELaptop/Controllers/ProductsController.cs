using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using ELaptop.Models;

namespace ELaptop.Controllers
{
    public class ProductsController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        // GET: Products
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public ActionResult Create()
        {
            Debug.WriteLine("From create controller");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ////Use Namespace called :  System.IO  
                string FileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                Debug.WriteLine(Path.GetFileNameWithoutExtension("From controller" +product.ImageFile.FileName));

                ////To Get File Extension  
                string FileExtension = Path.GetExtension(product.ImageFile.FileName);

                ////Add Current Date To Attached File Name  
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                ////Get Upload path from Web.Config file AppSettings.  
                string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                Debug.WriteLine(UploadPath);

                ////Its Create complete path to store in server.  
                product.ImagePath = "~/UserImages/" + FileName;

                FileName = Path.Combine(Server.MapPath("~/UserImages/"), FileName);

                ////To copy and save file into server.  
                product.ImageFile.SaveAs(FileName);

                Debug.WriteLine(product.ImagePath);

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            Debug.WriteLine(file.FileName);
            return View();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {

                if(product.ImageFile != null)
                {
                    ////Use Namespace called :  System.IO  
                    string FileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    Debug.WriteLine(Path.GetFileNameWithoutExtension("From controller" + product.ImageFile.FileName));

                    ////To Get File Extension  
                    string FileExtension = Path.GetExtension(product.ImageFile.FileName);

                    ////Add Current Date To Attached File Name  
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;

                    ////Get Upload path from Web.Config file AppSettings.  
                    string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                    Debug.WriteLine(UploadPath);

                    ////Its Create complete path to store in server.  
                    product.ImagePath = "~/UserImages/" + FileName;

                    FileName = Path.Combine(Server.MapPath("~/UserImages/"), FileName);

                    ////To copy and save file into server.  
                    product.ImageFile.SaveAs(FileName);

                    Debug.WriteLine(product.ImagePath);
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
