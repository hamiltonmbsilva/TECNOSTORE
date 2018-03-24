using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecnostore.Model.DB.Model
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual String Login { get; set; }
        public virtual String Senha { get; set; }

    }
    
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Login);
            Property(x => x.Senha);
          
        }
    }
}
