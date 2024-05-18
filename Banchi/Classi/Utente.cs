using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banchi.Classi
{
    internal class Utente
    {
        public string Username { get; set; }
        public List<Classe> ClassiDellUtente { get; set; }
    }
}
