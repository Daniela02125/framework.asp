using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework.asp.Models;

namespace framework.asp.Controllers
{
    
    public class ProveedorController : Controller
    {
        [Authorize]
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {

                return View(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.proveedor.Add(proveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            } catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findProveedor = db.proveedor.Find(id);
                return View(findProveedor);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findProveedor = db.proveedor.Find(id);
                    db.proveedor.Remove(findProveedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            } catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    
        public ActionResult Edit ( int id )
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var FindProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(FindProveedor);
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

        public ActionResult Edit (proveedor editproveedor)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    proveedor proveedor = db.proveedor.Find(editproveedor.id);

                    proveedor.nombre = editproveedor.nombre;
                    proveedor.direccion = editproveedor.direccion;
                    proveedor.telefono = editproveedor.telefono;
                    proveedor.nombre_contacto = editproveedor.nombre_contacto;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
    }
}