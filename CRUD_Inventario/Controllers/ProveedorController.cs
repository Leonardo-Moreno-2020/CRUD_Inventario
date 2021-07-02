using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult uploadCSV()
        {
            return View();
        }
        [HttpPost]

        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            //string para guardar la ruta
            string filePath = string.Empty;


            //condicion para saber si llego el archivo
            if (fileForm != null)
            {
                //ruta de la carpeta que gurdara el archivo
                string path = Server.MapPath("~/Uploads/");

                //condicion para saber si la ruta de la carpeta existe
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
                        var newProveedor = new proveedor
                        {
                            nombre = row.Split(';')[0],
                            direccion = row.Split(';')[1],
                            telefono = row.Split(';')[2],
                            nombre_contacto = row.Split(';')[3],
                        };

                        using (var Data_B = new inventario2021Entities())
                        {
                            Data_B.proveedor.Add(newProveedor);
                            Data_B.SaveChanges();
                        }
                    }
                }


            }
            return View();

        }
    }

}