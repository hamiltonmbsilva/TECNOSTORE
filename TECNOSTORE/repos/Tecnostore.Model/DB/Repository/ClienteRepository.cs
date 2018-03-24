using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecnostore.Model.DB.Model;
using NHibernate;

namespace Tecnostore.Model.DB.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente>
    {
        //public ClienteRepository(ISession session) : base(session) { }

        public ClienteRepository(ISession session) : base(session) { }

        public IList<Cliente> GetAllByName(String nome)
        {
            try
            {
                return this.Session.Query<Cliente>().Where(w => w.Nome.ToLower() == nome.Trim()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Não achei o CLiente pelo Nome", ex);
            }
        }
    }
}
