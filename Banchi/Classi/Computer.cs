namespace Banchi
{
    public class Computer
    {
        public int NomeDispositivo { get; set; }
        public string MarcaComputer { get; set; }
        public string IndirizzoIPComputer { get; set; }
        public string NoteComputer { get; set; }
        public string Processore { get; set; }
        public string TipoSistema { get; set; }
        public Computer(int NomeDispositivo, string MarcaComputer, string IndirizzoIPComputer, string NoteComputer, 
            string Processore = null, string TipoSistema = null)
        {
            // inizializzazione delle proprietà
            this.NomeDispositivo = NomeDispositivo;
            this.MarcaComputer = MarcaComputer;
            this.IndirizzoIPComputer = IndirizzoIPComputer;
            this.NoteComputer = NoteComputer;
            this.Processore = Processore;
            this.TipoSistema = TipoSistema;
        }

    }
}
