using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tecnostore.Model.DB;
using Tecnostore.Model.DB.Model;

namespace Tecnostore.Model.Ultil
{
    public class LoginUltils
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    return (User)HttpContext.Current.Session["User"];
                }

                return null;
            }
        }

        public static void Logar(string login, string senha)
        {
            var usuario = DbFactory.Instance.UserRepository.Login(login, senha);
            if (User != null)
            {
                HttpContext.Current.Session["User"] = usuario;
            }

        }

        public static void Deslogar()
        {
            HttpContext.Current.Session["User"] = null;
            HttpContext.Current.Session.Remove("User");

        }

    }
}
