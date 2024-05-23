using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banchi.Classi
{
    public class Serramento
    {
        // numero del lato su cui sta questo serrramento
        public int NLato { get; set; }
        public double DistanzaDaPrecedenteInCm { get; set; }
        public double LarghezzaSerramentoinCm { get; set; }
        public  bool IsPorta { get; set; }
    }
}
