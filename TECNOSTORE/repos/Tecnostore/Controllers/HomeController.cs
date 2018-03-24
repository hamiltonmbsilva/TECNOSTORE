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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(LoginUltils.User != null)
            {
                var clientes = DbFactory.Instance.ClienteRepository.FindAll();

                return View(clientes);
            }
            else
            {
                return RedirectToAction("EntrarUser", "User");
            }
            
        }

        public ActionResult InserirCliente()
        {
            return View();
        }

        public ActionResult GravarCliente(Cliente cliente)
        {
            DbFactory.Instance.ClienteRepository.SaveOrUptade(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult ApagarCliente(Guid id)
        {
            var cliente = DbFactory.Instance.ClienteRepository.FindById(id);

            if (cliente != null)
            {
                DbFactory.Instance.ClienteRepository.Delete(cliente);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Buscar(String edtBusca)
        {


            if (String.IsNullOrEmpty(edtBusca))
            {
                return RedirectToAction("Index");
            }

            var cliente = DbFactory.Instance.ClienteRepository.GetAllByName(edtBusca);

            return View("Index", cliente);
        }

        public ActionResult EditarCliente(Guid id)
        {
            var cliente = DbFactory.Instance.ClienteRepository.FindById(id);

            if (cliente != null)
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

    }
}