using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class ProductoImagenController : Controller
    {
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.producto_imagen.ToList());
            }
                
        }
        public static string NombreProducto(int idProducto)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.producto.Find(idProducto).nombre;
            }
        }
        public ActionResult ListarProducto()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return PartialView(Data_B.producto.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_imagen producto_imagen)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.producto_imagen.Add(producto_imagen);
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
                producto_imagen producto_imagen = Data_B.producto_imagen.Find(id);
                return View(producto_imagen);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    producto_imagen producto_imagen = Data_B.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(producto_imagen);
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
        public ActionResult Edit(producto_imagen producto_imagenEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var producto_imagen = Data_B.producto_imagen.Find(producto_imagenEdit.id);
                    producto_imagen.id = producto_imagen.id;
                    producto_imagen.id_producto = producto_imagen.id_producto;
                    producto_imagen.imagen = producto_imagen.imagen;
                    producto_imagen.producto = producto_imagen.producto;
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
                var producto_imagenDel = Data_B.producto_imagen.Find(id);
                Data_B.producto_imagen.Remove(producto_imagenDel);
                Data_B.SaveChanges();
                return RedirectToAction("Index");

            }


        }

    }
}