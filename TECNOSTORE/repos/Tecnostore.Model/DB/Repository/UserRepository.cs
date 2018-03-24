using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecnostore.Model.DB.Model;

namespace Tecnostore.Model.DB.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(ISession session) : base(session) { }

        public User Login(String login, String senha)
        {
            try
            {
                return this.Session.Query<User>().FirstOrDefault(f => f.Login == login && f.Senha == senha);

            }catch(Exception ex)
            {
                throw new Exception("Não achei o Usuraio", ex);
            }
        }
    }
}
