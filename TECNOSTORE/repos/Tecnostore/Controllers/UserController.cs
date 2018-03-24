using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tecnostore.Model.DB;
using Tecnostore.Model.DB.Model;
using Tecnostore.Model.Ultil;

namespace Tecnostore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult CadastrarUser()
        {
            return View( new User());
        }

        public ActionResult EntrarUser()
        {
            return View();
        }

        public ActionResult Logar(string user, string senha)
        {
            LoginUltils.Logar(user, senha);

            if(LoginUltils.User != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("EntrarUser");
            }
        }

        public ActionResult DeslogarUser()
        {
            LoginUltils.Deslogar();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GravarUser(User usuario)
        {
            DbFactory.Instance.UserRepository.SaveOrUptade(usuario);

            return RedirectToAction("EntrarUser");
        }
    }
}