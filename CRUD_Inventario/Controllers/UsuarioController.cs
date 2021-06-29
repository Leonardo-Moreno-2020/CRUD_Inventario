using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_Inventario.Models;
using System.Text;
using System.Web.Security;

namespace CRUD_Inventario.Controllers
{
    public class UsuarioController : Controller
    {
        
        // GET: Usuario

        public ActionResult Index()
        {
            using (var Data_B = new inventario2021Entities())
            {
                return View(Data_B.usuario.ToList());
            }
                
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    Data_B.usuario.Add(usuario);
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
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var tx = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                tx.Append(hash[i].ToString("X2"));
            }
            return tx.ToString();
        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    usuario findUser = Data_B.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
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
        public ActionResult Edit(usuario usuarioEdit)
        {
            try
            {
                using (var Data_B = new inventario2021Entities())
                {
                    usuario user = Data_B.usuario.Find(usuarioEdit.id);
                    user.nombre = usuarioEdit.nombre;
                    user.apellido = usuarioEdit.apellido;
                    user.fecha_nacimiento = usuarioEdit.fecha_nacimiento;
                    user.email = usuarioEdit.email;
                    user.password = usuarioEdit.password;

                    Data_B.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception Cap_error)
            {
                ModelState.AddModelError("", "error " + Cap_error);
                return View();

            }
        }
        public ActionResult Details(int id)
        {
            using (var Data_B = new inventario2021Entities())
            {
                usuario user = Data_B.usuario.Find(id);
                return View(user);
            }
        }
        public ActionResult Delete(int id)
        {
            using (inventario2021Entities Data_B = new inventario2021Entities())
            {
                var Usuario = Data_B.usuario.Find(id);
                Data_B.usuario.Remove(Usuario);
                Data_B.SaveChanges();
                return RedirectToAction("index");
            }
            
        }
        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String email, string password)
        {
            string passEncrip = UsuarioController.HashSHA1(password);
            using (var Data_B = new inventario2021Entities())
            {
                var userLogin = Data_B.usuario.FirstOrDefault(e => e.email == email && e.password == passEncrip);
                if (userLogin != null)
                {
                    FormsAuthentication.SetAuthCookie(userLogin.email, true);
                    Session["User"] = userLogin;
                    return RedirectToAction("Index");
                }
                else
                {
                    return Login("Verifique sus datos");
                }
            }
        }


        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}