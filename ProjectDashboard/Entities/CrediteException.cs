using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDashboard.Entities
{
   public class CrediteException:Exception
    {
        public string message { get; set; }
        public CrediteException(string mess)
        { 
            message = mess;
        }
        public override string Message
        {
            get
            {
                return message;
            }
        }
    }
}
