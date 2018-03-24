using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecnostore.Model.DB.Model
{
    public class Cliente
    {
        public static List<Cliente> Clientes = new List<Cliente>();

        public virtual Guid Id { get; set; }
        public virtual String Nome { get; set; }
        public virtual String CPF { get; set; }
        public virtual String CNPJ { get; set; }
        public virtual DateTime DtNascimento { get; set; }
        public virtual String Email { get; set; }
        public virtual String Telefone { get; set; }
        public virtual bool Inativo { get; set; }        
        
    }

    public class ClienteMap : ClassMapping<Cliente>
    {
        public ClienteMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.CPF);
            Property(x => x.CNPJ);
            Property(x => x.DtNascimento);
            Property(x => x.Email);
            Property(x => x.Telefone);
            Property(x => x.Inativo);
        }
    }
        
        

}
