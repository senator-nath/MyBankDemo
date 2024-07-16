using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Domain.Entities
{
    public class LGA
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign key for State
        public int StateId { get; set; }

        // Navigation property for related State
        public State State { get; set; }
    }
}
