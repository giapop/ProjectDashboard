using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Entities
{[Serializable]
   public class Student
    {
        public string NrMatricol { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public Student() { NrMatricol = "0";
            Nume = "Default";
            Prenume = "Default";
        }
        public Student(string nr,string nume,string prenume)
        {
            NrMatricol = nr;
            Nume = nume;
            Prenume = prenume;

        }
        public override string ToString()
        {
            return $"{NrMatricol}-{Nume}-{Prenume}";
        }
    }
}
