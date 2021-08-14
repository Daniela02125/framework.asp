using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework.asp.Models;

namespace framework.asp.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.roles.ToList());
            }
            
        }

        public ActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create (roles roles)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.roles.Add(roles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }

        public ActionResult Details (int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findroles = db.roles.Find(id);
                return View(findroles);
            }
        }

        public ActionResult Delete (int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findroles = db.roles.Find(id);
                    db.roles.Remove(findroles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Edit (int id )
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var Findroles = db.roles.Where(a => a.id == id).FirstOrDefault();
                    return View(Findroles);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit (roles editroles)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    roles roles = db.roles.Find(editroles.id);

                    roles.descripcion = editroles.descripcion;

                   

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
    }
}