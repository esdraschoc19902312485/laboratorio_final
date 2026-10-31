using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaCRUD
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int EdadMeses { get; set; }
        public string NombreDuenio { get; set; }
        public string TelefonoDuenio { get; set; }
    }
}