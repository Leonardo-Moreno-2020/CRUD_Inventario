using System;
using System.Collections.Generic;
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
    }
}