using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Data.Models
{
   public class WorkTime
    {
        public int Id { get; set; }

        public string Day { get; set; }

        //TO DO change name
        public string StartEvent { get; set; }

        public string EndEvent { get; set; }

        public int WorkerId { get; set; }
        
        public Worker Worker { get; set; }
    }
}
