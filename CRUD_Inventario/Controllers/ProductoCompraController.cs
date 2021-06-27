using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class ProductoCompraController : Controller
    {
        // GET: ProductoCompra
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities()) 
            {
                return View(Data_B.producto_compra.ToList());
            }                
        }
        public static int CompraProducto(int idCompra)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.compra.Find(idCompra).total;
            }
        }
        public ActionResult ListarCompra()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return PartialView(Data_B.compra.ToList());
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

        public ActionResult Create(producto_compra producto_compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.producto_compra.Add(producto_compra);
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
                producto_compra producto_compra = Data_B.producto_compra.Find(id);
                return View(producto_compra);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    producto_compra producto_compra = Data_B.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(producto_compra);
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

        public ActionResult Edit(producto_compra producto_compraEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var producto_compra = Data_B.producto_compra.Find(producto_compraEdit.id);
                    producto_compra.id = producto_compraEdit.id;
                    producto_compra.id_compra = producto_compraEdit.id_compra;
                    producto_compra.id_producto = producto_compraEdit.id_producto;
                    producto_compra.producto = producto_compraEdit.producto;
                    producto_compra.cantidad = producto_compra.cantidad;
                    producto_compra.compra = producto_compra.compra;
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
                var producto_compraDel = Data_B.producto_compra.Find(id);
                Data_B.producto_compra.Remove(producto_compraDel);
                Data_B.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }

}