using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionLaverie.Domaine.Entities
{
    public class Machine
    {
        public int IdMachine { get; set; }
        public bool EstUsine { get; set; }
        public string Modele { get; set; }

        public Machine(int idMachine, bool estUsine, string modele)
        {
            IdMachine = idMachine;
            EstUsine = estUsine;
            Modele = modele;
        }
        public List<Cycle> Cycles { get; set; } = new List<Cycle>();
        public object Id { get; set; }
    }
}
