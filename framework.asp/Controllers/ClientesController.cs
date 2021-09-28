using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework.asp.Models;
using System.Web.Routing;

namespace framework.asp.Controllers
{

    public class ClientesController : Controller
    {
        [Authorize]
        // GET: Clientes
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {

                return View(db.cliente.ToList());
            }

        }

        public ActionResult Create()
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

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                var findCliente = db.cliente.Find(id);
                return View(findCliente);
            }
        }

        public ActionResult Delete(int id)
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Edit(int id)
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

        public ActionResult Edit(cliente editcliente)
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

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }


        public ActionResult ClienteCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClienteCSV(HttpPostedFileBase fileForm)
        {
            try
            {

                //string para guardar la ruta
                string filePath = string.Empty;

                //condicion para saber si el archivo llego
                if (fileForm != null)
                {
                    //ruta de la carpeta que guardara el archivo
                    string path = Server.MapPath("~/Uploads/");

                    //condicion para saber si la carpeta uploads existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //obtener el nombre del archivo
                    filePath = path + Path.GetFileName(fileForm.FileName);

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(fileForm.FileName);

                    //guardar el archivo
                    fileForm.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newCliente = new cliente
                            {
                                nombre = row.Split(';')[0],
                                documento = row.Split(';')[2],
                                email = row.Split(';')[1],

                            };

                            using (var db = new inventario2021Entities())
                            {
                                db.cliente.Add(newCliente);
                                db.SaveChanges();
                            }
                        }
                    }



                }

                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult PaginadorIndex(int pagina = 1)
        {
            try
            {
                var cantidadRegistros = 5;

                using (var db = new inventario2021Entities())
                {
                    var clientes = db.cliente.OrderBy(x => x.id).Skip((pagina - 1) * cantidadRegistros).Take(cantidadRegistros).ToList();

                    var totalRegistros = db.cliente.Count();
                    var modelo = new ClienteIndex();
                    modelo.Clientes = clientes;
                    modelo.ActualPage = pagina;
                    modelo.Total = totalRegistros;
                    modelo.RecordsPage = cantidadRegistros;
                    modelo.valueQueryString = new RouteValueDictionary();

                    return View(modelo);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }
    }    
}