using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Entities
{
   public class Principal
    {public int Id { get; set; }
        public List<Dashboard> lista { get; set; }
        public Principal() { }
        public Principal(int id)
        {
            Id = id;
            lista = new List<Dashboard>();
        }
    }
}
