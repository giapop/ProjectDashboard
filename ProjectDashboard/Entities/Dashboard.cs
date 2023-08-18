using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Entities
{[Serializable]
   public class Dashboard
    {
        public int Id { get; set; }
        public Student student { get; set; }
       // public List<Materie> materii { get; set; }
       public Materie materie { get; set; }
        public Dashboard() { Id = 1;
            student = new Student();
            materie=new Materie();
        }
        public Dashboard(int id,Student _student,Materie mater)
        {
            Id = id;
            student = _student;
            // materii = new List<Materie>();
            materie = mater;
         
        }

    }
}
