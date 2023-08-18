using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Entities
{[Serializable]
    public class Materie
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public int NrCredite { get; set; }
        public int Nota { get; set; }
        public Materie()
        { Id = 0;
            Denumire = "Default";
            NrCredite = 0;
            Nota = 1;
        }
        public Materie(int id,string denumire,int nr,int nota)
        {
            Id = id;
            Denumire = denumire;
            NrCredite = nr;
            Nota = nota;
        }

        public override string ToString()
        {
            return $"{Id} - {Denumire} - {NrCredite}-{Nota}";
        }
    }
}
