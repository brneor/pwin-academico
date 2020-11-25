using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Professor.Models
{
    public class ProfessorClass : Model
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
    }
}
