using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.proveedor.ToList());
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
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.proveedor.Add(proveedor);
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
                proveedor proveedor = Data_B.proveedor.Find(id);
                return View(proveedor);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    proveedor proveedor = Data_B.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(proveedor);
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
        public ActionResult Edit(proveedor proveedorEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var proveedor = Data_B.proveedor.Find(proveedorEdit.id);
                    proveedor.nombre = proveedorEdit.nombre;
                    proveedor.direccion = proveedorEdit.direccion;
                    proveedor.telefono = proveedorEdit.telefono;
                    proveedor.producto = proveedorEdit.producto;
                    proveedor.nombre_contacto = proveedorEdit.nombre_contacto;
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
                var proveedorDel = Data_B.proveedor.Find(id);
                Data_B.proveedor.Remove(proveedorDel);
                Data_B.SaveChanges();
                return RedirectToAction("Index");
            }

        }

    }

}