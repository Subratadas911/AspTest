using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MidPractice.DB;

namespace MidPractice.Controllers
{
    public class ProductsController : Controller
    {
        private EcomEntities db = new EcomEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Qty")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Qty")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Cart(int Id)
        {
            var db = new EcomEntities();

            var filteredProduct = (from p in db.Products
                                   where p.Id == Id
                                   select p).FirstOrDefault();


            if (Session["cart"] == null)
            {
                List<Product> products = new List<Product>();
                products.Add(filteredProduct);
                string json = new JavaScriptSerializer().Serialize(filteredProduct);
                Session["cart"] = json.ToString();
            }
            else
            {
                string json = Session["cart"].ToString();
                var dec = new JavaScriptSerializer().Deserialize<List<Product>>(json);
                dec.Add(filteredProduct);
                var productList = new JavaScriptSerializer().Serialize(dec);
                Session["cart"] = productList.ToString();
            }

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            if (Session["cart"] == null)
            {
                List<Product> dec = new List<Product>();
                return View(dec);
            }
            else
            {
                string json = Session["cart"].ToString();
                var dec = new JavaScriptSerializer().Deserialize<List<Product>>(json);
                return View(dec);
            }
        }

        public ActionResult Checkout(int Id)
        {

            return View();
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
