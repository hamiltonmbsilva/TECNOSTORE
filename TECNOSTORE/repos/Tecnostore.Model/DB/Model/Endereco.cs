using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecnostore.Model.DB.Model
{
    public class Endereco
    {
        public static List<Endereco> Esportes = new List<Endereco>();

        public virtual Guid Id { get; set; }
        public virtual String Logradouro { get; set; }
        public virtual String Complemento { get; set; }
        public virtual int Numero { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String CEP { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String Estado { get; set; }
        public virtual String Pais { get; set; }

    }

    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Logradouro);
            Property(x => x.Complemento);
            Property(x => x.Numero);
            Property(x => x.Bairro);
            Property(x => x.CEP);
            Property(x => x.Cidade);
            Property(x => x.Estado);
            Property(x => x.Pais);
        }
    }

}
