using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework.asp.Models;

namespace framework.asp.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {

                return View(db.cliente.ToList());
            }
           
        }

        public ActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.cliente.Add(cliente);
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

        public ActionResult Details (int id )
        {
            using (var db = new inventario2021Entities())
            {
                var findCliente = db.cliente.Find(id);
                return View(findCliente);
            }
        }

        public ActionResult Delete (int id )
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var findcliente = db.cliente.Find(id);
                    db.cliente.Remove(findcliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Edit (int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    var Findcliente = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(Findcliente);
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

        public ActionResult Edit (cliente editcliente )
        {
            try 
            {
                using (var db = new inventario2021Entities())
                {
                    cliente cliente = db.cliente.Find(editcliente.id);

                    cliente.nombre = editcliente.nombre;
                    cliente.documento = editcliente.documento;
                    cliente.email = editcliente.email;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
    }
}