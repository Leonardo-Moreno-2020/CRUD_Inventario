using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;
using Rotativa;

namespace CRUD_Inventario.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.compra.ToList());
            }
        }
        public static string NombreUsuario(int idUsuario)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.usuario.Find(idUsuario).nombre;
            }
        }
        public ActionResult ListarUsuario()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return PartialView(Data_B.usuario.ToList());
            }
        }
        public static string NombreCliente(int idcliente)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.cliente.Find(idcliente).nombre;
            }
        }
        public ActionResult ListarCliente()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return PartialView(Data_B.cliente.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.compra.Add(compra);
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
                compra compra = Data_B.compra.Find(id);
                return View(compra);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    compra compra = Data_B.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(compra);
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
        public ActionResult Edit(compra compraEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var compra = Data_B.compra.Find(compraEdit.id);
                    compra.id = compraEdit.id;
                    compra.fecha = compraEdit.fecha;
                    compra.id_cliente = compraEdit.id_cliente;
                    compra.id_usuario = compraEdit.id_usuario;
                    compra.producto_compra = compraEdit.producto_compra;
                    compra.usuario = compraEdit.usuario;
                    compra.total = compraEdit.total;
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
                var compraDel = Data_B.compra.Find(id);
                Data_B.compra.Remove(compraDel);
                Data_B.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Reporte_1()
        {
            try
            {
                var Data_B = new inventario2021Entities();
                var query = from tabCliente in Data_B.cliente
                            join tabCompra in Data_B.compra on tabCliente.id equals tabCompra.id_cliente
                            select new Reporte_1
                            {
                                nombreCliente = tabCliente.nombre,
                                documentoCliente = tabCliente.documento,
                                emailCliente = tabCliente.email,
                                fechaCompra = tabCompra.fecha,
                                totalCompra = tabCompra.total
                            };

                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult ImprimirReporte_1()
        {
            return new ActionAsPdf("Reporte_1") { FileName = "reporte_1.pdf" };
        }
    } 
}