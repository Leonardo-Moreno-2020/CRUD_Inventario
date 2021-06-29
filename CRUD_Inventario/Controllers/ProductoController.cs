using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;
using Rotativa;

namespace CRUD_Inventario.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.producto.ToList());
            }
            
        }
        public static string NombreProveedor(int idProveedor)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.proveedor.Find(idProveedor).nombre;
            }
        }
        public ActionResult ListarProveedores()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return PartialView(Data_B.proveedor.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto newProducto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.producto.Add(newProducto);
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
                producto productoDetalle = Data_B.producto.Where(a => a.id == id).FirstOrDefault();
                return View(productoDetalle);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                var productDelete = Data_B.producto.Find(id);
                Data_B.producto.Remove(productDelete);
                Data_B.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    producto producto = Data_B.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(producto);
                }
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "error " + Cap_error);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var producto = Data_B.producto.Find(productoEdit.id);
                    producto.nombre = productoEdit.nombre;
                    producto.percio_unitario = productoEdit.percio_unitario;
                    producto.cantidad = productoEdit.cantidad;
                    producto.descripcion = productoEdit.descripcion;
                    producto.id_proveedor = productoEdit.id_proveedor;
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
        public ActionResult Reporte()
        {
            try
            {
                var Data_B = new inventario2021Entities();
                var query = from tabProveedor in Data_B.proveedor
                            join tabProducto in Data_B.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario
                            };
                return View(query);
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "error " + Cap_error);
                return View();
            }


        }
        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "reporte.pdf" };
        }
    }

}