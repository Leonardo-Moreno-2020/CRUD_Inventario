using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities()) 
            {
                return View(Data_B.roles.ToList());
            }               
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(roles roles)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    Data_B.roles.Add(roles);
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
                roles roles = Data_B.roles.Find(id);
                return View(roles);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    roles roles = Data_B.roles.Where(a => a.id == id).FirstOrDefault();
                    return View(roles);
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
        public ActionResult Edit(roles rolesEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    var roles = Data_B.roles.Find(rolesEdit.id);
                    roles.id = rolesEdit.id;
                    roles.descripcion = rolesEdit.descripcion;
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
                var rolesDel = Data_B.roles.Find(id);
                Data_B.roles.Remove(rolesDel);
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
                        var newRoles = new roles
                        {
                            descripcion = row.Split(';')[0],
                          
                        };

                        using (var Data_B = new inventario2021Entities())
                        {
                            Data_B.roles.Add(newRoles);
                            Data_B.SaveChanges();
                        }
                    }
                }


            }
            return View();

        }




    }
    



}