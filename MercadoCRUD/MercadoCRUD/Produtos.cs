using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoCRUD
{
    class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public float PrecoUnitario { get; set; }
        public string DataFabricacao { get; set; }
        public string DataValidade { get; set; }
        public int Estoque { get; set; }
        public int Quantidade { get; set; }



    }
}
