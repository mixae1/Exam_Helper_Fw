using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Helper.Controllers
{
    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tests/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tests/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
