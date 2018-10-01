using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string DateEvent { get; set; }

        public string ClientName { get; set; }

        public string StartEvent { get; set; }

        public string EndEvent { get; set; }

        public Worker Worker { get; set; }

        public int WorkerId {get;set;}
    }
}
