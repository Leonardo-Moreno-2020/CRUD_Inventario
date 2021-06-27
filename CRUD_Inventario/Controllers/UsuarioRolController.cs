using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;

namespace CRUD_Inventario.Controllers
{
    public class UsuarioRolController : Controller
    {
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.usuariorol.ToList());
            }
                
        }
        public static string NombreUsuario(int idUsuario)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.usuario.Find(idUsuario).nombre;
            }
        }
        public ActionResult ListarUsuarios()
        {
            using (var Data_B = new inventario2021Entities())

            {
                return PartialView(Data_B.usuario.ToList());

            }
        }
        public static string NombreRol(int idRol)
        {
            using (var Data_B = new inventario2021Entities())
            {
                return Data_B.roles.Find(idRol).descripcion;
            }
        }
        public ActionResult ListarRoles()
        {
            using (var Data_B = new inventario2021Entities())

            {
                return PartialView(Data_B.roles.ToList());

            }
        }
        public ActionResult Create()

        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuariorol usuarioRol)

        {

            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())

                {
                    Data_B.usuariorol.Add(usuarioRol);
                    Data_B.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                usuariorol usuariorolDetalle = Data_B.usuariorol.Where(a => a.id == id).FirstOrDefault();
                return View(usuariorolDetalle);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                var usuariorolDelete = Data_B.usuariorol.Find(id);
                Data_B.usuariorol.Remove(usuariorolDelete);
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
                    usuariorol finduser = Data_B.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(finduser);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuariorol usuarioRolEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    usuariorol usuarioRol = Data_B.usuariorol.Find(usuarioRolEdit.id);
                    usuarioRol.idUsuario = usuarioRolEdit.idUsuario;
                    usuarioRol.idRol = usuarioRolEdit.idRol;


                    Data_B.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }

    }

}

