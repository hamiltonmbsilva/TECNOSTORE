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

        public virtual String Rua { get; set; }
        public virtual String Bairro { get; set; }
        public virtual String CEP { get; set; }
        public virtual String Cidade { get; set; }
        public virtual String Estado { get; set; }

    }
}
