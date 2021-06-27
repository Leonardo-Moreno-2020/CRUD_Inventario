using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.cliente.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente Cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.cliente.Add(Cliente);
                    Data_B.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "Error" + Cap_error);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                cliente Cliente = Data_B.cliente.Find(id);
                return View(Cliente);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    cliente Cliente = Data_B.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(Cliente);
                }
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "error" + Cap_error);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente clienteEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var Cliente = Data_B.cliente.Find(clienteEdit.id);
                    Cliente.nombre = clienteEdit.nombre;
                    Cliente.documento = clienteEdit.documento;
                    Cliente.id = clienteEdit.id;
                    Cliente.email = clienteEdit.email;
                    Data_B.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "error" + Cap_error);
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                var clienteDel = Data_B.cliente.Find(id);
                Data_B.cliente.Remove(clienteDel);
                Data_B.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}