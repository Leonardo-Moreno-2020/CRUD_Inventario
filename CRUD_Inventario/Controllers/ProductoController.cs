using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

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

    }
}